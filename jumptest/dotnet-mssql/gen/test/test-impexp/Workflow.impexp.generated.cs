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
    /// Import/export round-trip test for core.workflow.
    /// Steps:
    ///   1. Import  Workflow.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  workflow.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class WorkflowImpExpTest
    {
        private const string DataFileName    = "Workflow.data.csv";
        private const string CompareFileName = "workflow.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "name",
            "seq",
            "last_start_time",
            "last_end_time",
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
                Logger.Info($"Workflow: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("Workflow: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<Workflow>("SELECT * FROM core.workflow WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"Workflow: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("Workflow: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<Workflow>("SELECT * FROM core.workflow WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"Workflow: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"Workflow: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one Workflow per row
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
                var workflow = new Workflow();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    workflow.name = csv.GetField("name") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "seq") >= 0)
                    workflow.seq = string.IsNullOrEmpty(csv.GetField("seq")) ? default : Convert.ToInt32(csv.GetField("seq"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_start_time") >= 0)
                    workflow.last_start_time = string.IsNullOrEmpty(csv.GetField("last_start_time")) ? default : Convert.ToDateTime(csv.GetField("last_start_time"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_end_time") >= 0)
                    workflow.last_end_time = string.IsNullOrEmpty(csv.GetField("last_end_time")) ? default : Convert.ToDateTime(csv.GetField("last_end_time"));

                WorkflowLogic.Unsafe().insert(workflow);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<Workflow> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("name");
            csv.WriteField("seq");
            csv.WriteField("last_start_time");
            csv.WriteField("last_end_time");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.name);
                csv.WriteField(record.seq);
                csv.WriteField(record.last_start_time);
                csv.WriteField(record.last_end_time);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, Workflow record)
        {

            if (importRow.ContainsKey("name"))
            {
                string importVal = importRow["name"];
                string exportVal = BaseTestImpExp.ToStr(record.name);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Workflow row {rowNum}: 'name' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("seq"))
            {
                string importVal = importRow["seq"];
                string exportVal = BaseTestImpExp.ToStr(record.seq);
                bool ok = BaseTestImpExp.CompareNumeric(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Workflow row {rowNum}: 'seq' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("last_start_time"))
            {
                string importVal = importRow["last_start_time"];
                string exportVal = BaseTestImpExp.ToStr(record.last_start_time);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Workflow row {rowNum}: 'last_start_time' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("last_end_time"))
            {
                string importVal = importRow["last_end_time"];
                string exportVal = BaseTestImpExp.ToStr(record.last_end_time);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Workflow row {rowNum}: 'last_end_time' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
