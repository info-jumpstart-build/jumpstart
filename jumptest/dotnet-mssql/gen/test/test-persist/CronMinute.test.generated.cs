using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronMinuteTest : BaseTest
    {
        public static void testInsert()
        {
            var cronminute = new CronMinute();


                    cronminute.name = Convert.ToString(BaseTest.getTestData(cronminute, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing CronMinuteLogic insert: " + cronminute.ToString());
                CronMinuteLogic.Create().insert(cronminute);
                BaseTest.addLastId("cronminute", cronminute.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("cronminute");
            var cronminuteView  = CronMinuteLogic.Create().get(lastId);

            CronMinute cronminute = new CronMinute(cronminuteView);

                        cronminute.name = (string) BaseTest.getTestData(cronminute, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing CronMinuteLogic update: " + cronminute.ToString());
                CronMinuteLogic.Create().update(lastId, cronminute);
                    }
    }
}
