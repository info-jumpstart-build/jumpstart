using System;
using System.IO;
using jumptest;

namespace jumptest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Data CSV files are copied alongside the executable by the csproj Content item.
            string dataDir = AppDomain.CurrentDomain.BaseDirectory;

            // Compare output CSVs are written to the same directory.
            string compareDir = dataDir;

            Logger.Info("=== Import/Export Tests ===");
            Logger.Info($"Data directory: {dataDir}");

            try
            {

                Logger.Info("--- Testing TestResultStatus ---");
                TestResultStatusImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing TestPlan ---");
                TestPlanImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Org ---");
                OrgImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Operation ---");
                OperationImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing OpRole ---");
                OpRoleImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing CronEvery ---");
                CronEveryImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing CronMinute ---");
                CronMinuteImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing CronHour ---");
                CronHourImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing CronDom ---");
                CronDomImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing CronMonth ---");
                CronMonthImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing CronDow ---");
                CronDowImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing NavMenu ---");
                NavMenuImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing DataSource ---");
                DataSourceImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing AgentStatus ---");
                AgentStatusImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing OnFailure ---");
                OnFailureImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing ExecStatus ---");
                ExecStatusImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing ServerNodeStatus ---");
                ServerNodeStatusImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing ScriptType ---");
                ScriptTypeImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing ServerNodeType ---");
                ServerNodeTypeImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing WorkflowType ---");
                WorkflowTypeImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing PrincipalStatus ---");
                PrincipalStatusImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing TestCase ---");
                TestCaseImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing TestRun ---");
                TestRunImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing OpRoleMap ---");
                OpRoleMapImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Schedule ---");
                ScheduleImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Sql ---");
                SqlImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Script ---");
                ScriptImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing ServerNode ---");
                ServerNodeImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Principal ---");
                PrincipalImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing EventService ---");
                EventServiceImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Process ---");
                ProcessImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing TestResult ---");
                TestResultImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing PrincipalOrg ---");
                PrincipalOrgImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing OpRoleMember ---");
                OpRoleMemberImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing Workflow ---");
                WorkflowImpExpTest.Run(dataDir, compareDir);

                Logger.Info("--- Testing ExecLog ---");
                ExecLogImpExpTest.Run(dataDir, compareDir);
            }
            catch (Exception ex)
            {
                Logger.Error("Unhandled error during import/export tests: " + ex.Message);
                if (ex.InnerException != null)
                    Logger.Error("  Inner: " + ex.InnerException.Message);
            }

            BaseTestImpExp.PrintSummary();
        }
    }
}
