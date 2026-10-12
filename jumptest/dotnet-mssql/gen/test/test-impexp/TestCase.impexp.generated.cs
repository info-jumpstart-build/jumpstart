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
    /// Import/export round-trip test for app.test_case.
    /// Steps:
    ///   1. Import  TestCase.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  testcase.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class TestCaseImpExpTest
    {
        private const string DataFileName    = "TestCase.data.csv";
        private const string CompareFileName = "testcase.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "code",
            "area",
            "title",
            "preconditions",
            "steps",
            "expected_result",
            "priority",
            "component",
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
                Logger.Info($"TestCase: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("TestCase: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<TestCase>("SELECT * FROM app.test_case WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"TestCase: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("TestCase: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<TestCase>("SELECT * FROM app.test_case WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"TestCase: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"TestCase: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one TestCase per row
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
                var testcase = new TestCase();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "code") >= 0)
                    testcase.code = csv.GetField("code") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "area") >= 0)
                    testcase.area = csv.GetField("area") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "title") >= 0)
                    testcase.title = csv.GetField("title") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "preconditions") >= 0)
                    testcase.preconditions = csv.GetField("preconditions") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "steps") >= 0)
                    testcase.steps = csv.GetField("steps") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "expected_result") >= 0)
                    testcase.expected_result = csv.GetField("expected_result") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "priority") >= 0)
                    testcase.priority = csv.GetField("priority") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "component") >= 0)
                    testcase.component = csv.GetField("component") ?? string.Empty;

                TestCaseLogic.Unsafe().insert(testcase);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<TestCase> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("code");
            csv.WriteField("area");
            csv.WriteField("title");
            csv.WriteField("preconditions");
            csv.WriteField("steps");
            csv.WriteField("expected_result");
            csv.WriteField("priority");
            csv.WriteField("component");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.code);
                csv.WriteField(record.area);
                csv.WriteField(record.title);
                csv.WriteField(record.preconditions);
                csv.WriteField(record.steps);
                csv.WriteField(record.expected_result);
                csv.WriteField(record.priority);
                csv.WriteField(record.component);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, TestCase record)
        {

            if (importRow.ContainsKey("code"))
            {
                string importVal = importRow["code"];
                string exportVal = BaseTestImpExp.ToStr(record.code);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'code' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("area"))
            {
                string importVal = importRow["area"];
                string exportVal = BaseTestImpExp.ToStr(record.area);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'area' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("title"))
            {
                string importVal = importRow["title"];
                string exportVal = BaseTestImpExp.ToStr(record.title);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'title' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("preconditions"))
            {
                string importVal = importRow["preconditions"];
                string exportVal = BaseTestImpExp.ToStr(record.preconditions);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'preconditions' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("steps"))
            {
                string importVal = importRow["steps"];
                string exportVal = BaseTestImpExp.ToStr(record.steps);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'steps' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("expected_result"))
            {
                string importVal = importRow["expected_result"];
                string exportVal = BaseTestImpExp.ToStr(record.expected_result);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'expected_result' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("priority"))
            {
                string importVal = importRow["priority"];
                string exportVal = BaseTestImpExp.ToStr(record.priority);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'priority' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("component"))
            {
                string importVal = importRow["component"];
                string exportVal = BaseTestImpExp.ToStr(record.component);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"TestCase row {rowNum}: 'component' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
