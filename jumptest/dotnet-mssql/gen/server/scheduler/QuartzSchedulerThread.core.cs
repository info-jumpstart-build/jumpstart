using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using CronExpressionDescriptor;
using jumptest;
using jumptest.core;

namespace jumptest.core
{
    /// <summary>
    /// Startup thread that integrates Quartz.NET with the application's workflow scheduling system.
    ///
    /// On startup it reads every active workflow whose linked Schedule record has all five cron
    /// component FK fields populated (plus the cron_every modifier), assembles a standard
    /// 5-field cron expression from those parts, and registers a corresponding Quartz job/trigger
    /// pair.  Every five minutes it reloads all schedules from the database so that changes made
    /// at runtime are picked up without a server restart.
    ///
    /// Standard cron field order:  minute  hour  day-of-month  month  day-of-week
    /// Example assembled expression: "30 8 * * 1-5" = weekdays at 08:30.
    ///
    /// The cron_every modifier affects non-wildcard fields:
    ///   "Every"   — converts a numeric value N to */N  (e.g. minute=5 → */5, every 5 minutes)
    ///   "Exactly" — uses the value literally           (e.g. minute=30 → 30, at :30 past)
    /// Wildcard values (* and ?) always pass through unchanged regardless of the modifier.
    ///
    /// Schedules with any NULL cron FK (e.g. the "Manual" entry) are excluded automatically
    /// by the INNER JOINs in the load query.
    ///
    /// Quartz requires a 6-field expression (with a leading seconds field); a fixed "0" is
    /// prepended internally.  The human-readable description uses the 5-field form directly.
    ///
    /// When a trigger fires, <see cref="WorkflowJob"/> calls
    /// <see cref="SchedulerLogic.execute"/> which enqueues the workflow for dispatch to agents.
    /// </summary>
    public class QuartzSchedulerThread
    {
        private IScheduler? _quartzScheduler;
        private readonly CancellationTokenSource _cts = new();
        private Task? _backgroundTask;

        private static readonly TimeSpan ReloadInterval = TimeSpan.FromMinutes(5);

        // SQL that returns one row per (workflow, schedule) pair that has a fully-defined
        // cron schedule.  INNER JOINs on all five cron tables plus cron_every exclude any
        // schedule where one or more cron FK columns are NULL (covers the "Manual" entry
        // and any incomplete rows).
        private const string LoadSql = @"
            SELECT w.id          AS workflow_id,  
                   w.name        AS workflow_name,
                   s.id          AS schedule_id,  
                   s.name        AS schedule_name,
                   COALESCE(ce.name, '') AS cron_every,        
                   COALESCE(cm.name, '*') AS cron_minute,       
                   COALESCE(ch.name, '*') AS cron_hour,         
                   COALESCE(cd.name, '*') AS cron_dom,          
                   COALESCE(cmo.name, '*') AS cron_month,        
                   COALESCE(cdow.name, '?') AS cron_dow          
            FROM   core.workflow    w                                  
            JOIN   core.schedule    s    ON s.id    = w.schedule_id    
            LEFT OUTER JOIN   core.cron_every  ce   ON ce.id   = s.cron_every_id  
            LEFT OUTER JOIN   core.cron_minute cm   ON cm.id   = s.cron_minute_id 
            LEFT OUTER JOIN   core.cron_hour   ch   ON ch.id   = s.cron_hour_id   
            LEFT OUTER JOIN   core.cron_dom    cd   ON cd.id   = s.cron_dom_id    
            LEFT OUTER JOIN   core.cron_month  cmo  ON cmo.id  = s.cron_month_id  
            LEFT OUTER JOIN   core.cron_dow    cdow ON cdow.id = s.cron_dow_id    
            WHERE  w.is_active = 1                                      ";

        // =====================================================================
        // Public API
        // =====================================================================

        /// <summary>Starts the background scheduling loop.</summary>
        public void Start()
        {
            _backgroundTask = Task.Run(async () =>
            {
                try   { await RunAsync(_cts.Token); }
                catch (Exception ex)
                {
                    Logger.Error($"QuartzSchedulerThread: Fatal error: {ex.Message}", ex);
                }
            });
            Console.WriteLine("QuartzSchedulerThread: Started");
        }

        /// <summary>Signals the background loop to stop and waits for clean shutdown.</summary>
        public void Stop()
        {
            Console.WriteLine("QuartzSchedulerThread: Stopping...");
            _cts.Cancel();
            try
            {
                if (_quartzScheduler != null && !_quartzScheduler.IsShutdown)
                    _quartzScheduler.Shutdown(waitForJobsToComplete: false).Wait(TimeSpan.FromSeconds(10));
            }
            catch { /* best-effort */ }
            _backgroundTask?.Wait(TimeSpan.FromSeconds(15));
            Console.WriteLine("QuartzSchedulerThread: Stopped");
        }

