using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronHourTest : BaseTest
    {
        public static async Task testInsert()
        {
            var cronhour = new CronHour();


                    cronhour.name = Convert.ToString(BaseTest.getTestData(cronhour, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing CronHour API insert: " + cronhour.ToString());
                var createdCronHour = await PostAsyncReturn("CronHour", cronhour);
                BaseTest.addLastId("CronHour", createdCronHour.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var cronhour = new CronHour();


                        cronhour.name = Convert.ToString(BaseTest.getTestData(cronhour, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing CronHour API insert (RWK only): " + cronhour.ToString());
                var createdCronHour = await PostAsyncReturn("CronHour", cronhour);
                BaseTest.addLastId("CronHour", createdCronHour.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("CronHour");
            var cronhourView = await GetByIdAsync<CronHourView>("CronHour", lastId);
            var cronhour = new CronHour(cronhourView);


                        cronhour.name = (string) BaseTest.getTestData(cronhour, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronHour API update: " + cronhour.ToString());
                await PutAsync("CronHour", lastId, cronhour);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("CronHour");
            var cronhourView = await GetByIdAsync<CronHourView>("CronHour", lastId);
            var cronhour = new CronHour(cronhourView);


                        cronhour.name = (string) BaseTest.getTestData(cronhour, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronHour API update: " + cronhour.ToString());
                await PutAsync("CronHour", lastId, cronhour);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing CronHour API select (list):");
            
            try
            {
                var cronhourList = await BaseTest.GetListAsync<CronHour>("CronHour");
                
                Console.WriteLine($"Retrieved {cronhourList.Count} CronHour records");
                
                if (cronhourList.Count > 0)
                {
                    Console.WriteLine("First record: " + cronhourList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed CronHour records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < cronhourList.Count; i++)
                    {
                        var cronhour = cronhourList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {cronhour.id}");

                        Console.WriteLine($"  name: {cronhour.name}");
                                
                        Console.WriteLine($"  is_active: {cronhour.is_active}");
                                
                        Console.WriteLine($"  created_by: {cronhour.created_by}");
                                
                        Console.WriteLine($"  last_updated: {cronhour.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {cronhour.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {cronhour.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", cronhourList[0].id);
                }
                else
                {
                    Console.WriteLine("No CronHour records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing CronHour select: {ex.Message}");
                throw;
            }
        }
    }
}
