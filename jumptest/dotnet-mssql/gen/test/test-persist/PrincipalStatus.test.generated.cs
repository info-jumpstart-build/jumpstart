using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class PrincipalStatusTest : BaseTest
    {
        public static void testInsert()
        {
            var principalstatus = new PrincipalStatus();


                    principalstatus.name = Convert.ToString(BaseTest.getTestData(principalstatus, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing PrincipalStatusLogic insert: " + principalstatus.ToString());
                PrincipalStatusLogic.Create().insert(principalstatus);
                BaseTest.addLastId("principalstatus", principalstatus.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("principalstatus");
            var principalstatusView  = PrincipalStatusLogic.Create().get(lastId);

            PrincipalStatus principalstatus = new PrincipalStatus(principalstatusView);

                        principalstatus.name = (string) BaseTest.getTestData(principalstatus, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing PrincipalStatusLogic update: " + principalstatus.ToString());
                PrincipalStatusLogic.Create().update(lastId, principalstatus);
                    }
    }
}
