using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class PrincipalStatusTest : BaseTest
    {
        public static async Task testInsert()
        {
            var principalstatus = new PrincipalStatus();


                    principalstatus.name = Convert.ToString(BaseTest.getTestData(principalstatus, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing PrincipalStatus API insert: " + principalstatus.ToString());
                var createdPrincipalStatus = await PostAsyncReturn("PrincipalStatus", principalstatus);
                BaseTest.addLastId("PrincipalStatus", createdPrincipalStatus.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var principalstatus = new PrincipalStatus();


                        principalstatus.name = Convert.ToString(BaseTest.getTestData(principalstatus, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing PrincipalStatus API insert (RWK only): " + principalstatus.ToString());
                var createdPrincipalStatus = await PostAsyncReturn("PrincipalStatus", principalstatus);
                BaseTest.addLastId("PrincipalStatus", createdPrincipalStatus.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("PrincipalStatus");
            var principalstatusView = await GetByIdAsync<PrincipalStatusView>("PrincipalStatus", lastId);
            var principalstatus = new PrincipalStatus(principalstatusView);


                        principalstatus.name = (string) BaseTest.getTestData(principalstatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing PrincipalStatus API update: " + principalstatus.ToString());
                await PutAsync("PrincipalStatus", lastId, principalstatus);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("PrincipalStatus");
            var principalstatusView = await GetByIdAsync<PrincipalStatusView>("PrincipalStatus", lastId);
            var principalstatus = new PrincipalStatus(principalstatusView);


                        principalstatus.name = (string) BaseTest.getTestData(principalstatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing PrincipalStatus API update: " + principalstatus.ToString());
                await PutAsync("PrincipalStatus", lastId, principalstatus);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing PrincipalStatus API select (list):");
            
            try
            {
                var principalstatusList = await BaseTest.GetListAsync<PrincipalStatus>("PrincipalStatus");
                
                Console.WriteLine($"Retrieved {principalstatusList.Count} PrincipalStatus records");
                
                if (principalstatusList.Count > 0)
                {
                    Console.WriteLine("First record: " + principalstatusList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed PrincipalStatus records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < principalstatusList.Count; i++)
                    {
                        var principalstatus = principalstatusList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {principalstatus.id}");

                        Console.WriteLine($"  name: {principalstatus.name}");
                                
                        Console.WriteLine($"  is_active: {principalstatus.is_active}");
                                
                        Console.WriteLine($"  created_by: {principalstatus.created_by}");
                                
                        Console.WriteLine($"  last_updated: {principalstatus.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {principalstatus.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {principalstatus.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", principalstatusList[0].id);
                }
                else
                {
                    Console.WriteLine("No PrincipalStatus records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing PrincipalStatus select: {ex.Message}");
                throw;
            }
        }
    }
}
