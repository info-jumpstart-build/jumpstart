using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace jumptest
{
    public class BaseTestImpExp
    {
        // -----------------------------------------------------------------------
        // CSV utilities shared by all per-object test classes
        // -----------------------------------------------------------------------

        /// <summary>
        /// Reads a CSV file and returns each data row as a column-name → raw-string dictionary.
        /// Returns an empty list when the file has no data rows (header-only or missing file).
        /// </summary>
        public static List<Dictionary<string, string>> ReadCsvRows(string path)
        {
            var result = new List<Dictionary<string, string>>();

            if (!File.Exists(path))
                return result;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null,
            };

            using var reader = new StreamReader(path, System.Text.Encoding.UTF8);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            if (csv.HeaderRecord == null)
                return result;

            while (csv.Read())
            {
                var row = new Dictionary<string, string>();
                foreach (var header in csv.HeaderRecord)
                    row[header] = csv.GetField(header) ?? string.Empty;
                result.Add(row);
            }

            return result;
        }

        // -----------------------------------------------------------------------
        // Comparison helpers — normalise values before comparing so that
        // formatting differences (decimal trailing zeros, date representations)
        // do not produce false failures.
        // -----------------------------------------------------------------------

        /// <summary>Safely converts any value to string; returns empty string for null.</summary>
        public static string ToStr(object? value) => value?.ToString() ?? string.Empty;

        public static bool CompareString(string import, string export)
            => import == export;

        public static bool CompareNumeric(string import, string export)
        {
            if (decimal.TryParse(import, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal a) &&
                decimal.TryParse(export, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal b))
                return a == b;
            return import == export;
        }

        public static bool CompareDateTime(string import, string export)
        {
            if (DateTime.TryParse(import, out DateTime a) && DateTime.TryParse(export, out DateTime b))
                return Math.Abs((a - b).TotalSeconds) < 2;
            return import == export;
        }

        public static bool CompareBool(string import, string export)
        {
            if (bool.TryParse(import, out bool a) && bool.TryParse(export, out bool b))
                return a == b;
            // Fall back to integer representation (0/1)
            if (int.TryParse(import, out int ai) && int.TryParse(export, out int bi))
                return (ai != 0) == (bi != 0);
            return string.Equals(import, export, StringComparison.OrdinalIgnoreCase);
        }

        // -----------------------------------------------------------------------
        // Summary tracking
        // -----------------------------------------------------------------------

        private static int _totalErrors = 0;
        private static int _totalTests  = 0;

        public static void RecordResult(bool passed) { _totalTests++; if (!passed) _totalErrors++; }

        public static void PrintSummary()
        {
            int passed = _totalTests - _totalErrors;
            Logger.Info($"=== Import/Export Test Summary: {passed}/{_totalTests} checks passed ===");
            if (_totalErrors > 0)
                Logger.Error($"=== {_totalErrors} check(s) FAILED ===");
        }
    }
}
