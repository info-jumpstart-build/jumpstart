using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OperationTest : BaseTest
    {
        public static async Task testInsert()
        {
            var operation = new Operation();


                    operation.objectname = Convert.ToString(BaseTest.getTestData(operation, "VARCHAR", TestDataType.random));
                    
                    operation.methodname = Convert.ToString(BaseTest.getTestData(operation, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing Operation API insert: " + operation.ToString());
                var createdOperation = await PostAsyncReturn("Operation", operation);
                BaseTest.addLastId("Operation", createdOperation.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var operation = new Operation();


                        operation.objectname = Convert.ToString(BaseTest.getTestData(operation, "VARCHAR", TestDataType.random));
                        
                        operation.methodname = Convert.ToString(BaseTest.getTestData(operation, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing Operation API insert (RWK only): " + operation.ToString());
                var createdOperation = await PostAsyncReturn("Operation", operation);
                BaseTest.addLastId("Operation", createdOperation.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Operation");
            var operationView = await GetByIdAsync<OperationView>("Operation", lastId);
            var operation = new Operation(operationView);


                        operation.objectname = (string) BaseTest.getTestData(operation, "VARCHAR", TestDataType.random);
                    
                        operation.methodname = (string) BaseTest.getTestData(operation, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing Operation API update: " + operation.ToString());
                await PutAsync("Operation", lastId, operation);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Operation");
            var operationView = await GetByIdAsync<OperationView>("Operation", lastId);
            var operation = new Operation(operationView);


                        operation.objectname = (string) BaseTest.getTestData(operation, "VARCHAR", TestDataType.random);
                    
                        operation.methodname = (string) BaseTest.getTestData(operation, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing Operation API update: " + operation.ToString());
                await PutAsync("Operation", lastId, operation);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Operation API select (list):");
            
            try
            {
                var operationList = await BaseTest.GetListAsync<Operation>("Operation");
                
                Console.WriteLine($"Retrieved {operationList.Count} Operation records");
                
                if (operationList.Count > 0)
                {
                    Console.WriteLine("First record: " + operationList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Operation records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < operationList.Count; i++)
                    {
                        var operation = operationList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {operation.id}");

                        Console.WriteLine($"  objectname: {operation.objectname}");
                                
                        Console.WriteLine($"  methodname: {operation.methodname}");
                                
                        Console.WriteLine($"  is_active: {operation.is_active}");
                                
                        Console.WriteLine($"  created_by: {operation.created_by}");
                                
                        Console.WriteLine($"  last_updated: {operation.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {operation.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {operation.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", operationList[0].id);
                }
                else
                {
                    Console.WriteLine("No Operation records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Operation select: {ex.Message}");
                throw;
            }
        }
    }
}
