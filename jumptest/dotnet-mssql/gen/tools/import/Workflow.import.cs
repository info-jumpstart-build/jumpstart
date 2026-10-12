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
    /// Imports rows from a CSV file into core.workflow.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class WorkflowImporter
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
                var workflow = new Workflow();


                // workflow_type_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "workflow_type_id") >= 0)
                    workflow.workflow_type_id = string.IsNullOrEmpty(csv.GetField("workflow_type_id")) ? default : Convert.ToInt64(csv.GetField("workflow_type_id"));

                // parent_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "parent_id") >= 0)
                    workflow.parent_id = string.IsNullOrEmpty(csv.GetField("parent_id")) ? default : Convert.ToInt64(csv.GetField("parent_id"));

                // name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    workflow.name = csv.GetField("name") ?? string.Empty;

                // seq  (INTEGER → int)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "seq") >= 0)
                    workflow.seq = string.IsNullOrEmpty(csv.GetField("seq")) ? default : Convert.ToInt32(csv.GetField("seq"));

                // server_node_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "server_node_id") >= 0)
                    workflow.server_node_id = string.IsNullOrEmpty(csv.GetField("server_node_id")) ? default : Convert.ToInt64(csv.GetField("server_node_id"));

                // process_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "process_id") >= 0)
                    workflow.process_id = string.IsNullOrEmpty(csv.GetField("process_id")) ? default : Convert.ToInt64(csv.GetField("process_id"));

                // exec_status_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "exec_status_id") >= 0)
                    workflow.exec_status_id = string.IsNullOrEmpty(csv.GetField("exec_status_id")) ? default : Convert.ToInt64(csv.GetField("exec_status_id"));

                // last_start_time  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_start_time") >= 0)
                    workflow.last_start_time = string.IsNullOrEmpty(csv.GetField("last_start_time")) ? default : Convert.ToDateTime(csv.GetField("last_start_time"));

                // last_end_time  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_end_time") >= 0)
                    workflow.last_end_time = string.IsNullOrEmpty(csv.GetField("last_end_time")) ? default : Convert.ToDateTime(csv.GetField("last_end_time"));

                // schedule_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "schedule_id") >= 0)
                    workflow.schedule_id = string.IsNullOrEmpty(csv.GetField("schedule_id")) ? default : Convert.ToInt64(csv.GetField("schedule_id"));

                // on_failure_action_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "on_failure_action_id") >= 0)
                    workflow.on_failure_action_id = string.IsNullOrEmpty(csv.GetField("on_failure_action_id")) ? default : Convert.ToInt64(csv.GetField("on_failure_action_id"));

                WorkflowLogic.Create().insert(workflow);
                count++;
            }

            return count;
        }
    }
}
