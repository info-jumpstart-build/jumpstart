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
    /// Exports all rows from core.workflow to a CSV file.
    /// </summary>
    public static class WorkflowExporter
    {
        public static int Export(string outputPath)
        {
            var records = WorkflowLogic.Create().select<Workflow>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);

            // Header row — column order matches the domain model attribute order
            csv.WriteField("id");
            csv.WriteField("workflow_type_id");
            csv.WriteField("parent_id");
            csv.WriteField("name");
            csv.WriteField("seq");
            csv.WriteField("server_node_id");
            csv.WriteField("process_id");
            csv.WriteField("exec_status_id");
            csv.WriteField("last_start_time");
            csv.WriteField("last_end_time");
            csv.WriteField("schedule_id");
            csv.WriteField("on_failure_action_id");
            csv.WriteField("is_active");
            csv.WriteField("created_by");
            csv.WriteField("last_updated");
            csv.WriteField("last_updated_by");
            csv.WriteField("txn_id");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.id);
                csv.WriteField(record.workflow_type_id);
                csv.WriteField(record.parent_id);
                csv.WriteField(record.name);
                csv.WriteField(record.seq);
                csv.WriteField(record.server_node_id);
                csv.WriteField(record.process_id);
                csv.WriteField(record.exec_status_id);
                csv.WriteField(record.last_start_time);
                csv.WriteField(record.last_end_time);
                csv.WriteField(record.schedule_id);
                csv.WriteField(record.on_failure_action_id);
                csv.WriteField(record.is_active);
                csv.WriteField(record.created_by);
                csv.WriteField(record.last_updated);
                csv.WriteField(record.last_updated_by);
                csv.WriteField(record.txn_id);
                csv.NextRecord();
            }

            return records.Count;
        }
    }
}
