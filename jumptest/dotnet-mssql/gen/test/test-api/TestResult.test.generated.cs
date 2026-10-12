using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestResultTest : BaseTest
    {
        public static async Task testInsert()
        {
            var testresult = new TestResult();


                    testresult.test_run_id = BaseTest.getLastId("TestRun");
                    
                    testresult.test_case_id = BaseTest.getLastId("TestCase");
                    
                    testresult.test_result_status_id = BaseTest.getLastId("TestResultStatus");
                    
                    testresult.executed_at = Convert.ToDateTime(BaseTest.getTestData(testresult, "TIMESTAMP", TestDataType.random));
                    
                    testresult.executed_by_id = BaseTest.getLastId("Principal");
                    
                    testresult.actual_result = Convert.ToString(BaseTest.getTestData(testresult, "TEXT", TestDataType.random));
                    
                    testresult.notes = Convert.ToString(BaseTest.getTestData(testresult, "TEXT", TestDataType.random));
                    
                Console.WriteLine("Testing TestResult API insert: " + testresult.ToString());
                var createdTestResult = await PostAsyncReturn("TestResult", testresult);
                BaseTest.addLastId("TestResult", createdTestResult.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var testresult = new TestResult();


                        testresult.test_run_id = BaseTest.getLastId("TestRun");
                        
                        testresult.test_case_id = BaseTest.getLastId("TestCase");
                        
                Console.WriteLine("Testing TestResult API insert (RWK only): " + testresult.ToString());
                var createdTestResult = await PostAsyncReturn("TestResult", testresult);
                BaseTest.addLastId("TestResult", createdTestResult.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("TestResult");
            var testresultView = await GetByIdAsync<TestResultView>("TestResult", lastId);
            var testresult = new TestResult(testresultView);


                            testresult.test_run_id = BaseTest.getLastId("TestRun");
                        
                            testresult.test_case_id = BaseTest.getLastId("TestCase");
                        
                            testresult.test_result_status_id = BaseTest.getLastId("TestResultStatus");
                        
                        testresult.executed_at = (DateTime) BaseTest.getTestData(testresult, "TIMESTAMP", TestDataType.random);
                    
                            testresult.executed_by_id = BaseTest.getLastId("Principal");
                        
                        testresult.actual_result = (string) BaseTest.getTestData(testresult, "TEXT", TestDataType.random);
                    
                        testresult.notes = (string) BaseTest.getTestData(testresult, "TEXT", TestDataType.random);
                    
                Console.WriteLine("Testing TestResult API update: " + testresult.ToString());
                await PutAsync("TestResult", lastId, testresult);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("TestResult");
            var testresultView = await GetByIdAsync<TestResultView>("TestResult", lastId);
            var testresult = new TestResult(testresultView);


                            testresult.test_run_id = BaseTest.getLastId("TestRun");
                        
                            testresult.test_case_id = BaseTest.getLastId("TestCase");
                        
                            testresult.test_result_status_id = BaseTest.getLastId("TestResultStatus");
                        
                        testresult.executed_at = (DateTime) BaseTest.getTestData(testresult, "TIMESTAMP", TestDataType.random);
                    
                            testresult.executed_by_id = BaseTest.getLastId("Principal");
                        
                        testresult.actual_result = (string) BaseTest.getTestData(testresult, "TEXT", TestDataType.random);
                    
                        testresult.notes = (string) BaseTest.getTestData(testresult, "TEXT", TestDataType.random);
                    
                Console.WriteLine("Testing TestResult API update: " + testresult.ToString());
                await PutAsync("TestResult", lastId, testresult);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing TestResult API select (list):");
            
            try
            {
                var testresultList = await BaseTest.GetListAsync<TestResult>("TestResult");
                
                Console.WriteLine($"Retrieved {testresultList.Count} TestResult records");
                
                if (testresultList.Count > 0)
                {
                    Console.WriteLine("First record: " + testresultList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed TestResult records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < testresultList.Count; i++)
                    {
                        var testresult = testresultList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {testresult.id}");

                        Console.WriteLine($"  test_run_id: {testresult.test_run_id}");
                                
                        Console.WriteLine($"  test_case_id: {testresult.test_case_id}");
                                
                        Console.WriteLine($"  test_result_status_id: {testresult.test_result_status_id}");
                                
                        Console.WriteLine($"  executed_at: {testresult.executed_at}");
                                
                        Console.WriteLine($"  executed_by_id: {testresult.executed_by_id}");
                                
                        Console.WriteLine($"  actual_result: {testresult.actual_result}");
                                
                        Console.WriteLine($"  notes: {testresult.notes}");
                                
                        Console.WriteLine($"  is_active: {testresult.is_active}");
                                
                        Console.WriteLine($"  created_by: {testresult.created_by}");
                                
                        Console.WriteLine($"  last_updated: {testresult.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {testresult.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {testresult.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", testresultList[0].id);
                }
                else
                {
                    Console.WriteLine("No TestResult records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing TestResult select: {ex.Message}");
                throw;
            }
        }
    }
}
