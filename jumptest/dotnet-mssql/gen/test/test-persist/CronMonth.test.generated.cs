using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronMonthTest : BaseTest
    {
        public static void testInsert()
        {
            var cronmonth = new CronMonth();


                    cronmonth.name = Convert.ToString(BaseTest.getTestData(cronmonth, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing CronMonthLogic insert: " + cronmonth.ToString());
                CronMonthLogic.Create().insert(cronmonth);
                BaseTest.addLastId("cronmonth", cronmonth.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("cronmonth");
            var cronmonthView  = CronMonthLogic.Create().get(lastId);

            CronMonth cronmonth = new CronMonth(cronmonthView);

                        cronmonth.name = (string) BaseTest.getTestData(cronmonth, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing CronMonthLogic update: " + cronmonth.ToString());
                CronMonthLogic.Create().update(lastId, cronmonth);
                    }
    }
}
