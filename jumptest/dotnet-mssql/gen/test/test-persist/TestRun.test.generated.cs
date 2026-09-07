using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestRunTest : BaseTest
    {
        public static void testInsert()
        {
            var testrun = new TestRun();


                    testrun.name = Convert.ToString(BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random));
                    
                    testrun.test_plan_id = (long) BaseTest.getLastId("testplan");
                    
                    testrun.run_at = Convert.ToDateTime(BaseTest.getTestData(testrun, "TIMESTAMP", TestDataType.random));
                    
                    testrun.run_by = Convert.ToString(BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random));
                    
                    testrun.notes = Convert.ToString(BaseTest.getTestData(testrun, "TEXT", TestDataType.random));
                    
                Logger.Info("Testing TestRunLogic insert: " + testrun.ToString());
                TestRunLogic.Create().insert(testrun);
                BaseTest.addLastId("testrun", testrun.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("testrun");
            var testrunView  = TestRunLogic.Create().get(lastId);

            TestRun testrun = new TestRun(testrunView);

                        testrun.name = (string) BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random);
                    
                            testrun.test_plan_id = (long) BaseTest.getLastId("testplan");
                        
                        testrun.run_at = (DateTime) BaseTest.getTestData(testrun, "TIMESTAMP", TestDataType.random);
                    
                        testrun.run_by = (string) BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random);
                    
                        testrun.notes = (string) BaseTest.getTestData(testrun, "TEXT", TestDataType.random);
                    
                Logger.Info("Testing TestRunLogic update: " + testrun.ToString());
                TestRunLogic.Create().update(lastId, testrun);
                    }
    }
}
