using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ServerNodeTypeTest : BaseTest
    {
        public static void testInsert()
        {
            var servernodetype = new ServerNodeType();


                    servernodetype.name = Convert.ToString(BaseTest.getTestData(servernodetype, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing ServerNodeTypeLogic insert: " + servernodetype.ToString());
                ServerNodeTypeLogic.Create().insert(servernodetype);
                BaseTest.addLastId("servernodetype", servernodetype.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("servernodetype");
            var servernodetypeView  = ServerNodeTypeLogic.Create().get(lastId);

            ServerNodeType servernodetype = new ServerNodeType(servernodetypeView);

                        servernodetype.name = (string) BaseTest.getTestData(servernodetype, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing ServerNodeTypeLogic update: " + servernodetype.ToString());
                ServerNodeTypeLogic.Create().update(lastId, servernodetype);
                    }
    }
}
