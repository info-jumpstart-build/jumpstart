using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class TestCaseTest : BaseTest
    {
        public static async Task testInsert()
        {
            var testcase = new TestCase();


                    testcase.test_plan_id = BaseTest.getLastId("TestPlan");
                    
                    testcase.code = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.area = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.title = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.preconditions = Convert.ToString(BaseTest.getTestData(testcase, "TEXT", TestDataType.random));
                    
                    testcase.steps = Convert.ToString(BaseTest.getTestData(testcase, "TEXT", TestDataType.random));
                    
                    testcase.expected_result = Convert.ToString(BaseTest.getTestData(testcase, "TEXT", TestDataType.random));
                    
                    testcase.priority = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                    testcase.component = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing TestCase API insert: " + testcase.ToString());
                var createdTestCase = await PostAsyncReturn("TestCase", testcase);
                BaseTest.addLastId("TestCase", createdTestCase.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var testcase = new TestCase();


                        testcase.test_plan_id = BaseTest.getLastId("TestPlan");
                        
                        testcase.code = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                        
                        testcase.title = Convert.ToString(BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing TestCase API insert (RWK only): " + testcase.ToString());
                var createdTestCase = await PostAsyncReturn("TestCase", testcase);
                BaseTest.addLastId("TestCase", createdTestCase.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("TestCase");
            var testcaseView = await GetByIdAsync<TestCaseView>("TestCase", lastId);
            var testcase = new TestCase(testcaseView);


                            testcase.test_plan_id = BaseTest.getLastId("TestPlan");
                        
                        testcase.code = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.area = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.title = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.preconditions = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.steps = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.expected_result = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.priority = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.component = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing TestCase API update: " + testcase.ToString());
                await PutAsync("TestCase", lastId, testcase);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("TestCase");
            var testcaseView = await GetByIdAsync<TestCaseView>("TestCase", lastId);
            var testcase = new TestCase(testcaseView);


                            testcase.test_plan_id = BaseTest.getLastId("TestPlan");
                        
                        testcase.code = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.area = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.title = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.preconditions = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.steps = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.expected_result = (string) BaseTest.getTestData(testcase, "TEXT", TestDataType.random);
                    
                        testcase.priority = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                        testcase.component = (string) BaseTest.getTestData(testcase, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing TestCase API update: " + testcase.ToString());
                await PutAsync("TestCase", lastId, testcase);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing TestCase API select (list):");
            
            try
            {
                var testcaseList = await BaseTest.GetListAsync<TestCase>("TestCase");
                
                Console.WriteLine($"Retrieved {testcaseList.Count} TestCase records");
                
                if (testcaseList.Count > 0)
                {
                    Console.WriteLine("First record: " + testcaseList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed TestCase records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < testcaseList.Count; i++)
                    {
                        var testcase = testcaseList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {testcase.id}");

                        Console.WriteLine($"  test_plan_id: {testcase.test_plan_id}");
                                
                        Console.WriteLine($"  code: {testcase.code}");
                                
                        Console.WriteLine($"  area: {testcase.area}");
                                
                        Console.WriteLine($"  title: {testcase.title}");
                                
                        Console.WriteLine($"  preconditions: {testcase.preconditions}");
                                
                        Console.WriteLine($"  steps: {testcase.steps}");
                                
                        Console.WriteLine($"  expected_result: {testcase.expected_result}");
                                
                        Console.WriteLine($"  priority: {testcase.priority}");
                                
                        Console.WriteLine($"  component: {testcase.component}");
                                
                        Console.WriteLine($"  is_active: {testcase.is_active}");
                                
                        Console.WriteLine($"  created_by: {testcase.created_by}");
                                
                        Console.WriteLine($"  last_updated: {testcase.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {testcase.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {testcase.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", testcaseList[0].id);
                }
                else
                {
                    Console.WriteLine("No TestCase records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing TestCase select: {ex.Message}");
                throw;
            }
        }
    }
}
