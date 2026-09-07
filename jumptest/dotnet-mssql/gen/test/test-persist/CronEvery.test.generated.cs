using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronEveryTest : BaseTest
    {
        public static void testInsert()
        {
            var cronevery = new CronEvery();


                    cronevery.name = Convert.ToString(BaseTest.getTestData(cronevery, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing CronEveryLogic insert: " + cronevery.ToString());
                CronEveryLogic.Create().insert(cronevery);
                BaseTest.addLastId("cronevery", cronevery.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("cronevery");
            var croneveryView  = CronEveryLogic.Create().get(lastId);

            CronEvery cronevery = new CronEvery(croneveryView);

                        cronevery.name = (string) BaseTest.getTestData(cronevery, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing CronEveryLogic update: " + cronevery.ToString());
                CronEveryLogic.Create().update(lastId, cronevery);
                    }
    }
}
