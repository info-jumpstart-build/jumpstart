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
    /// Exports all rows from core.principal to a CSV file.
    /// </summary>
    public static class PrincipalExporter
    {
        public static int Export(string outputPath)
        {
            var records = PrincipalLogic.Create().select<Principal>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var writer = new StreamWriter(outputPath, append: false, encoding: System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);

            // Header row — column order matches the domain model attribute order
            csv.WriteField("id");
            csv.WriteField("first_name");
            csv.WriteField("last_name");
            csv.WriteField("username");
            csv.WriteField("email");
            csv.WriteField("principal_status_id");
            csv.WriteField("created_date");
            csv.WriteField("last_login_date");
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
                csv.WriteField(record.first_name);
                csv.WriteField(record.last_name);
                csv.WriteField(record.username);
                csv.WriteField(record.email);
                csv.WriteField(record.principal_status_id);
                csv.WriteField(record.created_date);
                csv.WriteField(record.last_login_date);
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
