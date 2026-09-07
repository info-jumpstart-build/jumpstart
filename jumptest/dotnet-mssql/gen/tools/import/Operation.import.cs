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
    /// Imports rows from a CSV file into core.operation.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class OperationImporter
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
                var operation = new Operation();


                // objectname  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "objectname") >= 0)
                    operation.objectname = csv.GetField("objectname") ?? string.Empty;

                // methodname  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "methodname") >= 0)
                    operation.methodname = csv.GetField("methodname") ?? string.Empty;

                OperationLogic.Create().insert(operation);
                count++;
            }

            return count;
        }
    }
}
