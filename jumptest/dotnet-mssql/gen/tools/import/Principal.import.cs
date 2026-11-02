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
    /// Imports rows from a CSV file into core.principal.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class PrincipalImporter
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
                var principal = new Principal();


                // first_name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "first_name") >= 0)
                    principal.first_name = csv.GetField("first_name") ?? string.Empty;

                // last_name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_name") >= 0)
                    principal.last_name = csv.GetField("last_name") ?? string.Empty;

                // username  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "username") >= 0)
                    principal.username = csv.GetField("username") ?? string.Empty;

                // email  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "email") >= 0)
                    principal.email = csv.GetField("email") ?? string.Empty;

                // principal_status_id  (INTEGER → int)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "principal_status_id") >= 0)
                    principal.principal_status_id = string.IsNullOrEmpty(csv.GetField("principal_status_id")) ? default : Convert.ToInt32(csv.GetField("principal_status_id"));

                // created_date  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "created_date") >= 0)
                    principal.created_date = string.IsNullOrEmpty(csv.GetField("created_date")) ? default : Convert.ToDateTime(csv.GetField("created_date"));

                // last_login_date  (TIMESTAMP → DateTime)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "last_login_date") >= 0)
                    principal.last_login_date = string.IsNullOrEmpty(csv.GetField("last_login_date")) ? default : Convert.ToDateTime(csv.GetField("last_login_date"));

                PrincipalLogic.Create().insert(principal);
                count++;
            }

            return count;
        }
    }
}
