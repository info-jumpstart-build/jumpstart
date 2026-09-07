using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ServerNodeTest : BaseTest
    {
        public static void testInsert()
        {
            var servernode = new ServerNode();


                    servernode.server_node_type_id = (long) BaseTest.getLastId("servernodetype");
                    
                    servernode.hostname = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.ip_address = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.port = Convert.ToInt32(BaseTest.getTestData(servernode, "INTEGER", TestDataType.random));
                    
                    servernode.username = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.url = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.user_domain = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.os_name = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.os_version = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.architecture = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                    
                    servernode.registered_at = Convert.ToDateTime(BaseTest.getTestData(servernode, "TIMESTAMP", TestDataType.random));
                    
                    servernode.server_node_status_id = (long) BaseTest.getLastId("servernodestatus");
                    
                Logger.Info("Testing ServerNodeLogic insert: " + servernode.ToString());
                ServerNodeLogic.Create().insert(servernode);
                BaseTest.addLastId("servernode", servernode.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("servernode");
            var servernodeView  = ServerNodeLogic.Create().get(lastId);

            ServerNode servernode = new ServerNode(servernodeView);

                            servernode.server_node_type_id = (long) BaseTest.getLastId("servernodetype");
                        
                        servernode.hostname = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.ip_address = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.port = (int) BaseTest.getTestData(servernode, "INTEGER", TestDataType.random);
                    
                        servernode.username = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.url = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.user_domain = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.os_name = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.os_version = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.architecture = (string) BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random);
                    
                        servernode.registered_at = (DateTime) BaseTest.getTestData(servernode, "TIMESTAMP", TestDataType.random);
                    
                            servernode.server_node_status_id = (long) BaseTest.getLastId("servernodestatus");
                        
                Logger.Info("Testing ServerNodeLogic update: " + servernode.ToString());
                ServerNodeLogic.Create().update(lastId, servernode);
                    }
    }
}
