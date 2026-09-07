using System;
using System.Collections.Generic;
using jumptest;

namespace jumptest
{
    class Program
    {
        static void Main(string[] args)
        {
            string table = null;
            string input = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--table" && i + 1 < args.Length)
                    table = args[++i];
                else if (args[i] == "--input" && i + 1 < args.Length)
                    input = args[++i];
            }

            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(input))
            {
                PrintUsage();
                Environment.Exit(1);
            }

            if (!Importers.TryGetValue(table, out var importer))
            {
                Console.Error.WriteLine($"Error: Unknown table '{table}'");
                Console.Error.WriteLine();
                PrintUsage();
                Environment.Exit(1);
            }

            if (!System.IO.File.Exists(input))
            {
                Console.Error.WriteLine($"Error: Input file not found: {input}");
                Environment.Exit(1);
            }

            try
            {
                int count = importer(input);
                Console.WriteLine($"Imported {count} record(s) into '{table}' from {input}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error importing '{table}': {ex.Message}");
                if (ex.InnerException != null)
                    Console.Error.WriteLine($"  {ex.InnerException.Message}");
                Environment.Exit(1);
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: import --table <tablename> --input <filepath>");
            Console.WriteLine();
            Console.WriteLine("  --table    The table to import into (required)");
            Console.WriteLine("  --input    Input CSV file path (required)");
            Console.WriteLine();
            Console.WriteLine("Notes:");
            Console.WriteLine("  - The 'id' column is ignored; the database assigns new IDs.");
            Console.WriteLine("  - Global columns (is_active, created_by, etc.) are set to defaults.");
            Console.WriteLine("  - CSV must have a header row with column names matching the table.");
            Console.WriteLine();
            Console.WriteLine("Available tables:");
            foreach (var key in Importers.Keys)
                Console.WriteLine($"  {key}");
        }

        static readonly Dictionary<string, Func<string, int>> Importers = new()
        {
            { "testresultstatus", path => TestResultStatusImporter.Import(path) },
            { "testplan", path => TestPlanImporter.Import(path) },
            { "org", path => OrgImporter.Import(path) },
            { "operation", path => OperationImporter.Import(path) },
            { "oprole", path => OpRoleImporter.Import(path) },
            { "cronevery", path => CronEveryImporter.Import(path) },
            { "cronminute", path => CronMinuteImporter.Import(path) },
            { "cronhour", path => CronHourImporter.Import(path) },
            { "crondom", path => CronDomImporter.Import(path) },
            { "cronmonth", path => CronMonthImporter.Import(path) },
            { "crondow", path => CronDowImporter.Import(path) },
            { "navmenu", path => NavMenuImporter.Import(path) },
            { "datasource", path => DataSourceImporter.Import(path) },
            { "agentstatus", path => AgentStatusImporter.Import(path) },
            { "onfailure", path => OnFailureImporter.Import(path) },
            { "execstatus", path => ExecStatusImporter.Import(path) },
            { "servernodestatus", path => ServerNodeStatusImporter.Import(path) },
            { "scripttype", path => ScriptTypeImporter.Import(path) },
            { "servernodetype", path => ServerNodeTypeImporter.Import(path) },
            { "workflowtype", path => WorkflowTypeImporter.Import(path) },
            { "principalstatus", path => PrincipalStatusImporter.Import(path) },
            { "testcase", path => TestCaseImporter.Import(path) },
            { "testrun", path => TestRunImporter.Import(path) },
            { "oprolemap", path => OpRoleMapImporter.Import(path) },
            { "schedule", path => ScheduleImporter.Import(path) },
            { "sql", path => SqlImporter.Import(path) },
            { "script", path => ScriptImporter.Import(path) },
            { "servernode", path => ServerNodeImporter.Import(path) },
            { "principal", path => PrincipalImporter.Import(path) },
            { "eventservice", path => EventServiceImporter.Import(path) },
            { "process", path => ProcessImporter.Import(path) },
            { "testresult", path => TestResultImporter.Import(path) },
            { "principalorg", path => PrincipalOrgImporter.Import(path) },
            { "oprolemember", path => OpRoleMemberImporter.Import(path) },
            { "workflow", path => WorkflowImporter.Import(path) },
            { "execlog", path => ExecLogImporter.Import(path) },
        };
    }
}
