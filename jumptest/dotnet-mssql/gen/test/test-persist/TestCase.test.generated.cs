using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestCaseTest : BaseTest
    {
        public static void testInsert()
        {
            var testcase = new TestCase();


                    testcase.test_plan_id = (long) BaseTest.getLastId("testplan");
                    
                    testcase.code = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.area = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.title = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.preconditions = Convert.ToString(BaseTest.getTestData(testcase, "TEXT", TestDataType.random));
                    
                    testcase.steps = Convert.ToString(BaseTest.getTestData(testcase, "TEXT", TestDataType.random));
                    
                    testcase.expected_result = Convert.ToString(BaseTest.getTestData(testcase, "TEXT", TestDataType.random));
                    
                    testcase.priority = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.component = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing TestCaseLogic insert: " + testcase.ToString());
                TestCaseLogic.Create().insert(testcase);
                BaseTest.addLastId("testcase", testcase.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("testcase");
            var testcaseView  = TestCaseLogic.Create().get(lastId);

            TestCase testcase = new TestCase(testcaseView);

                            testcase.test_plan_id = (long) BaseTest.getLastId("testplan");
                        
                        testcase.code = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.area = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.title = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.preconditions = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.steps = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.expected_result = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.priority = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.component = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing TestCaseLogic update: " + testcase.ToString());
                TestCaseLogic.Create().update(lastId, testcase);
                    }
    }
}
