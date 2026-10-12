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
    /// Imports rows from a CSV file into core.exec_log.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class ExecLogImporter
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
                var execlog = new ExecLog();


                // token  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "token") >= 0)
                    execlog.token = csv.GetField("token") ?? string.Empty;

                // workflow_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "workflow_id") >= 0)
                    execlog.workflow_id = string.IsNullOrEmpty(csv.GetField("workflow_id")) ? default : Convert.ToInt64(csv.GetField("workflow_id"));

                // start_time  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "start_time") >= 0)
                    execlog.start_time = string.IsNullOrEmpty(csv.GetField("start_time")) ? default : Convert.ToDateTime(csv.GetField("start_time"));

                // end_time  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "end_time") >= 0)
                    execlog.end_time = string.IsNullOrEmpty(csv.GetField("end_time")) ? default : Convert.ToDateTime(csv.GetField("end_time"));

                // exec_status_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "exec_status_id") >= 0)
                    execlog.exec_status_id = string.IsNullOrEmpty(csv.GetField("exec_status_id")) ? default : Convert.ToInt64(csv.GetField("exec_status_id"));

                // stdout  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "stdout") >= 0)
                    execlog.stdout = csv.GetField("stdout") ?? string.Empty;

                // stderr  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "stderr") >= 0)
                    execlog.stderr = csv.GetField("stderr") ?? string.Empty;

                ExecLogLogic.Create().insert(execlog);
                count++;
            }

            return count;
        }
    }
}
