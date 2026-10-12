using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ServerNodeTypeTest : BaseTest
    {
        public static async Task testInsert()
        {
            var servernodetype = new ServerNodeType();


                    servernodetype.name = Convert.ToString(BaseTest.getTestData(servernodetype, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing ServerNodeType API insert: " + servernodetype.ToString());
                var createdServerNodeType = await PostAsyncReturn("ServerNodeType", servernodetype);
                BaseTest.addLastId("ServerNodeType", createdServerNodeType.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var servernodetype = new ServerNodeType();


                        servernodetype.name = Convert.ToString(BaseTest.getTestData(servernodetype, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing ServerNodeType API insert (RWK only): " + servernodetype.ToString());
                var createdServerNodeType = await PostAsyncReturn("ServerNodeType", servernodetype);
                BaseTest.addLastId("ServerNodeType", createdServerNodeType.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("ServerNodeType");
            var servernodetypeView = await GetByIdAsync<ServerNodeTypeView>("ServerNodeType", lastId);
            var servernodetype = new ServerNodeType(servernodetypeView);


                        servernodetype.name = (string) BaseTest.getTestData(servernodetype, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ServerNodeType API update: " + servernodetype.ToString());
                await PutAsync("ServerNodeType", lastId, servernodetype);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("ServerNodeType");
            var servernodetypeView = await GetByIdAsync<ServerNodeTypeView>("ServerNodeType", lastId);
            var servernodetype = new ServerNodeType(servernodetypeView);


                        servernodetype.name = (string) BaseTest.getTestData(servernodetype, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ServerNodeType API update: " + servernodetype.ToString());
                await PutAsync("ServerNodeType", lastId, servernodetype);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing ServerNodeType API select (list):");
            
            try
            {
                var servernodetypeList = await BaseTest.GetListAsync<ServerNodeType>("ServerNodeType");
                
                Console.WriteLine($"Retrieved {servernodetypeList.Count} ServerNodeType records");
                
                if (servernodetypeList.Count > 0)
                {
                    Console.WriteLine("First record: " + servernodetypeList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed ServerNodeType records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < servernodetypeList.Count; i++)
                    {
                        var servernodetype = servernodetypeList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {servernodetype.id}");

                        Console.WriteLine($"  name: {servernodetype.name}");
                                
                        Console.WriteLine($"  is_active: {servernodetype.is_active}");
                                
                        Console.WriteLine($"  created_by: {servernodetype.created_by}");
                                
                        Console.WriteLine($"  last_updated: {servernodetype.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {servernodetype.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {servernodetype.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", servernodetypeList[0].id);
                }
                else
                {
                    Console.WriteLine("No ServerNodeType records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing ServerNodeType select: {ex.Message}");
                throw;
            }
        }
    }
}