        // =====================================================================
        // Private implementation
        // =====================================================================

        private async Task RunAsync(CancellationToken ct)
        {
            var factory = new StdSchedulerFactory();
            _quartzScheduler = await factory.GetScheduler(ct);
            await _quartzScheduler.Start(ct);
            Console.WriteLine("QuartzSchedulerThread: Quartz scheduler started");

            await LoadAndScheduleWorkflowsAsync(ct);

            while (!ct.IsCancellationRequested)
            {
                try   { await Task.Delay(ReloadInterval, ct); }
                catch (OperationCanceledException) { break; }

                Console.WriteLine("QuartzSchedulerThread: Reloading workflow schedules from database...");
                await _quartzScheduler.Clear(ct);
                await LoadAndScheduleWorkflowsAsync(ct);
            }

            if (!_quartzScheduler.IsShutdown)
                await _quartzScheduler.Shutdown(waitForJobsToComplete: false, ct);

            Console.WriteLine("QuartzSchedulerThread: Quartz scheduler stopped");
        }

        /// <summary>
        /// Executes the JOIN query, assembles each cron expression from its component
        /// parts (applying the Every/Exactly modifier), and registers a Quartz job +
        /// trigger for every qualifying workflow.
        /// </summary>
        private async Task LoadAndScheduleWorkflowsAsync(CancellationToken ct)
        {
            var rows = new List<WorkflowCronRow>();

            DBPersist.select(rdr =>
            {
                string Get(string col) =>
                    rdr.IsDBNull(rdr.GetOrdinal(col)) ? "" : rdr.GetString(rdr.GetOrdinal(col));

                rows.Add(new WorkflowCronRow(
                    WorkflowId   : rdr.GetInt64(rdr.GetOrdinal("workflow_id")),
                    WorkflowName : Get("workflow_name"),
                    ScheduleId   : rdr.GetInt64(rdr.GetOrdinal("schedule_id")),
                    ScheduleName : Get("schedule_name"),
                    CronEvery    : Get("cron_every"),
                    CronMinute   : Get("cron_minute"),
                    CronHour     : Get("cron_hour"),
                    CronDom      : Get("cron_dom"),
                    CronMonth    : Get("cron_month"),
                    CronDow      : Get("cron_dow")
                ));
            }, LoadSql);

            if (rows.Count == 0)
            {
                Console.WriteLine("QuartzSchedulerThread: No workflows with fully-defined cron schedules found.");
                return;
            }

            Console.WriteLine($"QuartzSchedulerThread: Found {rows.Count} workflow(s) to schedule.");

            foreach (var row in rows)
            {
                try
                {
                    // Build the 5-field standard cron expression, applying the Every/Exactly
                    // modifier to each non-wildcard field.
                    string minute = ApplyEvery(row.CronMinute, row.CronEvery);
                    string hour   = ApplyEvery(row.CronHour,   row.CronEvery);
                    string dom    = ApplyEvery(row.CronDom,    row.CronEvery);
                    string month  = ApplyEvery(row.CronMonth,  row.CronEvery);
                    string dow    = ApplyEvery(row.CronDow,    row.CronEvery);

                    // Quartz requires exactly one of dom/dow to be '?'.
                    // When both default to '*' (neither cron param set), use dom=* dow=? convention.
                    (dom, dow) = ReconcileDomDow(dom, dow);

                    // 5-field expression used for human-readable description.
                    string cronExpr5 = $"{minute} {hour} {dom} {month} {dow}";

                    // Quartz requires a 6-field expression; prepend a fixed 0 for seconds.
                    string quartzExpr = $"0 {cronExpr5}";

                    await ScheduleWorkflowAsync(row, quartzExpr, cronExpr5, ct);
                }
                catch (Exception ex)
                {
                    Logger.Error(
                        $"QuartzSchedulerThread: Failed to register workflow {row.WorkflowId} " +
                        $"({row.WorkflowName}): {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Applies the Every/Exactly modifier to a single cron field value.
        /// Wildcards (* and ?) and compound expressions (containing /, -, or ,)
        /// are always returned unchanged.
        /// "Every"   converts a plain numeric value N to */N.
        /// "Exactly" (or any other value) returns the value as-is.
        /// </summary>
        private static string ApplyEvery(string value, string cronEvery)
        {
            if (string.IsNullOrEmpty(value) ||
                value == "*" || value == "?" ||
                value.Contains('/') || value.Contains('-') || value.Contains(','))
                return value;

            if (string.Equals(cronEvery, "Every", StringComparison.OrdinalIgnoreCase))
                return $"*/{value}";

            return value; // "Exactly" — use the literal value
        }

        /// <summary>
        /// Ensures exactly one of dom/dow is '?' as required by Quartz.
        /// Rules (in priority order):
        ///   - specific dom + specific dow → dom wins, dow becomes '?'
        ///   - specific dom only           → dow becomes '?'
        ///   - specific dow only           → dom becomes '?'
        ///   - both wildcard               → dom stays '*', dow becomes '?' (standard convention)
        /// </summary>
        private static (string dom, string dow) ReconcileDomDow(string dom, string dow)
        {
            bool domIsWild = dom == "*" || dom == "?";
            bool dowIsWild = dow == "*" || dow == "?";

            if (!domIsWild && !dowIsWild) return (dom,  "?"); // both specific: dom wins
            if (!domIsWild)               return (dom,  "?"); // specific dom:  dow → ?
            if (!dowIsWild)               return ("?",  dow); // specific dow:  dom → ?
            return ("*", "?");                                // both wild:     dom=* dow=?
        }

        /// <summary>Registers a single workflow as a Quartz job with a cron trigger.</summary>
        private async Task ScheduleWorkflowAsync(WorkflowCronRow row, string quartzExpr, string cronExpr5, CancellationToken ct)
        {
            var jobKey = new JobKey($"workflow-{row.WorkflowId}", "workflows");

            if (await _quartzScheduler!.CheckExists(jobKey, ct))
            {
                Console.WriteLine($"QuartzSchedulerThread: Workflow {row.WorkflowId} already registered — skipping.");
                return;
            }

            IJobDetail job = JobBuilder.Create<WorkflowJob>()
                .WithIdentity(jobKey)
                .WithDescription(row.WorkflowName)
                .UsingJobData("workflowId", row.WorkflowId)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger-{row.WorkflowId}", "workflows")
                .WithCronSchedule(quartzExpr)
                .WithDescription(row.ScheduleName)
                .Build();

            await _quartzScheduler.ScheduleJob(job, trigger, ct);

            string humanDescription = GetCronDescription(cronExpr5);
            DateTimeOffset? nextFire = trigger.GetNextFireTimeUtc();

            Console.WriteLine(
                $"QuartzSchedulerThread: Registered workflow {row.WorkflowId} ({row.WorkflowName}) " +
                $"| schedule: '{row.ScheduleName}' " +
                $"| expression: {cronExpr5} " +
                $"| runs: {humanDescription} " +
                $"| next: {(nextFire.HasValue ? nextFire.Value.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
        }

        /// <summary>
        /// Returns an English-language description of a standard 5-field cron expression
        /// (minute hour day-of-month month day-of-week).
        /// The raw expression is returned as a fallback if parsing fails.
        /// </summary>
        public static string GetCronDescription(string cronExpr5)
        {
            if (string.IsNullOrWhiteSpace(cronExpr5))
                return "(no schedule)";

            try
            {
                return ExpressionDescriptor.GetDescription(cronExpr5.Trim());
            }
            catch
            {
                return cronExpr5;
            }
        }
    }

    // =========================================================================
    // Quartz IJob implementation
    // =========================================================================

    /// <summary>
    /// Quartz job that fires when a workflow's cron trigger is due.
    /// Delegates to <see cref="SchedulerLogic.execute"/> which enqueues the workflow
    /// for dispatch to the configured script agents.
    /// </summary>
    public class WorkflowJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            long workflowId = context.JobDetail.JobDataMap.GetLong("workflowId");
            Console.WriteLine($"WorkflowJob: Trigger fired for workflow {workflowId} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            try
            {
                SchedulerLogic.Create().execute(workflowId);
                Console.WriteLine($"WorkflowJob: Workflow {workflowId} dispatched successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error($"WorkflowJob: Error dispatching workflow {workflowId}: {ex.Message}", ex);
                throw new JobExecutionException(ex, refireImmediately: false);
            }

            await Task.CompletedTask;
        }
    }

    // =========================================================================
    // Helper record
    // =========================================================================

    /// <summary>
    /// Holds one row from the workflow × schedule × cron-component JOIN query.
    /// </summary>
    internal record WorkflowCronRow(
        long   WorkflowId,
        string WorkflowName,
        long   ScheduleId,
        string ScheduleName,
        string CronEvery,
        string CronMinute,
        string CronHour,
        string CronDom,
        string CronMonth,
        string CronDow);
}
