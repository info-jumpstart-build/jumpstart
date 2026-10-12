using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronDowTest : BaseTest
    {
        public static void testInsert()
        {
            var crondow = new CronDow();


                    crondow.name = Convert.ToString(BaseTest.getTestData(crondow, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing CronDowLogic insert: " + crondow.ToString());
                CronDowLogic.Create().insert(crondow);
                BaseTest.addLastId("crondow", crondow.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("crondow");
            var crondowView  = CronDowLogic.Create().get(lastId);

            CronDow crondow = new CronDow(crondowView);

                        crondow.name = (string) BaseTest.getTestData(crondow, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing CronDowLogic update: " + crondow.ToString());
                CronDowLogic.Create().update(lastId, crondow);
                    }
    }
}
