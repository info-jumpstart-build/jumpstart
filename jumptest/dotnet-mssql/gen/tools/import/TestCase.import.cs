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
    /// Imports rows from a CSV file into app.test_case.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class TestCaseImporter
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
                var testcase = new TestCase();


                // test_plan_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "test_plan_id") >= 0)
                    testcase.test_plan_id = string.IsNullOrEmpty(csv.GetField("test_plan_id")) ? default : Convert.ToInt64(csv.GetField("test_plan_id"));

                // code  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "code") >= 0)
                    testcase.code = csv.GetField("code") ?? string.Empty;

                // area  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "area") >= 0)
                    testcase.area = csv.GetField("area") ?? string.Empty;

                // title  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "title") >= 0)
                    testcase.title = csv.GetField("title") ?? string.Empty;

                // preconditions  (TEXT → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "preconditions") >= 0)
                    testcase.preconditions = csv.GetField("preconditions") ?? string.Empty;

                // steps  (TEXT → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "steps") >= 0)
                    testcase.steps = csv.GetField("steps") ?? string.Empty;

                // expected_result  (TEXT → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "expected_result") >= 0)
                    testcase.expected_result = csv.GetField("expected_result") ?? string.Empty;

                // priority  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "priority") >= 0)
                    testcase.priority = csv.GetField("priority") ?? string.Empty;

                // component  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "component") >= 0)
                    testcase.component = csv.GetField("component") ?? string.Empty;

                TestCaseLogic.Create().insert(testcase);
                count++;
            }

            return count;
        }
    }
}
