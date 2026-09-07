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
    /// Import/export round-trip test for core.process.
    /// Steps:
    ///   1. Import  Process.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  process.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class ProcessImpExpTest
    {
        private const string DataFileName    = "Process.data.csv";
        private const string CompareFileName = "process.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "name",
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
                Logger.Info($"Process: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("Process: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<Process>("SELECT * FROM core.process WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"Process: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("Process: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<Process>("SELECT * FROM core.process WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"Process: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"Process: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one Process per row
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
                var process = new Process();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    process.name = csv.GetField("name") ?? string.Empty;

                ProcessLogic.Unsafe().insert(process);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<Process> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("name");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.name);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, Process record)
        {

            if (importRow.ContainsKey("name"))
            {
                string importVal = importRow["name"];
                string exportVal = BaseTestImpExp.ToStr(record.name);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Process row {rowNum}: 'name' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
