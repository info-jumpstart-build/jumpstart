using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OnFailureTest : BaseTest
    {
        public static async Task testInsert()
        {
            var onfailure = new OnFailure();


                    onfailure.action = Convert.ToString(BaseTest.getTestData(onfailure, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing OnFailure API insert: " + onfailure.ToString());
                var createdOnFailure = await PostAsyncReturn("OnFailure", onfailure);
                BaseTest.addLastId("OnFailure", createdOnFailure.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var onfailure = new OnFailure();


                        onfailure.action = Convert.ToString(BaseTest.getTestData(onfailure, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing OnFailure API insert (RWK only): " + onfailure.ToString());
                var createdOnFailure = await PostAsyncReturn("OnFailure", onfailure);
                BaseTest.addLastId("OnFailure", createdOnFailure.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("OnFailure");
            var onfailureView = await GetByIdAsync<OnFailureView>("OnFailure", lastId);
            var onfailure = new OnFailure(onfailureView);


                        onfailure.action = (string) BaseTest.getTestData(onfailure, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing OnFailure API update: " + onfailure.ToString());
                await PutAsync("OnFailure", lastId, onfailure);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("OnFailure");
            var onfailureView = await GetByIdAsync<OnFailureView>("OnFailure", lastId);
            var onfailure = new OnFailure(onfailureView);


                        onfailure.action = (string) BaseTest.getTestData(onfailure, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing OnFailure API update: " + onfailure.ToString());
                await PutAsync("OnFailure", lastId, onfailure);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing OnFailure API select (list):");
            
            try
            {
                var onfailureList = await BaseTest.GetListAsync<OnFailure>("OnFailure");
                
                Console.WriteLine($"Retrieved {onfailureList.Count} OnFailure records");
                
                if (onfailureList.Count > 0)
                {
                    Console.WriteLine("First record: " + onfailureList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed OnFailure records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < onfailureList.Count; i++)
                    {
                        var onfailure = onfailureList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {onfailure.id}");

                        Console.WriteLine($"  action: {onfailure.action}");
                                
                        Console.WriteLine($"  is_active: {onfailure.is_active}");
                                
                        Console.WriteLine($"  created_by: {onfailure.created_by}");
                                
                        Console.WriteLine($"  last_updated: {onfailure.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {onfailure.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {onfailure.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", onfailureList[0].id);
                }
                else
                {
                    Console.WriteLine("No OnFailure records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing OnFailure select: {ex.Message}");
                throw;
            }
        }
    }
}
