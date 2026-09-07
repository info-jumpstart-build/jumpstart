using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ServerNodeStatusTest : BaseTest
    {
        public static void testInsert()
        {
            var servernodestatus = new ServerNodeStatus();


                    servernodestatus.name = Convert.ToString(BaseTest.getTestData(servernodestatus, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing ServerNodeStatusLogic insert: " + servernodestatus.ToString());
                ServerNodeStatusLogic.Create().insert(servernodestatus);
                BaseTest.addLastId("servernodestatus", servernodestatus.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("servernodestatus");
            var servernodestatusView  = ServerNodeStatusLogic.Create().get(lastId);

            ServerNodeStatus servernodestatus = new ServerNodeStatus(servernodestatusView);

                        servernodestatus.name = (string) BaseTest.getTestData(servernodestatus, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing ServerNodeStatusLogic update: " + servernodestatus.ToString());
                ServerNodeStatusLogic.Create().update(lastId, servernodestatus);
                    }
    }
}
