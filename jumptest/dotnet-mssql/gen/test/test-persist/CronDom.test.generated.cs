using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronDomTest : BaseTest
    {
        public static void testInsert()
        {
            var crondom = new CronDom();


                    crondom.name = Convert.ToString(BaseTest.getTestData(crondom, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing CronDomLogic insert: " + crondom.ToString());
                CronDomLogic.Create().insert(crondom);
                BaseTest.addLastId("crondom", crondom.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("crondom");
            var crondomView  = CronDomLogic.Create().get(lastId);

            CronDom crondom = new CronDom(crondomView);

                        crondom.name = (string) BaseTest.getTestData(crondom, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing CronDomLogic update: " + crondom.ToString());
                CronDomLogic.Create().update(lastId, crondom);
                    }
    }
}
