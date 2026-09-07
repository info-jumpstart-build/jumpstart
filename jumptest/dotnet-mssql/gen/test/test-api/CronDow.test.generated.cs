using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class CronDowTest : BaseTest
    {
        public static async Task testInsert()
        {
            var crondow = new CronDow();


                    crondow.name = Convert.ToString(BaseTest.getTestData(crondow, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing CronDow API insert: " + crondow.ToString());
                var createdCronDow = await PostAsyncReturn("CronDow", crondow);
                BaseTest.addLastId("CronDow", createdCronDow.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var crondow = new CronDow();


                        crondow.name = Convert.ToString(BaseTest.getTestData(crondow, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing CronDow API insert (RWK only): " + crondow.ToString());
                var createdCronDow = await PostAsyncReturn("CronDow", crondow);
                BaseTest.addLastId("CronDow", createdCronDow.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("CronDow");
            var crondowView = await GetByIdAsync<CronDowView>("CronDow", lastId);
            var crondow = new CronDow(crondowView);


                        crondow.name = (string) BaseTest.getTestData(crondow, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronDow API update: " + crondow.ToString());
                await PutAsync("CronDow", lastId, crondow);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("CronDow");
            var crondowView = await GetByIdAsync<CronDowView>("CronDow", lastId);
            var crondow = new CronDow(crondowView);


                        crondow.name = (string) BaseTest.getTestData(crondow, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing CronDow API update: " + crondow.ToString());
                await PutAsync("CronDow", lastId, crondow);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing CronDow API select (list):");
            
            try
            {
                var crondowList = await BaseTest.GetListAsync<CronDow>("CronDow");
                
                Console.WriteLine($"Retrieved {crondowList.Count} CronDow records");
                
                if (crondowList.Count > 0)
                {
                    Console.WriteLine("First record: " + crondowList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed CronDow records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < crondowList.Count; i++)
                    {
                        var crondow = crondowList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {crondow.id}");

                        Console.WriteLine($"  name: {crondow.name}");
                                
                        Console.WriteLine($"  is_active: {crondow.is_active}");
                                
                        Console.WriteLine($"  created_by: {crondow.created_by}");
                                
                        Console.WriteLine($"  last_updated: {crondow.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {crondow.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {crondow.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", crondowList[0].id);
                }
                else
                {
                    Console.WriteLine("No CronDow records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing CronDow select: {ex.Message}");
                throw;
            }
        }
    }
}
