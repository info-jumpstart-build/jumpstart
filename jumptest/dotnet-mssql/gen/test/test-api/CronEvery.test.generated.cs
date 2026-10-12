using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronEveryTest : BaseTest
    {
        public static async Task testInsert()
        {
            var cronevery = new CronEvery();


                    cronevery.name = Convert.ToString(BaseTest.getTestData(cronevery, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing CronEvery API insert: " + cronevery.ToString());
                var createdCronEvery = await PostAsyncReturn("CronEvery", cronevery);
                BaseTest.addLastId("CronEvery", createdCronEvery.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var cronevery = new CronEvery();


                        cronevery.name = Convert.ToString(BaseTest.getTestData(cronevery, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing CronEvery API insert (RWK only): " + cronevery.ToString());
                var createdCronEvery = await PostAsyncReturn("CronEvery", cronevery);
                BaseTest.addLastId("CronEvery", createdCronEvery.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("CronEvery");
            var croneveryView = await GetByIdAsync<CronEveryView>("CronEvery", lastId);
            var cronevery = new CronEvery(croneveryView);


                        cronevery.name = (string) BaseTest.getTestData(cronevery, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronEvery API update: " + cronevery.ToString());
                await PutAsync("CronEvery", lastId, cronevery);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("CronEvery");
            var croneveryView = await GetByIdAsync<CronEveryView>("CronEvery", lastId);
            var cronevery = new CronEvery(croneveryView);


                        cronevery.name = (string) BaseTest.getTestData(cronevery, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronEvery API update: " + cronevery.ToString());
                await PutAsync("CronEvery", lastId, cronevery);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing CronEvery API select (list):");
            
            try
            {
                var croneveryList = await BaseTest.GetListAsync<CronEvery>("CronEvery");
                
                Console.WriteLine($"Retrieved {croneveryList.Count} CronEvery records");
                
                if (croneveryList.Count > 0)
                {
                    Console.WriteLine("First record: " + croneveryList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed CronEvery records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < croneveryList.Count; i++)
                    {
                        var cronevery = croneveryList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {cronevery.id}");

                        Console.WriteLine($"  name: {cronevery.name}");
                                
                        Console.WriteLine($"  is_active: {cronevery.is_active}");
                                
                        Console.WriteLine($"  created_by: {cronevery.created_by}");
                                
                        Console.WriteLine($"  last_updated: {cronevery.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {cronevery.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {cronevery.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", croneveryList[0].id);
                }
                else
                {
                    Console.WriteLine("No CronEvery records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing CronEvery select: {ex.Message}");
                throw;
            }
        }
    }
}
