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
    /// Import/export round-trip test for core.exec_log.
    /// Steps:
    ///   1. Import  ExecLog.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  execlog.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class ExecLogImpExpTest
    {
        private const string DataFileName    = "ExecLog.data.csv";
        private const string CompareFileName = "execlog.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "token",
            "start_time",
            "end_time",
            "exec_status_id",
            "stdout",
            "stderr",
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
                Logger.Info($"ExecLog: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("ExecLog: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<ExecLog>("SELECT * FROM core.exec_log WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"ExecLog: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("ExecLog: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<ExecLog>("SELECT * FROM core.exec_log WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"ExecLog: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"ExecLog: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one ExecLog per row
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
                var execlog = new ExecLog();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "token") >= 0)
                    execlog.token = csv.GetField("token") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "start_time") >= 0)
                    execlog.start_time = string.IsNullOrEmpty(csv.GetField("start_time")) ? default : Convert.ToDateTime(csv.GetField("start_time"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "end_time") >= 0)
                    execlog.end_time = string.IsNullOrEmpty(csv.GetField("end_time")) ? default : Convert.ToDateTime(csv.GetField("end_time"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "exec_status_id") >= 0)
                    execlog.exec_status_id = string.IsNullOrEmpty(csv.GetField("exec_status_id")) ? default : Convert.ToInt64(csv.GetField("exec_status_id"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "stdout") >= 0)
                    execlog.stdout = csv.GetField("stdout") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "stderr") >= 0)
                    execlog.stderr = csv.GetField("stderr") ?? string.Empty;

                ExecLogLogic.Unsafe().insert(execlog);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<ExecLog> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("token");
            csv.WriteField("start_time");
            csv.WriteField("end_time");
            csv.WriteField("exec_status_id");
            csv.WriteField("stdout");
            csv.WriteField("stderr");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.token);
                csv.WriteField(record.start_time);
                csv.WriteField(record.end_time);
                csv.WriteField(record.exec_status_id);
                csv.WriteField(record.stdout);
                csv.WriteField(record.stderr);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, ExecLog record)
        {

            if (importRow.ContainsKey("token"))
            {
                string importVal = importRow["token"];
                string exportVal = BaseTestImpExp.ToStr(record.token);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ExecLog row {rowNum}: 'token' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("start_time"))
            {
                string importVal = importRow["start_time"];
                string exportVal = BaseTestImpExp.ToStr(record.start_time);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ExecLog row {rowNum}: 'start_time' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("end_time"))
            {
                string importVal = importRow["end_time"];
                string exportVal = BaseTestImpExp.ToStr(record.end_time);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ExecLog row {rowNum}: 'end_time' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("exec_status_id"))
            {
                string importVal = importRow["exec_status_id"];
                string exportVal = BaseTestImpExp.ToStr(record.exec_status_id);
                bool ok = BaseTestImpExp.CompareNumeric(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ExecLog row {rowNum}: 'exec_status_id' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("stdout"))
            {
                string importVal = importRow["stdout"];
                string exportVal = BaseTestImpExp.ToStr(record.stdout);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ExecLog row {rowNum}: 'stdout' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("stderr"))
            {
                string importVal = importRow["stderr"];
                string exportVal = BaseTestImpExp.ToStr(record.stderr);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ExecLog row {rowNum}: 'stderr' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
