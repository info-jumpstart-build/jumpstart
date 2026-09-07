using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ProcessTest : BaseTest
    {
        public static async Task testInsert()
        {
            var process = new Process();


                    process.name = Convert.ToString(BaseTest.getTestData(process, "VARCHAR", TestDataType.random));
                    
                    process.script_id = BaseTest.getLastId("Script");
                    
                Console.WriteLine("Testing Process API insert: " + process.ToString());
                var createdProcess = await PostAsyncReturn("Process", process);
                BaseTest.addLastId("Process", createdProcess.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var process = new Process();


                        process.name = Convert.ToString(BaseTest.getTestData(process, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing Process API insert (RWK only): " + process.ToString());
                var createdProcess = await PostAsyncReturn("Process", process);
                BaseTest.addLastId("Process", createdProcess.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Process");
            var processView = await GetByIdAsync<ProcessView>("Process", lastId);
            var process = new Process(processView);


                        process.name = (string) BaseTest.getTestData(process, "VARCHAR", TestDataType.random);
                    
                            process.script_id = BaseTest.getLastId("Script");
                        
                Console.WriteLine("Testing Process API update: " + process.ToString());
                await PutAsync("Process", lastId, process);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Process");
            var processView = await GetByIdAsync<ProcessView>("Process", lastId);
            var process = new Process(processView);


                        process.name = (string) BaseTest.getTestData(process, "VARCHAR", TestDataType.random);
                    
                            process.script_id = BaseTest.getLastId("Script");
                        
                Console.WriteLine("Testing Process API update: " + process.ToString());
                await PutAsync("Process", lastId, process);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Process API select (list):");
            
            try
            {
                var processList = await BaseTest.GetListAsync<Process>("Process");
                
                Console.WriteLine($"Retrieved {processList.Count} Process records");
                
                if (processList.Count > 0)
                {
                    Console.WriteLine("First record: " + processList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Process records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < processList.Count; i++)
                    {
                        var process = processList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {process.id}");

                        Console.WriteLine($"  name: {process.name}");
                                
                        Console.WriteLine($"  script_id: {process.script_id}");
                                
                        Console.WriteLine($"  is_active: {process.is_active}");
                                
                        Console.WriteLine($"  created_by: {process.created_by}");
                                
                        Console.WriteLine($"  last_updated: {process.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {process.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {process.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", processList[0].id);
                }
                else
                {
                    Console.WriteLine("No Process records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Process select: {ex.Message}");
                throw;
            }
        }
    }
}
