using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestResultStatusTest : BaseTest
    {
        public static async Task testInsert()
        {
            var testresultstatus = new TestResultStatus();


                    testresultstatus.name = Convert.ToString(BaseTest.getTestData(testresultstatus, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing TestResultStatus API insert: " + testresultstatus.ToString());
                var createdTestResultStatus = await PostAsyncReturn("TestResultStatus", testresultstatus);
                BaseTest.addLastId("TestResultStatus", createdTestResultStatus.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var testresultstatus = new TestResultStatus();


                        testresultstatus.name = Convert.ToString(BaseTest.getTestData(testresultstatus, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing TestResultStatus API insert (RWK only): " + testresultstatus.ToString());
                var createdTestResultStatus = await PostAsyncReturn("TestResultStatus", testresultstatus);
                BaseTest.addLastId("TestResultStatus", createdTestResultStatus.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("TestResultStatus");
            var testresultstatusView = await GetByIdAsync<TestResultStatusView>("TestResultStatus", lastId);
            var testresultstatus = new TestResultStatus(testresultstatusView);


                        testresultstatus.name = (string) BaseTest.getTestData(testresultstatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing TestResultStatus API update: " + testresultstatus.ToString());
                await PutAsync("TestResultStatus", lastId, testresultstatus);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("TestResultStatus");
            var testresultstatusView = await GetByIdAsync<TestResultStatusView>("TestResultStatus", lastId);
            var testresultstatus = new TestResultStatus(testresultstatusView);


                        testresultstatus.name = (string) BaseTest.getTestData(testresultstatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing TestResultStatus API update: " + testresultstatus.ToString());
                await PutAsync("TestResultStatus", lastId, testresultstatus);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing TestResultStatus API select (list):");
            
            try
            {
                var testresultstatusList = await BaseTest.GetListAsync<TestResultStatus>("TestResultStatus");
                
                Console.WriteLine($"Retrieved {testresultstatusList.Count} TestResultStatus records");
                
                if (testresultstatusList.Count > 0)
                {
                    Console.WriteLine("First record: " + testresultstatusList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed TestResultStatus records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < testresultstatusList.Count; i++)
                    {
                        var testresultstatus = testresultstatusList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {testresultstatus.id}");

                        Console.WriteLine($"  name: {testresultstatus.name}");
                                
                        Console.WriteLine($"  is_active: {testresultstatus.is_active}");
                                
                        Console.WriteLine($"  created_by: {testresultstatus.created_by}");
                                
                        Console.WriteLine($"  last_updated: {testresultstatus.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {testresultstatus.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {testresultstatus.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", testresultstatusList[0].id);
                }
                else
                {
                    Console.WriteLine("No TestResultStatus records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing TestResultStatus select: {ex.Message}");
                throw;
            }
        }
    }
}
