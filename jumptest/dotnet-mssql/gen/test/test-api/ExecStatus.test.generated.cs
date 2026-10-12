using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ExecStatusTest : BaseTest
    {
        public static async Task testInsert()
        {
            var execstatus = new ExecStatus();


                    execstatus.name = Convert.ToString(BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random));
                    
                    execstatus.image = Convert.ToString(BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing ExecStatus API insert: " + execstatus.ToString());
                var createdExecStatus = await PostAsyncReturn("ExecStatus", execstatus);
                BaseTest.addLastId("ExecStatus", createdExecStatus.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var execstatus = new ExecStatus();


                        execstatus.name = Convert.ToString(BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random));
                        
                        execstatus.image = Convert.ToString(BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing ExecStatus API insert (RWK only): " + execstatus.ToString());
                var createdExecStatus = await PostAsyncReturn("ExecStatus", execstatus);
                BaseTest.addLastId("ExecStatus", createdExecStatus.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("ExecStatus");
            var execstatusView = await GetByIdAsync<ExecStatusView>("ExecStatus", lastId);
            var execstatus = new ExecStatus(execstatusView);


                        execstatus.name = (string) BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random);
                    
                        execstatus.image = (string) BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ExecStatus API update: " + execstatus.ToString());
                await PutAsync("ExecStatus", lastId, execstatus);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("ExecStatus");
            var execstatusView = await GetByIdAsync<ExecStatusView>("ExecStatus", lastId);
            var execstatus = new ExecStatus(execstatusView);


                        execstatus.name = (string) BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random);
                    
                        execstatus.image = (string) BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ExecStatus API update: " + execstatus.ToString());
                await PutAsync("ExecStatus", lastId, execstatus);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing ExecStatus API select (list):");
            
            try
            {
                var execstatusList = await BaseTest.GetListAsync<ExecStatus>("ExecStatus");
                
                Console.WriteLine($"Retrieved {execstatusList.Count} ExecStatus records");
                
                if (execstatusList.Count > 0)
                {
                    Console.WriteLine("First record: " + execstatusList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed ExecStatus records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < execstatusList.Count; i++)
                    {
                        var execstatus = execstatusList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {execstatus.id}");

                        Console.WriteLine($"  name: {execstatus.name}");
                                
                        Console.WriteLine($"  image: {execstatus.image}");
                                
                        Console.WriteLine($"  is_active: {execstatus.is_active}");
                                
                        Console.WriteLine($"  created_by: {execstatus.created_by}");
                                
                        Console.WriteLine($"  last_updated: {execstatus.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {execstatus.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {execstatus.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", execstatusList[0].id);
                }
                else
                {
                    Console.WriteLine("No ExecStatus records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing ExecStatus select: {ex.Message}");
                throw;
            }
        }
    }
}
