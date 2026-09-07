using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronMonthTest : BaseTest
    {
        public static async Task testInsert()
        {
            var cronmonth = new CronMonth();


                    cronmonth.name = Convert.ToString(BaseTest.getTestData(cronmonth, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing CronMonth API insert: " + cronmonth.ToString());
                var createdCronMonth = await PostAsyncReturn("CronMonth", cronmonth);
                BaseTest.addLastId("CronMonth", createdCronMonth.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var cronmonth = new CronMonth();


                        cronmonth.name = Convert.ToString(BaseTest.getTestData(cronmonth, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing CronMonth API insert (RWK only): " + cronmonth.ToString());
                var createdCronMonth = await PostAsyncReturn("CronMonth", cronmonth);
                BaseTest.addLastId("CronMonth", createdCronMonth.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("CronMonth");
            var cronmonthView = await GetByIdAsync<CronMonthView>("CronMonth", lastId);
            var cronmonth = new CronMonth(cronmonthView);


                        cronmonth.name = (string) BaseTest.getTestData(cronmonth, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronMonth API update: " + cronmonth.ToString());
                await PutAsync("CronMonth", lastId, cronmonth);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("CronMonth");
            var cronmonthView = await GetByIdAsync<CronMonthView>("CronMonth", lastId);
            var cronmonth = new CronMonth(cronmonthView);


                        cronmonth.name = (string) BaseTest.getTestData(cronmonth, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronMonth API update: " + cronmonth.ToString());
                await PutAsync("CronMonth", lastId, cronmonth);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing CronMonth API select (list):");
            
            try
            {
                var cronmonthList = await BaseTest.GetListAsync<CronMonth>("CronMonth");
                
                Console.WriteLine($"Retrieved {cronmonthList.Count} CronMonth records");
                
                if (cronmonthList.Count > 0)
                {
                    Console.WriteLine("First record: " + cronmonthList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed CronMonth records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < cronmonthList.Count; i++)
                    {
                        var cronmonth = cronmonthList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {cronmonth.id}");

                        Console.WriteLine($"  name: {cronmonth.name}");
                                
                        Console.WriteLine($"  is_active: {cronmonth.is_active}");
                                
                        Console.WriteLine($"  created_by: {cronmonth.created_by}");
                                
                        Console.WriteLine($"  last_updated: {cronmonth.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {cronmonth.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {cronmonth.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", cronmonthList[0].id);
                }
                else
                {
                    Console.WriteLine("No CronMonth records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing CronMonth select: {ex.Message}");
                throw;
            }
        }
    }
}
