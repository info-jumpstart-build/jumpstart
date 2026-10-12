using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronDomTest : BaseTest
    {
        public static async Task testInsert()
        {
            var crondom = new CronDom();


                    crondom.name = Convert.ToString(BaseTest.getTestData(crondom, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing CronDom API insert: " + crondom.ToString());
                var createdCronDom = await PostAsyncReturn("CronDom", crondom);
                BaseTest.addLastId("CronDom", createdCronDom.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var crondom = new CronDom();


                        crondom.name = Convert.ToString(BaseTest.getTestData(crondom, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing CronDom API insert (RWK only): " + crondom.ToString());
                var createdCronDom = await PostAsyncReturn("CronDom", crondom);
                BaseTest.addLastId("CronDom", createdCronDom.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("CronDom");
            var crondomView = await GetByIdAsync<CronDomView>("CronDom", lastId);
            var crondom = new CronDom(crondomView);


                        crondom.name = (string) BaseTest.getTestData(crondom, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronDom API update: " + crondom.ToString());
                await PutAsync("CronDom", lastId, crondom);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("CronDom");
            var crondomView = await GetByIdAsync<CronDomView>("CronDom", lastId);
            var crondom = new CronDom(crondomView);


                        crondom.name = (string) BaseTest.getTestData(crondom, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronDom API update: " + crondom.ToString());
                await PutAsync("CronDom", lastId, crondom);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing CronDom API select (list):");
            
            try
            {
                var crondomList = await BaseTest.GetListAsync<CronDom>("CronDom");
                
                Console.WriteLine($"Retrieved {crondomList.Count} CronDom records");
                
                if (crondomList.Count > 0)
                {
                    Console.WriteLine("First record: " + crondomList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed CronDom records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < crondomList.Count; i++)
                    {
                        var crondom = crondomList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {crondom.id}");

                        Console.WriteLine($"  name: {crondom.name}");
                                
                        Console.WriteLine($"  is_active: {crondom.is_active}");
                                
                        Console.WriteLine($"  created_by: {crondom.created_by}");
                                
                        Console.WriteLine($"  last_updated: {crondom.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {crondom.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {crondom.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", crondomList[0].id);
                }
                else
                {
                    Console.WriteLine("No CronDom records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing CronDom select: {ex.Message}");
                throw;
            }
        }
    }
}
