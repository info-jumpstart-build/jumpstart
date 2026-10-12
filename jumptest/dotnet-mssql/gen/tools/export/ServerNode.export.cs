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
    /// Exports all rows from core.server_node to a CSV file.
    /// </summary>
    public static class ServerNodeExporter
    {
        public static int Export(string outputPath)
        {
            var records = ServerNodeLogic.Create().select<ServerNode>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);

            // Header row — column order matches the domain model attribute order
            csv.WriteField("id");
            csv.WriteField("server_node_type_id");
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
            csv.WriteField("server_node_status_id");
            csv.WriteField("is_active");
            csv.WriteField("created_by");
            csv.WriteField("last_updated");
            csv.WriteField("last_updated_by");
            csv.WriteField("txn_id");
            csv.NextRecord();

            // Data rows
            foreach (var record in records)
            {
                csv.WriteField(record.id);
                csv.WriteField(record.server_node_type_id);
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
                csv.WriteField(record.server_node_status_id);
                csv.WriteField(record.is_active);
                csv.WriteField(record.created_by);
                csv.WriteField(record.last_updated);
                csv.WriteField(record.last_updated_by);
                csv.WriteField(record.txn_id);
                csv.NextRecord();
            }

            return records.Count;
        }
    }
}
