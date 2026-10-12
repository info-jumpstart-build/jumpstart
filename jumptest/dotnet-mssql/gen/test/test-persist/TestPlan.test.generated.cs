using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestPlanTest : BaseTest
    {
        public static void testInsert()
        {
            var testplan = new TestPlan();


                    testplan.name = Convert.ToString(BaseTest.getTestData(testplan, "VARCHAR", TestDataType.random));
                    
                    testplan.description = Convert.ToString(BaseTest.getTestData(testplan, "TEXT", TestDataType.random));
                    
                Logger.Info("Testing TestPlanLogic insert: " + testplan.ToString());
                TestPlanLogic.Create().insert(testplan);
                BaseTest.addLastId("testplan", testplan.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("testplan");
            var testplanView  = TestPlanLogic.Create().get(lastId);

            TestPlan testplan = new TestPlan(testplanView);

                        testplan.name = (string) BaseTest.getTestData(testplan, "VARCHAR", TestDataType.random);
                    
                        testplan.description = (string) BaseTest.getTestData(testplan, "TEXT", TestDataType.random);
                    
                Logger.Info("Testing TestPlanLogic update: " + testplan.ToString());
                TestPlanLogic.Create().update(lastId, testplan);
                    }
    }
}
