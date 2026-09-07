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
    /// Imports rows from a CSV file into core.nav_menu.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class NavMenuImporter
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
                var navmenu = new NavMenu();


                // parent_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "parent_id") >= 0)
                    navmenu.parent_id = string.IsNullOrEmpty(csv.GetField("parent_id")) ? default : Convert.ToInt64(csv.GetField("parent_id"));

                // ordinal  (INTEGER → int)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "ordinal") >= 0)
                    navmenu.ordinal = string.IsNullOrEmpty(csv.GetField("ordinal")) ? default : Convert.ToInt32(csv.GetField("ordinal"));

                // name  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "name") >= 0)
                    navmenu.name = csv.GetField("name") ?? string.Empty;

                // link  (VARCHAR → string)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "link") >= 0)
                    navmenu.link = csv.GetField("link") ?? string.Empty;

                NavMenuLogic.Create().insert(navmenu);
                count++;
            }

            return count;
        }
    }
}
