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
    /// Exports all rows from core.exec_log to a CSV file.
    /// </summary>
    public static class ExecLogExporter
    {
        public static int Export(string outputPath)
        {
            var records = ExecLogLogic.Create().select<ExecLog>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);

            // Header row — column order matches the domain model attribute order
            csv.WriteField("id");
            csv.WriteField("token");
            csv.WriteField("workflow_id");
            csv.WriteField("start_time");
            csv.WriteField("end_time");
            csv.WriteField("exec_status_id");
            csv.WriteField("stdout");
            csv.WriteField("stderr");
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
                csv.WriteField(record.token);
                csv.WriteField(record.workflow_id);
                csv.WriteField(record.start_time);
                csv.WriteField(record.end_time);
                csv.WriteField(record.exec_status_id);
                csv.WriteField(record.stdout);
                csv.WriteField(record.stderr);
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
