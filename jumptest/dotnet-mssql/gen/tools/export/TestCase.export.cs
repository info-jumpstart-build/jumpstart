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
    /// Exports all rows from app.test_case to a CSV file.
    /// </summary>
    public static class TestCaseExporter
    {
        public static int Export(string outputPath)
        {
            var records = TestCaseLogic.Create().select<TestCase>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);

            // Header row — column order matches the domain model attribute order
            csv.WriteField("id");
            csv.WriteField("test_plan_id");
            csv.WriteField("code");
            csv.WriteField("area");
            csv.WriteField("title");
            csv.WriteField("preconditions");
            csv.WriteField("steps");
            csv.WriteField("expected_result");
            csv.WriteField("priority");
            csv.WriteField("component");
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
                csv.WriteField(record.test_plan_id);
                csv.WriteField(record.code);
                csv.WriteField(record.area);
                csv.WriteField(record.title);
                csv.WriteField(record.preconditions);
                csv.WriteField(record.steps);
                csv.WriteField(record.expected_result);
                csv.WriteField(record.priority);
                csv.WriteField(record.component);
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
