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
    /// Exports all rows from app.test_result to a CSV file.
    /// </summary>
    public static class TestResultExporter
    {
        public static int Export(string outputPath)
        {
            var records = TestResultLogic.Create().select<TestResult>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);

            // Header row — column order matches the domain model attribute order
            csv.WriteField("id");
            csv.WriteField("test_run_id");
            csv.WriteField("test_case_id");
            csv.WriteField("test_result_status_id");
            csv.WriteField("executed_at");
            csv.WriteField("executed_by_id");
            csv.WriteField("actual_result");
            csv.WriteField("notes");
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
                csv.WriteField(record.test_run_id);
                csv.WriteField(record.test_case_id);
                csv.WriteField(record.test_result_status_id);
                csv.WriteField(record.executed_at);
                csv.WriteField(record.executed_by_id);
                csv.WriteField(record.actual_result);
                csv.WriteField(record.notes);
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
