using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ServerNodeStatusTest : BaseTest
    {
        public static async Task testInsert()
        {
            var servernodestatus = new ServerNodeStatus();


                    servernodestatus.name = Convert.ToString(BaseTest.getTestData(servernodestatus, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing ServerNodeStatus API insert: " + servernodestatus.ToString());
                var createdServerNodeStatus = await PostAsyncReturn("ServerNodeStatus", servernodestatus);
                BaseTest.addLastId("ServerNodeStatus", createdServerNodeStatus.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var servernodestatus = new ServerNodeStatus();


                        servernodestatus.name = Convert.ToString(BaseTest.getTestData(servernodestatus, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing ServerNodeStatus API insert (RWK only): " + servernodestatus.ToString());
                var createdServerNodeStatus = await PostAsyncReturn("ServerNodeStatus", servernodestatus);
                BaseTest.addLastId("ServerNodeStatus", createdServerNodeStatus.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("ServerNodeStatus");
            var servernodestatusView = await GetByIdAsync<ServerNodeStatusView>("ServerNodeStatus", lastId);
            var servernodestatus = new ServerNodeStatus(servernodestatusView);


                        servernodestatus.name = (string) BaseTest.getTestData(servernodestatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ServerNodeStatus API update: " + servernodestatus.ToString());
                await PutAsync("ServerNodeStatus", lastId, servernodestatus);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("ServerNodeStatus");
            var servernodestatusView = await GetByIdAsync<ServerNodeStatusView>("ServerNodeStatus", lastId);
            var servernodestatus = new ServerNodeStatus(servernodestatusView);


                        servernodestatus.name = (string) BaseTest.getTestData(servernodestatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ServerNodeStatus API update: " + servernodestatus.ToString());
                await PutAsync("ServerNodeStatus", lastId, servernodestatus);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing ServerNodeStatus API select (list):");
            
            try
            {
                var servernodestatusList = await BaseTest.GetListAsync<ServerNodeStatus>("ServerNodeStatus");
                
                Console.WriteLine($"Retrieved {servernodestatusList.Count} ServerNodeStatus records");
                
                if (servernodestatusList.Count > 0)
                {
                    Console.WriteLine("First record: " + servernodestatusList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed ServerNodeStatus records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < servernodestatusList.Count; i++)
                    {
                        var servernodestatus = servernodestatusList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {servernodestatus.id}");

                        Console.WriteLine($"  name: {servernodestatus.name}");
                                
                        Console.WriteLine($"  is_active: {servernodestatus.is_active}");
                                
                        Console.WriteLine($"  created_by: {servernodestatus.created_by}");
                                
                        Console.WriteLine($"  last_updated: {servernodestatus.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {servernodestatus.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {servernodestatus.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", servernodestatusList[0].id);
                }
                else
                {
                    Console.WriteLine("No ServerNodeStatus records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing ServerNodeStatus select: {ex.Message}");
                throw;
            }
        }
    }
}
