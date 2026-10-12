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
    /// Imports rows from a CSV file into app.test_result.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class TestResultImporter
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
                var testresult = new TestResult();


                // test_run_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "test_run_id") >= 0)
                    testresult.test_run_id = string.IsNullOrEmpty(csv.GetField("test_run_id")) ? default : Convert.ToInt64(csv.GetField("test_run_id"));

                // test_case_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "test_case_id") >= 0)
                    testresult.test_case_id = string.IsNullOrEmpty(csv.GetField("test_case_id")) ? default : Convert.ToInt64(csv.GetField("test_case_id"));

                // test_result_status_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "test_result_status_id") >= 0)
                    testresult.test_result_status_id = string.IsNullOrEmpty(csv.GetField("test_result_status_id")) ? default : Convert.ToInt64(csv.GetField("test_result_status_id"));

                // executed_at  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "executed_at") >= 0)
                    testresult.executed_at = string.IsNullOrEmpty(csv.GetField("executed_at")) ? default : Convert.ToDateTime(csv.GetField("executed_at"));

<<<<<<< HEAD
                // executed_by  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "executed_by") >= 0)
                    testresult.executed_by = csv.GetField("executed_by") ?? string.Empty;
=======
                // executed_by_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "executed_by_id") >= 0)
                    testresult.executed_by_id = string.IsNullOrEmpty(csv.GetField("executed_by_id")) ? default : Convert.ToInt64(csv.GetField("executed_by_id"));
>>>>>>> main

                // actual_result  (TEXT → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "actual_result") >= 0)
                    testresult.actual_result = csv.GetField("actual_result") ?? string.Empty;

                // notes  (TEXT → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "notes") >= 0)
                    testresult.notes = csv.GetField("notes") ?? string.Empty;

                TestResultLogic.Create().insert(testresult);
                count++;
            }

            return count;
        }
    }
}
