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
    /// Imports rows from a CSV file into core.op_role_member.
    /// The 'id' column is skipped — the database assigns new IDs on insert.
    /// Global audit columns (is_active, created_by, last_updated, last_updated_by, txn_id)
    /// are not set from the CSV; the persist layer applies defaults.
    /// </summary>
    public static class OpRoleMemberImporter
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
                var oprolemember = new OpRoleMember();


                // principal_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "principal_id") >= 0)
                    oprolemember.principal_id = string.IsNullOrEmpty(csv.GetField("principal_id")) ? default : Convert.ToInt64(csv.GetField("principal_id"));

                // op_role_id  (BIGINT → long)
                if (csv.HeaderRecord != null && Array.IndexOf(csv.HeaderRecord, "op_role_id") >= 0)
                    oprolemember.op_role_id = string.IsNullOrEmpty(csv.GetField("op_role_id")) ? default : Convert.ToInt64(csv.GetField("op_role_id"));

                OpRoleMemberLogic.Create().insert(oprolemember);
                count++;
            }

            return count;
        }
    }
}
