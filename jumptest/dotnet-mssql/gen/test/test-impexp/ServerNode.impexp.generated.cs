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
    /// Import/export round-trip test for core.server_node.
    /// Steps:
    ///   1. Import  ServerNode.data.csv  into the database.
    ///   2. Fetch only the rows just inserted (id > pre-import max).
    ///   3. Export those rows to  servernode.data.compare.csv.
    ///   4. Compare every testable column; call Logger.Error on any mismatch.
    /// Testable columns: all except id, global audit columns, and FK references.
    /// </summary>
    public static class ServerNodeImpExpTest
    {
        private const string DataFileName    = "ServerNode.data.csv";
        private const string CompareFileName = "servernode.data.compare.csv";

        // Generated list of columns that appear in the test data CSV.
        private static readonly string[] TestColumns = new string[]
        {
            "hostname",
            "ip_address",
            "port",
            "username",
            "url",
            "user_domain",
            "os_name",
            "os_version",
            "architecture",
            "registered_at",
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
                Logger.Info($"ServerNode: No data file at {importPath} — skipping.");
                return;
            }

            if (TestColumns.Length == 0)
            {
                Logger.Info("ServerNode: No testable columns (all are id/global/FK) — skipping.");
                return;
            }

            // Snapshot highest existing id so we can identify freshly inserted rows.
            // Use inline SQL — named queries require the core.sql table which may not exist
            // on a fresh/empty database used for import/export testing.
            var snapshot  = DBPersist.select<ServerNode>("SELECT * FROM core.server_node WHERE is_active = 1");
            long maxIdBefore = snapshot.Count > 0 ? snapshot.Max(r => r.id) : 0;

            // Step 1 — Import
            int imported = ImportRows(importPath);
            Logger.Info($"ServerNode: Inserted {imported} row(s).");

            if (imported == 0)
            {
                Logger.Error("ServerNode: Import produced 0 rows — aborting compare.");
                return;
            }

            // Step 2 — Isolate and export only the new rows
            var allRecords = DBPersist.select<ServerNode>("SELECT * FROM core.server_node WHERE is_active = 1");
            var newRecords = allRecords
                .Where(r => r.id > maxIdBefore)
                .OrderBy(r => r.id)
                .ToList();

            if (newRecords.Count != imported)
                Logger.Error($"ServerNode: Expected {imported} new record(s), found {newRecords.Count}.");

            ExportRows(newRecords, comparePath);
            Logger.Info($"ServerNode: Exported {newRecords.Count} row(s) to {comparePath}");

            // Step 3 — Compare
            var importRows = BaseTestImpExp.ReadCsvRows(importPath);
            int rowCount   = Math.Min(importRows.Count, newRecords.Count);

            for (int i = 0; i < rowCount; i++)
                CompareRow(i + 1, importRows[i], newRecords[i]);
        }

        // -----------------------------------------------------------------------
        // Import — read the data CSV and insert one ServerNode per row
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
                var servernode = new ServerNode();


                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "hostname") >= 0)
                    servernode.hostname = csv.GetField("hostname") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "ip_address") >= 0)
                    servernode.ip_address = csv.GetField("ip_address") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "port") >= 0)
                    servernode.port = string.IsNullOrEmpty(csv.GetField("port")) ? default : Convert.ToInt32(csv.GetField("port"));

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "username") >= 0)
                    servernode.username = csv.GetField("username") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "url") >= 0)
                    servernode.url = csv.GetField("url") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "user_domain") >= 0)
                    servernode.user_domain = csv.GetField("user_domain") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "os_name") >= 0)
                    servernode.os_name = csv.GetField("os_name") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "os_version") >= 0)
                    servernode.os_version = csv.GetField("os_version") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "architecture") >= 0)
                    servernode.architecture = csv.GetField("architecture") ?? string.Empty;

                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "registered_at") >= 0)
                    servernode.registered_at = string.IsNullOrEmpty(csv.GetField("registered_at")) ? default : Convert.ToDateTime(csv.GetField("registered_at"));

                ServerNodeLogic.Unsafe().insert(servernode);
                count++;
            }

            return count;
        }

        // -----------------------------------------------------------------------
        // Export — write TestColumns from the supplied records to a CSV file
        // -----------------------------------------------------------------------

        private static void ExportRows(List<ServerNode> records, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv    = new CsvWriter(writer, config);

            // Header
            csv.WriteField("hostname");
            csv.WriteField("ip_address");
            csv.WriteField("port");
            csv.WriteField("username");
            csv.WriteField("url");
            csv.WriteField("user_domain");
            csv.WriteField("os_name");
            csv.WriteField("os_version");
            csv.WriteField("architecture");
            csv.WriteField("registered_at");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.hostname);
                csv.WriteField(record.ip_address);
                csv.WriteField(record.port);
                csv.WriteField(record.username);
                csv.WriteField(record.url);
                csv.WriteField(record.user_domain);
                csv.WriteField(record.os_name);
                csv.WriteField(record.os_version);
                csv.WriteField(record.architecture);
                csv.WriteField(record.registered_at);
                csv.NextRecord();
            }
        }

        // -----------------------------------------------------------------------
        // Compare — field-by-field check with type-appropriate helpers
        // -----------------------------------------------------------------------

        private static void CompareRow(int rowNum, Dictionary<string, string> importRow, ServerNode record)
        {

            if (importRow.ContainsKey("hostname"))
            {
                string importVal = importRow["hostname"];
                string exportVal = BaseTestImpExp.ToStr(record.hostname);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'hostname' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("ip_address"))
            {
                string importVal = importRow["ip_address"];
                string exportVal = BaseTestImpExp.ToStr(record.ip_address);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'ip_address' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("port"))
            {
                string importVal = importRow["port"];
                string exportVal = BaseTestImpExp.ToStr(record.port);
                bool ok = BaseTestImpExp.CompareNumeric(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'port' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("username"))
            {
                string importVal = importRow["username"];
                string exportVal = BaseTestImpExp.ToStr(record.username);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'username' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("url"))
            {
                string importVal = importRow["url"];
                string exportVal = BaseTestImpExp.ToStr(record.url);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'url' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("user_domain"))
            {
                string importVal = importRow["user_domain"];
                string exportVal = BaseTestImpExp.ToStr(record.user_domain);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'user_domain' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("os_name"))
            {
                string importVal = importRow["os_name"];
                string exportVal = BaseTestImpExp.ToStr(record.os_name);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'os_name' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("os_version"))
            {
                string importVal = importRow["os_version"];
                string exportVal = BaseTestImpExp.ToStr(record.os_version);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'os_version' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("architecture"))
            {
                string importVal = importRow["architecture"];
                string exportVal = BaseTestImpExp.ToStr(record.architecture);
                bool ok = BaseTestImpExp.CompareString(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'architecture' mismatch — imported='{importVal}' exported='{exportVal}'");
            }

            if (importRow.ContainsKey("registered_at"))
            {
                string importVal = importRow["registered_at"];
                string exportVal = BaseTestImpExp.ToStr(record.registered_at);
                bool ok = BaseTestImpExp.CompareDateTime(importVal, exportVal);
                BaseTestImpExp.RecordResult(ok);
                if (!ok)
                    Logger.Error($"ServerNode row {rowNum}: 'registered_at' mismatch — imported='{importVal}' exported='{exportVal}'");
            }
        }
    }
}
