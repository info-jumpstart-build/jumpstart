using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using jumptest;

namespace jumptest
{
    /// <summary>
    /// Imports rows from a CSV file into core.schedule.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class ScheduleImporter
    {
        public static int Import(string inputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,       // tolerate columns absent in the CSV
                BadDataFound = null,             // skip malformed fields rather than throwing
            };

            int count = 0;

            using var reader = new StreamReader(inputPath, System.Text.Encoding.UTF8);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var schedule = new Schedule();


                // name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    schedule.name = csv.GetField("name") ?? string.Empty;

                // cron_every_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "cron_every_id") >= 0)
                    schedule.cron_every_id = string.IsNullOrEmpty(csv.GetField("cron_every_id")) ? default : Convert.ToInt64(csv.GetField("cron_every_id"));

                // cron_minute_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "cron_minute_id") >= 0)
                    schedule.cron_minute_id = string.IsNullOrEmpty(csv.GetField("cron_minute_id")) ? default : Convert.ToInt64(csv.GetField("cron_minute_id"));

                // cron_hour_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "cron_hour_id") >= 0)
                    schedule.cron_hour_id = string.IsNullOrEmpty(csv.GetField("cron_hour_id")) ? default : Convert.ToInt64(csv.GetField("cron_hour_id"));

                // cron_dom_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "cron_dom_id") >= 0)
                    schedule.cron_dom_id = string.IsNullOrEmpty(csv.GetField("cron_dom_id")) ? default : Convert.ToInt64(csv.GetField("cron_dom_id"));

                // cron_month_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "cron_month_id") >= 0)
                    schedule.cron_month_id = string.IsNullOrEmpty(csv.GetField("cron_month_id")) ? default : Convert.ToInt64(csv.GetField("cron_month_id"));

                // cron_dow_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "cron_dow_id") >= 0)
                    schedule.cron_dow_id = string.IsNullOrEmpty(csv.GetField("cron_dow_id")) ? default : Convert.ToInt64(csv.GetField("cron_dow_id"));

                // schedule_label  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "schedule_label") >= 0)
                    schedule.schedule_label = csv.GetField("schedule_label") ?? string.Empty;

                // next_run_time  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "next_run_time") >= 0)
                    schedule.next_run_time = string.IsNullOrEmpty(csv.GetField("next_run_time")) ? default : Convert.ToDateTime(csv.GetField("next_run_time"));

                // last_run_time  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_run_time") >= 0)
                    schedule.last_run_time = string.IsNullOrEmpty(csv.GetField("last_run_time")) ? default : Convert.ToDateTime(csv.GetField("last_run_time"));

                ScheduleLogic.Create().insert(schedule);
                count++;
            }

            return count;
        }
    }
}
