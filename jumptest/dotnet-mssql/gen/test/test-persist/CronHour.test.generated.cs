using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronHourTest : BaseTest
    {
        public static void testInsert()
        {
            var cronhour = new CronHour();


                    cronhour.name = Convert.ToString(BaseTest.getTestData(cronhour, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing CronHourLogic insert: " + cronhour.ToString());
                CronHourLogic.Create().insert(cronhour);
                BaseTest.addLastId("cronhour", cronhour.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("cronhour");
            var cronhourView  = CronHourLogic.Create().get(lastId);

            CronHour cronhour = new CronHour(cronhourView);

                        cronhour.name = (string) BaseTest.getTestData(cronhour, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing CronHourLogic update: " + cronhour.ToString());
                CronHourLogic.Create().update(lastId, cronhour);
                    }
    }
}
