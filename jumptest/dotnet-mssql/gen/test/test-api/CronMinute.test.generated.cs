using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronMinuteTest : BaseTest
    {
        public static async Task testInsert()
        {
            var cronminute = new CronMinute();


                    cronminute.name = Convert.ToString(BaseTest.getTestData(cronminute, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing CronMinute API insert: " + cronminute.ToString());
                var createdCronMinute = await PostAsyncReturn("CronMinute", cronminute);
                BaseTest.addLastId("CronMinute", createdCronMinute.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var cronminute = new CronMinute();


                        cronminute.name = Convert.ToString(BaseTest.getTestData(cronminute, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing CronMinute API insert (RWK only): " + cronminute.ToString());
                var createdCronMinute = await PostAsyncReturn("CronMinute", cronminute);
                BaseTest.addLastId("CronMinute", createdCronMinute.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("CronMinute");
            var cronminuteView = await GetByIdAsync<CronMinuteView>("CronMinute", lastId);
            var cronminute = new CronMinute(cronminuteView);


                        cronminute.name = (string) BaseTest.getTestData(cronminute, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronMinute API update: " + cronminute.ToString());
                await PutAsync("CronMinute", lastId, cronminute);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("CronMinute");
            var cronminuteView = await GetByIdAsync<CronMinuteView>("CronMinute", lastId);
            var cronminute = new CronMinute(cronminuteView);


                        cronminute.name = (string) BaseTest.getTestData(cronminute, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronMinute API update: " + cronminute.ToString());
                await PutAsync("CronMinute", lastId, cronminute);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing CronMinute API select (list):");
            
            try
            {
                var cronminuteList = await BaseTest.GetListAsync<CronMinute>("CronMinute");
                
                Console.WriteLine($"Retrieved {cronminuteList.Count} CronMinute records");
                
                if (cronminuteList.Count > 0)
                {
                    Console.WriteLine("First record: " + cronminuteList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed CronMinute records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < cronminuteList.Count; i++)
                    {
                        var cronminute = cronminuteList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {cronminute.id}");

                        Console.WriteLine($"  name: {cronminute.name}");
                                
                        Console.WriteLine($"  is_active: {cronminute.is_active}");
                                
                        Console.WriteLine($"  created_by: {cronminute.created_by}");
                                
                        Console.WriteLine($"  last_updated: {cronminute.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {cronminute.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {cronminute.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", cronminuteList[0].id);
                }
                else
                {
                    Console.WriteLine("No CronMinute records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing CronMinute select: {ex.Message}");
                throw;
            }
        }
    }
}
