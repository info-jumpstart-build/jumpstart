using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using jumptest;

namespace jumptest
{
    /// <summary>
    /// Import/export round-trip test for app.test_result.
    /// Steps:
    ///   1. Import  TestResult.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  testresult.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class TestResultImpExpTest
    {
        private const string DataFileName    = "TestResult.data.csv";
        private const string CompareFileName = "testresult.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "executed_at",
<<<<<<< HEAD
            "executed_by",
=======
>>>>>>> main
            "actual_result",
            "notes",
        };

        // -----------------------------------------------------------------------
        // Entry point
        // -----------------------------------------------------------------------

        public static void Run(string dataDir, string compareDir)
        {
            string importPath  = Path.Combine(dataDir,    DataFileName);
            string comparePath = Path.Combine(compareDir, CompareFileName);

            if (!File.Exists(importPath))
            {
                Logger.Info($"TestResult: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("TestResult: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<TestResult>("SELECT * FROM app.test_result WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"TestResult: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("TestResult: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<TestResult>("SELECT * FROM app.test_result WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"TestResult: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"TestResult: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one TestResult per row
        // -----------------------------------------------------------------------

        private static int ImportRows(string inputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord  = true,
                MissingFieldFound = null,
                BadDataFound     = null,
            };

            int count = 0;

            using var reader = new StreamReader(inputPath, System.Text.Encoding.UTF8);
            using var csv    = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var testresult = new TestResult();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "executed_at") >= 0)
                    testresult.executed_at = string.IsNullOrEmpty(csv.GetField("executed_at")) ? default : Convert.ToDateTime(csv.GetField("executed_at"));

<<<<<<< HEAD
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "executed_by") >= 0)
                    testresult.executed_by = csv.GetField("executed_by") ?? string.Empty;

=======
>>>>>>> main
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "actual_result") >= 0)
                    testresult.actual_result = csv.GetField("actual_result") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "notes") >= 0)
                    testresult.notes = csv.GetField("notes") ?? string.Empty;

                TestResultLogic.Unsafe().insert(testresult);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<TestResult> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("executed_at");
<<<<<<< HEAD
            csv.WriteField("executed_by");
=======
>>>>>>> main
            csv.WriteField("actual_result");
            csv.WriteField("notes");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.executed_at);
<<<<<<< HEAD
                csv.WriteField(record.executed_by);
=======
>>>>>>> main
                csv.WriteField(record.actual_result);
                csv.WriteField(record.notes);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, TestResult record)
        {

            if (importRow.ContainsKey("executed_at"))
            {
                string importVal = importRow["executed_at"];
                string exportVal = BaseTestImpExp.ToStr(record.executed_at);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestResult row {rowNum}: 'executed_at' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

<<<<<<< HEAD
            if (importRow.ContainsKey("executed_by"))
            {
                string importVal = importRow["executed_by"];
                string exportVal = BaseTestImpExp.ToStr(record.executed_by);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestResult row {rowNum}: 'executed_by' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

=======
>>>>>>> main
            if (importRow.ContainsKey("actual_result"))
            {
                string importVal = importRow["actual_result"];
                string exportVal = BaseTestImpExp.ToStr(record.actual_result);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestResult row {rowNum}: 'actual_result' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("notes"))
            {
                string importVal = importRow["notes"];
                string exportVal = BaseTestImpExp.ToStr(record.notes);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestResult row {rowNum}: 'notes' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
