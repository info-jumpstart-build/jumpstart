using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestResultStatusTest : BaseTest
    {
        public static void testInsert()
        {
            var testresultstatus = new TestResultStatus();


                    testresultstatus.name = Convert.ToString(BaseTest.getTestData(testresultstatus, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing TestResultStatusLogic insert: " + testresultstatus.ToString());
                TestResultStatusLogic.Create().insert(testresultstatus);
                BaseTest.addLastId("testresultstatus", testresultstatus.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("testresultstatus");
            var testresultstatusView  = TestResultStatusLogic.Create().get(lastId);

            TestResultStatus testresultstatus = new TestResultStatus(testresultstatusView);

                        testresultstatus.name = (string) BaseTest.getTestData(testresultstatus, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing TestResultStatusLogic update: " + testresultstatus.ToString());
                TestResultStatusLogic.Create().update(lastId, testresultstatus);
                    }
    }
}
