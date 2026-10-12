using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestResultTest : BaseTest
    {
        public static void testInsert()
        {
            var testresult = new TestResult();


                    testresult.test_run_id = (long) BaseTest.getLastId("testrun");
                    
                    testresult.test_case_id = (long) BaseTest.getLastId("testcase");
                    
                    testresult.test_result_status_id = (long) BaseTest.getLastId("testresultstatus");
                    
                    testresult.executed_at = Convert.ToDateTime(BaseTest.getTestData(testresult, "TIMESTAMP", TestDataType.random));
                    
<<<<<<< HEAD
                    testresult.executed_by = Convert.ToString(BaseTest.getTestData(testresult, "VARCHAR", TestDataType.random));
=======
                    testresult.executed_by_id = (long) BaseTest.getLastId("principal");
>>>>>>> main
                    
                    testresult.actual_result = Convert.ToString(BaseTest.getTestData(testresult, "TEXT", TestDataType.random));
                    
                    testresult.notes = Convert.ToString(BaseTest.getTestData(testresult, "TEXT", TestDataType.random));
                    
                Logger.Info("Testing TestResultLogic insert: " + testresult.ToString());
                TestResultLogic.Create().insert(testresult);
                BaseTest.addLastId("testresult", testresult.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("testresult");
            var testresultView  = TestResultLogic.Create().get(lastId);

            TestResult testresult = new TestResult(testresultView);

                            testresult.test_run_id = (long) BaseTest.getLastId("testrun");
                        
                            testresult.test_case_id = (long) BaseTest.getLastId("testcase");
                        
                            testresult.test_result_status_id = (long) BaseTest.getLastId("testresultstatus");
                        
                        testresult.executed_at = (DateTime) BaseTest.getTestData(testresult, "TIMESTAMP", TestDataType.random);
                    
<<<<<<< HEAD
                        testresult.executed_by = (string) BaseTest.getTestData(testresult, "VARCHAR", TestDataType.random);
                    
=======
                            testresult.executed_by_id = (long) BaseTest.getLastId("principal");
                        
>>>>>>> main
                        testresult.actual_result = (string) BaseTest.getTestData(testresult, "TEXT", TestDataType.random);
                    
                        testresult.notes = (string) BaseTest.getTestData(testresult, "TEXT", TestDataType.random);
                    
                Logger.Info("Testing TestResultLogic update: " + testresult.ToString());
                TestResultLogic.Create().update(lastId, testresult);
                    }
    }
}
