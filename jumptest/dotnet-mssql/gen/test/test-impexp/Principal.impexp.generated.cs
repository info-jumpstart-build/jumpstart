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
    /// Import/export round-trip test for core.principal.
    /// Steps:
    ///   1. Import  Principal.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  principal.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class PrincipalImpExpTest
    {
        private const string DataFileName    = "Principal.data.csv";
        private const string CompareFileName = "principal.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "first_name",
            "last_name",
            "username",
            "email",
<<<<<<< HEAD
            "enabled",
=======
>>>>>>> main
            "created_date",
            "last_login_date",
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
                Logger.Info($"Principal: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("Principal: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<Principal>("SELECT * FROM core.principal WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"Principal: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("Principal: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<Principal>("SELECT * FROM core.principal WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"Principal: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"Principal: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one Principal per row
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
                var principal = new Principal();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "first_name") >= 0)
                    principal.first_name = csv.GetField("first_name") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_name") >= 0)
                    principal.last_name = csv.GetField("last_name") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "username") >= 0)
                    principal.username = csv.GetField("username") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "email") >= 0)
                    principal.email = csv.GetField("email") ?? string.Empty;

<<<<<<< HEAD
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "enabled") >= 0)
                    principal.enabled = string.IsNullOrEmpty(csv.GetField("enabled")) ? default : Convert.ToInt32(csv.GetField("enabled"));

=======
>>>>>>> main
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "created_date") >= 0)
                    principal.created_date = string.IsNullOrEmpty(csv.GetField("created_date")) ? default : Convert.ToDateTime(csv.GetField("created_date"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_login_date") >= 0)
                    principal.last_login_date = string.IsNullOrEmpty(csv.GetField("last_login_date")) ? default : Convert.ToDateTime(csv.GetField("last_login_date"));

                PrincipalLogic.Unsafe().insert(principal);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<Principal> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("first_name");
            csv.WriteField("last_name");
            csv.WriteField("username");
            csv.WriteField("email");
<<<<<<< HEAD
            csv.WriteField("enabled");
=======
>>>>>>> main
            csv.WriteField("created_date");
            csv.WriteField("last_login_date");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.first_name);
                csv.WriteField(record.last_name);
                csv.WriteField(record.username);
                csv.WriteField(record.email);
<<<<<<< HEAD
                csv.WriteField(record.enabled);
=======
>>>>>>> main
                csv.WriteField(record.created_date);
                csv.WriteField(record.last_login_date);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, Principal record)
        {

            if (importRow.ContainsKey("first_name"))
            {
                string importVal = importRow["first_name"];
                string exportVal = BaseTestImpExp.ToStr(record.first_name);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'first_name' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("last_name"))
            {
                string importVal = importRow["last_name"];
                string exportVal = BaseTestImpExp.ToStr(record.last_name);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'last_name' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("username"))
            {
                string importVal = importRow["username"];
                string exportVal = BaseTestImpExp.ToStr(record.username);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'username' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("email"))
            {
                string importVal = importRow["email"];
                string exportVal = BaseTestImpExp.ToStr(record.email);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'email' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

<<<<<<< HEAD
            if (importRow.ContainsKey("enabled"))
            {
                string importVal = importRow["enabled"];
                string exportVal = BaseTestImpExp.ToStr(record.enabled);
                bool ok = BaseTestImpExp.CompareNumeric(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'enabled' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

=======
>>>>>>> main
            if (importRow.ContainsKey("created_date"))
            {
                string importVal = importRow["created_date"];
                string exportVal = BaseTestImpExp.ToStr(record.created_date);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'created_date' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("last_login_date"))
            {
                string importVal = importRow["last_login_date"];
                string exportVal = BaseTestImpExp.ToStr(record.last_login_date);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"Principal row {rowNum}: 'last_login_date' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
