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
    /// Import/export round-trip test for core.schedule.
    /// Steps:
    ///   1. Import  Schedule.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  schedule.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class ScheduleImpExpTest
    {
        private const string DataFileName    = "Schedule.data.csv";
        private const string CompareFileName = "schedule.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "name",
            "schedule_label",
            "next_run_time",
            "last_run_time",
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
                Logger.Info($"Schedule: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("Schedule: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<Schedule>("SELECT * FROM core.schedule WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"Schedule: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("Schedule: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<Schedule>("SELECT * FROM core.schedule WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"Schedule: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"Schedule: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one Schedule per row
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
                var schedule = new Schedule();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    schedule.name = csv.GetField("name") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "schedule_label") >= 0)
                    schedule.schedule_label = csv.GetField("schedule_label") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "next_run_time") >= 0)
                    schedule.next_run_time = string.IsNullOrEmpty(csv.GetField("next_run_time")) ? default : Convert.ToDateTime(csv.GetField("next_run_time"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_run_time") >= 0)
                    schedule.last_run_time = string.IsNullOrEmpty(csv.GetField("last_run_time")) ? default : Convert.ToDateTime(csv.GetField("last_run_time"));

                ScheduleLogic.Unsafe().insert(schedule);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<Schedule> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("name");
            csv.WriteField("schedule_label");
            csv.WriteField("next_run_time");
            csv.WriteField("last_run_time");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.name);
                csv.WriteField(record.schedule_label);
                csv.WriteField(record.next_run_time);
                csv.WriteField(record.last_run_time);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, Schedule record)
        {

            if (importRow.ContainsKey("name"))
            {
                string importVal = importRow["name"];
                string exportVal = BaseTestImpExp.ToStr(record.name);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Schedule row {rowNum}: 'name' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("schedule_label"))
            {
                string importVal = importRow["schedule_label"];
                string exportVal = BaseTestImpExp.ToStr(record.schedule_label);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Schedule row {rowNum}: 'schedule_label' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("next_run_time"))
            {
                string importVal = importRow["next_run_time"];
                string exportVal = BaseTestImpExp.ToStr(record.next_run_time);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Schedule row {rowNum}: 'next_run_time' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("last_run_time"))
            {
                string importVal = importRow["last_run_time"];
                string exportVal = BaseTestImpExp.ToStr(record.last_run_time);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Schedule row {rowNum}: 'last_run_time' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
