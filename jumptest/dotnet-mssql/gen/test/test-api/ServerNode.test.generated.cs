using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ServerNodeTest : BaseTest
    {
        public static async Task testInsert()
        {
            var servernode = new ServerNode();


                    servernode.server_node_type_id = BaseTest.getLastId("ServerNodeType");
                    
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
                    
                    servernode.server_node_status_id = BaseTest.getLastId("ServerNodeStatus");
                    
                Console.WriteLine("Testing ServerNode API insert: " + servernode.ToString());
                var createdServerNode = await PostAsyncReturn("ServerNode", servernode);
                BaseTest.addLastId("ServerNode", createdServerNode.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var servernode = new ServerNode();


                        servernode.hostname = Convert.ToString(BaseTest.getTestData(servernode, "VARCHAR", TestDataType.random));
                        
                        servernode.port = Convert.ToInt32(BaseTest.getTestData(servernode, "INTEGER", TestDataType.random));
                        
                Console.WriteLine("Testing ServerNode API insert (RWK only): " + servernode.ToString());
                var createdServerNode = await PostAsyncReturn("ServerNode", servernode);
                BaseTest.addLastId("ServerNode", createdServerNode.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("ServerNode");
            var servernodeView = await GetByIdAsync<ServerNodeView>("ServerNode", lastId);
            var servernode = new ServerNode(servernodeView);


                            servernode.server_node_type_id = BaseTest.getLastId("ServerNodeType");
                        
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
                    
                            servernode.server_node_status_id = BaseTest.getLastId("ServerNodeStatus");
                        
                Console.WriteLine("Testing ServerNode API update: " + servernode.ToString());
                await PutAsync("ServerNode", lastId, servernode);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("ServerNode");
            var servernodeView = await GetByIdAsync<ServerNodeView>("ServerNode", lastId);
            var servernode = new ServerNode(servernodeView);


                            servernode.server_node_type_id = BaseTest.getLastId("ServerNodeType");
                        
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
                    
                            servernode.server_node_status_id = BaseTest.getLastId("ServerNodeStatus");
                        
                Console.WriteLine("Testing ServerNode API update: " + servernode.ToString());
                await PutAsync("ServerNode", lastId, servernode);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing ServerNode API select (list):");
            
            try
            {
                var servernodeList = await BaseTest.GetListAsync<ServerNode>("ServerNode");
                
                Console.WriteLine($"Retrieved {servernodeList.Count} ServerNode records");
                
                if (servernodeList.Count > 0)
                {
                    Console.WriteLine("First record: " + servernodeList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed ServerNode records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < servernodeList.Count; i++)
                    {
                        var servernode = servernodeList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {servernode.id}");

                        Console.WriteLine($"  server_node_type_id: {servernode.server_node_type_id}");
                                
                        Console.WriteLine($"  hostname: {servernode.hostname}");
                                
                        Console.WriteLine($"  ip_address: {servernode.ip_address}");
                                
                        Console.WriteLine($"  port: {servernode.port}");
                                
                        Console.WriteLine($"  username: {servernode.username}");
                                
                        Console.WriteLine($"  url: {servernode.url}");
                                
                        Console.WriteLine($"  user_domain: {servernode.user_domain}");
                                
                        Console.WriteLine($"  os_name: {servernode.os_name}");
                                
                        Console.WriteLine($"  os_version: {servernode.os_version}");
                                
                        Console.WriteLine($"  architecture: {servernode.architecture}");
                                
                        Console.WriteLine($"  registered_at: {servernode.registered_at}");
                                
                        Console.WriteLine($"  server_node_status_id: {servernode.server_node_status_id}");
                                
                        Console.WriteLine($"  is_active: {servernode.is_active}");
                                
                        Console.WriteLine($"  created_by: {servernode.created_by}");
                                
                        Console.WriteLine($"  last_updated: {servernode.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {servernode.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {servernode.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", servernodeList[0].id);
                }
                else
                {
                    Console.WriteLine("No ServerNode records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing ServerNode select: {ex.Message}");
                throw;
            }
        }
    }
}
