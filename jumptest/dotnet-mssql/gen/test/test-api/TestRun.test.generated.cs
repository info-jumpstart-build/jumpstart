using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestRunTest : BaseTest
    {
        public static async Task testInsert()
        {
            var testrun = new TestRun();


                    testrun.name = Convert.ToString(BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random));
                    
                    testrun.test_plan_id = BaseTest.getLastId("TestPlan");
                    
                    testrun.run_at = Convert.ToDateTime(BaseTest.getTestData(testrun, "TIMESTAMP", TestDataType.random));
                    
                    testrun.run_by = Convert.ToString(BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random));
                    
                    testrun.notes = Convert.ToString(BaseTest.getTestData(testrun, "TEXT", TestDataType.random));
                    
                Console.WriteLine("Testing TestRun API insert: " + testrun.ToString());
                var createdTestRun = await PostAsyncReturn("TestRun", testrun);
                BaseTest.addLastId("TestRun", createdTestRun.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var testrun = new TestRun();


                        testrun.name = Convert.ToString(BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random));
                        
                        testrun.test_plan_id = BaseTest.getLastId("TestPlan");
                        
                Console.WriteLine("Testing TestRun API insert (RWK only): " + testrun.ToString());
                var createdTestRun = await PostAsyncReturn("TestRun", testrun);
                BaseTest.addLastId("TestRun", createdTestRun.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("TestRun");
            var testrunView = await GetByIdAsync<TestRunView>("TestRun", lastId);
            var testrun = new TestRun(testrunView);


                        testrun.name = (string) BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random);
                    
                            testrun.test_plan_id = BaseTest.getLastId("TestPlan");
                        
                        testrun.run_at = (DateTime) BaseTest.getTestData(testrun, "TIMESTAMP", TestDataType.random);
                    
                        testrun.run_by = (string) BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random);
                    
                        testrun.notes = (string) BaseTest.getTestData(testrun, "TEXT", TestDataType.random);
                    
                Console.WriteLine("Testing TestRun API update: " + testrun.ToString());
                await PutAsync("TestRun", lastId, testrun);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("TestRun");
            var testrunView = await GetByIdAsync<TestRunView>("TestRun", lastId);
            var testrun = new TestRun(testrunView);


                        testrun.name = (string) BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random);
                    
                            testrun.test_plan_id = BaseTest.getLastId("TestPlan");
                        
                        testrun.run_at = (DateTime) BaseTest.getTestData(testrun, "TIMESTAMP", TestDataType.random);
                    
                        testrun.run_by = (string) BaseTest.getTestData(testrun, "VARCHAR", TestDataType.random);
                    
                        testrun.notes = (string) BaseTest.getTestData(testrun, "TEXT", TestDataType.random);
                    
                Console.WriteLine("Testing TestRun API update: " + testrun.ToString());
                await PutAsync("TestRun", lastId, testrun);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing TestRun API select (list):");
            
            try
            {
                var testrunList = await BaseTest.GetListAsync<TestRun>("TestRun");
                
                Console.WriteLine($"Retrieved {testrunList.Count} TestRun records");
                
                if (testrunList.Count > 0)
                {
                    Console.WriteLine("First record: " + testrunList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed TestRun records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < testrunList.Count; i++)
                    {
                        var testrun = testrunList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {testrun.id}");

                        Console.WriteLine($"  name: {testrun.name}");
                                
                        Console.WriteLine($"  test_plan_id: {testrun.test_plan_id}");
                                
                        Console.WriteLine($"  run_at: {testrun.run_at}");
                                
                        Console.WriteLine($"  run_by: {testrun.run_by}");
                                
                        Console.WriteLine($"  notes: {testrun.notes}");
                                
                        Console.WriteLine($"  is_active: {testrun.is_active}");
                                
                        Console.WriteLine($"  created_by: {testrun.created_by}");
                                
                        Console.WriteLine($"  last_updated: {testrun.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {testrun.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {testrun.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", testrunList[0].id);
                }
                else
                {
                    Console.WriteLine("No TestRun records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing TestRun select: {ex.Message}");
                throw;
            }
        }
    }
}
