using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestPlanTest : BaseTest
    {
        public static async Task testInsert()
        {
            var testplan = new TestPlan();


                    testplan.name = Convert.ToString(BaseTest.getTestData(testplan, "VARCHAR", TestDataType.random));
                    
                    testplan.description = Convert.ToString(BaseTest.getTestData(testplan, "TEXT", TestDataType.random));
                    
                Console.WriteLine("Testing TestPlan API insert: " + testplan.ToString());
                var createdTestPlan = await PostAsyncReturn("TestPlan", testplan);
                BaseTest.addLastId("TestPlan", createdTestPlan.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var testplan = new TestPlan();


                        testplan.name = Convert.ToString(BaseTest.getTestData(testplan, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing TestPlan API insert (RWK only): " + testplan.ToString());
                var createdTestPlan = await PostAsyncReturn("TestPlan", testplan);
                BaseTest.addLastId("TestPlan", createdTestPlan.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("TestPlan");
            var testplanView = await GetByIdAsync<TestPlanView>("TestPlan", lastId);
            var testplan = new TestPlan(testplanView);


                        testplan.name = (string) BaseTest.getTestData(testplan, "VARCHAR", TestDataType.random);
                    
                        testplan.description = (string) BaseTest.getTestData(testplan, "TEXT", TestDataType.random);
                    
                Console.WriteLine("Testing TestPlan API update: " + testplan.ToString());
                await PutAsync("TestPlan", lastId, testplan);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("TestPlan");
            var testplanView = await GetByIdAsync<TestPlanView>("TestPlan", lastId);
            var testplan = new TestPlan(testplanView);


                        testplan.name = (string) BaseTest.getTestData(testplan, "VARCHAR", TestDataType.random);
                    
                        testplan.description = (string) BaseTest.getTestData(testplan, "TEXT", TestDataType.random);
                    
                Console.WriteLine("Testing TestPlan API update: " + testplan.ToString());
                await PutAsync("TestPlan", lastId, testplan);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing TestPlan API select (list):");
            
            try
            {
                var testplanList = await BaseTest.GetListAsync<TestPlan>("TestPlan");
                
                Console.WriteLine($"Retrieved {testplanList.Count} TestPlan records");
                
                if (testplanList.Count > 0)
                {
                    Console.WriteLine("First record: " + testplanList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed TestPlan records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < testplanList.Count; i++)
                    {
                        var testplan = testplanList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {testplan.id}");

                        Console.WriteLine($"  name: {testplan.name}");
                                
                        Console.WriteLine($"  description: {testplan.description}");
                                
                        Console.WriteLine($"  is_active: {testplan.is_active}");
                                
                        Console.WriteLine($"  created_by: {testplan.created_by}");
                                
                        Console.WriteLine($"  last_updated: {testplan.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {testplan.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {testplan.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", testplanList[0].id);
                }
                else
                {
                    Console.WriteLine("No TestPlan records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing TestPlan select: {ex.Message}");
                throw;
            }
        }
    }
}
