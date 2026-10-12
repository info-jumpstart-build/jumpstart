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
    /// Import/export round-trip test for core.event_service.
    /// Steps:
    ///   1. Import  EventService.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  eventservice.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class EventServiceImpExpTest
    {
        private const string DataFileName    = "EventService.data.csv";
        private const string CompareFileName = "eventservice.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "event_type",
            "objectname_filter",
            "methodname_filter",
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
                Logger.Info($"EventService: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("EventService: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<EventService>("SELECT * FROM core.event_service WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"EventService: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("EventService: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<EventService>("SELECT * FROM core.event_service WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"EventService: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"EventService: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one EventService per row
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
                var eventservice = new EventService();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "event_type") >= 0)
                    eventservice.event_type = csv.GetField("event_type") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "objectname_filter") >= 0)
                    eventservice.objectname_filter = csv.GetField("objectname_filter") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "methodname_filter") >= 0)
                    eventservice.methodname_filter = csv.GetField("methodname_filter") ?? string.Empty;

                EventServiceLogic.Unsafe().insert(eventservice);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<EventService> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("event_type");
            csv.WriteField("objectname_filter");
            csv.WriteField("methodname_filter");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.event_type);
                csv.WriteField(record.objectname_filter);
                csv.WriteField(record.methodname_filter);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, EventService record)
        {

            if (importRow.ContainsKey("event_type"))
            {
                string importVal = importRow["event_type"];
                string exportVal = BaseTestImpExp.ToStr(record.event_type);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"EventService row {rowNum}: 'event_type' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("objectname_filter"))
            {
                string importVal = importRow["objectname_filter"];
                string exportVal = BaseTestImpExp.ToStr(record.objectname_filter);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"EventService row {rowNum}: 'objectname_filter' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("methodname_filter"))
            {
                string importVal = importRow["methodname_filter"];
                string exportVal = BaseTestImpExp.ToStr(record.methodname_filter);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"EventService row {rowNum}: 'methodname_filter' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
