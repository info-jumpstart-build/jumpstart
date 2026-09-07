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
    /// Imports rows from a CSV file into app.test_run.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class TestRunImporter
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
                var testrun = new TestRun();


                // name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    testrun.name = csv.GetField("name") ?? string.Empty;

                // test_plan_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "test_plan_id") >= 0)
                    testrun.test_plan_id = string.IsNullOrEmpty(csv.GetField("test_plan_id")) ? default : Convert.ToInt64(csv.GetField("test_plan_id"));

                // run_at  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "run_at") >= 0)
                    testrun.run_at = string.IsNullOrEmpty(csv.GetField("run_at")) ? default : Convert.ToDateTime(csv.GetField("run_at"));

                // run_by  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "run_by") >= 0)
                    testrun.run_by = csv.GetField("run_by") ?? string.Empty;

                // notes  (TEXT → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "notes") >= 0)
                    testrun.notes = csv.GetField("notes") ?? string.Empty;

                TestRunLogic.Create().insert(testrun);
                count++;
            }

            return count;
        }
    }
}
