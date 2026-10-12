using System;
using System.Collections.Generic;
using jumptest;

namespace jumptest
{
    class Program
    {
        static void Main(string[] args)
        {
            string table = null;
            string output = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--table" && i + 1 < args.Length)
                    table = args[++i];
                else if (args[i] == "--output" && i + 1 < args.Length)
                    output = args[++i];
            }

            if (string.IsNullOrEmpty(table))
            {
                PrintUsage();
                Environment.Exit(1);
            }

            if (!Exporters.TryGetValue(table, out var exporter))
            {
                Console.Error.WriteLine($"Error: Unknown table '{table}'");
                Console.Error.WriteLine();
                PrintUsage();
                Environment.Exit(1);
            }

            string outputPath = output ?? $"{table}.csv";

            try
            {
                int count = exporter(outputPath);
                Console.WriteLine($"Exported {count} record(s) from '{table}' to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error exporting '{table}': {ex.Message}");
                if (ex.InnerException != null)
                    Console.Error.WriteLine($"  {ex.InnerException.Message}");
                Environment.Exit(1);
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: export --table <tablename> [--output <filepath>]");
            Console.WriteLine();
            Console.WriteLine("  --table    The table to export (required)");
            Console.WriteLine("  --output   Output CSV file path (default: <tablename>.csv)");
            Console.WriteLine();
            Console.WriteLine("Available tables:");
            foreach (var key in Exporters.Keys)
                Console.WriteLine($"  {key}");
        }

        static readonly Dictionary<string, Func<string, int>> Exporters = new()
        {
            { "testresultstatus", path => TestResultStatusExporter.Export(path) },
            { "testplan", path => TestPlanExporter.Export(path) },
            { "org", path => OrgExporter.Export(path) },
<<<<<<< HEAD
            { "principal", path => PrincipalExporter.Export(path) },
=======
>>>>>>> main
            { "operation", path => OperationExporter.Export(path) },
            { "oprole", path => OpRoleExporter.Export(path) },
            { "cronevery", path => CronEveryExporter.Export(path) },
            { "cronminute", path => CronMinuteExporter.Export(path) },
            { "cronhour", path => CronHourExporter.Export(path) },
            { "crondom", path => CronDomExporter.Export(path) },
            { "cronmonth", path => CronMonthExporter.Export(path) },
            { "crondow", path => CronDowExporter.Export(path) },
            { "navmenu", path => NavMenuExporter.Export(path) },
            { "datasource", path => DataSourceExporter.Export(path) },
            { "agentstatus", path => AgentStatusExporter.Export(path) },
            { "onfailure", path => OnFailureExporter.Export(path) },
            { "execstatus", path => ExecStatusExporter.Export(path) },
            { "servernodestatus", path => ServerNodeStatusExporter.Export(path) },
            { "scripttype", path => ScriptTypeExporter.Export(path) },
            { "servernodetype", path => ServerNodeTypeExporter.Export(path) },
            { "workflowtype", path => WorkflowTypeExporter.Export(path) },
<<<<<<< HEAD
            { "testcase", path => TestCaseExporter.Export(path) },
            { "testrun", path => TestRunExporter.Export(path) },
            { "principalorg", path => PrincipalOrgExporter.Export(path) },
            { "oprolemap", path => OpRoleMapExporter.Export(path) },
            { "oprolemember", path => OpRoleMemberExporter.Export(path) },
=======
            { "principalstatus", path => PrincipalStatusExporter.Export(path) },
            { "testcase", path => TestCaseExporter.Export(path) },
            { "testrun", path => TestRunExporter.Export(path) },
            { "oprolemap", path => OpRoleMapExporter.Export(path) },
>>>>>>> main
            { "schedule", path => ScheduleExporter.Export(path) },
            { "sql", path => SqlExporter.Export(path) },
            { "script", path => ScriptExporter.Export(path) },
            { "servernode", path => ServerNodeExporter.Export(path) },
<<<<<<< HEAD
            { "testresult", path => TestResultExporter.Export(path) },
            { "eventservice", path => EventServiceExporter.Export(path) },
            { "process", path => ProcessExporter.Export(path) },
=======
            { "principal", path => PrincipalExporter.Export(path) },
            { "eventservice", path => EventServiceExporter.Export(path) },
            { "process", path => ProcessExporter.Export(path) },
            { "testresult", path => TestResultExporter.Export(path) },
            { "principalorg", path => PrincipalOrgExporter.Export(path) },
            { "oprolemember", path => OpRoleMemberExporter.Export(path) },
>>>>>>> main
            { "workflow", path => WorkflowExporter.Export(path) },
            { "execlog", path => ExecLogExporter.Export(path) },
        };
    }
}
