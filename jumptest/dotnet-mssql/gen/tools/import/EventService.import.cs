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
    /// Imports rows from a CSV file into core.event_service.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class EventServiceImporter
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
                var eventservice = new EventService();


                // event_type  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "event_type") >= 0)
                    eventservice.event_type = csv.GetField("event_type") ?? string.Empty;

                // objectname_filter  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "objectname_filter") >= 0)
                    eventservice.objectname_filter = csv.GetField("objectname_filter") ?? string.Empty;

                // methodname_filter  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "methodname_filter") >= 0)
                    eventservice.methodname_filter = csv.GetField("methodname_filter") ?? string.Empty;

                // script_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "script_id") >= 0)
                    eventservice.script_id = string.IsNullOrEmpty(csv.GetField("script_id")) ? default : Convert.ToInt64(csv.GetField("script_id"));

                EventServiceLogic.Create().insert(eventservice);
                count++;
            }

            return count;
        }
    }
}
