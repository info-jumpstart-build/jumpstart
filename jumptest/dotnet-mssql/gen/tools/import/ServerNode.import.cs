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
    /// Imports rows from a CSV file into core.server_node.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class ServerNodeImporter
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
                var servernode = new ServerNode();


                // server_node_type_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "server_node_type_id") >= 0)
                    servernode.server_node_type_id = string.IsNullOrEmpty(csv.GetField("server_node_type_id")) ? default : Convert.ToInt64(csv.GetField("server_node_type_id"));

                // hostname  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "hostname") >= 0)
                    servernode.hostname = csv.GetField("hostname") ?? string.Empty;

                // ip_address  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "ip_address") >= 0)
                    servernode.ip_address = csv.GetField("ip_address") ?? string.Empty;

                // port  (INTEGER → int)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "port") >= 0)
                    servernode.port = string.IsNullOrEmpty(csv.GetField("port")) ? default : Convert.ToInt32(csv.GetField("port"));

                // username  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "username") >= 0)
                    servernode.username = csv.GetField("username") ?? string.Empty;

                // url  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "url") >= 0)
                    servernode.url = csv.GetField("url") ?? string.Empty;

                // user_domain  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "user_domain") >= 0)
                    servernode.user_domain = csv.GetField("user_domain") ?? string.Empty;

                // os_name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "os_name") >= 0)
                    servernode.os_name = csv.GetField("os_name") ?? string.Empty;

                // os_version  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "os_version") >= 0)
                    servernode.os_version = csv.GetField("os_version") ?? string.Empty;

                // architecture  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "architecture") >= 0)
                    servernode.architecture = csv.GetField("architecture") ?? string.Empty;

                // registered_at  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "registered_at") >= 0)
                    servernode.registered_at = string.IsNullOrEmpty(csv.GetField("registered_at")) ? default : Convert.ToDateTime(csv.GetField("registered_at"));

                // server_node_status_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "server_node_status_id") >= 0)
                    servernode.server_node_status_id = string.IsNullOrEmpty(csv.GetField("server_node_status_id")) ? default : Convert.ToInt64(csv.GetField("server_node_status_id"));

                ServerNodeLogic.Create().insert(servernode);
                count++;
            }

            return count;
        }
    }
}
