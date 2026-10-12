using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ExecLogTest : BaseTest
    {
        public static async Task testInsert()
        {
            var execlog = new ExecLog();


                    execlog.token = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                    
                    execlog.workflow_id = BaseTest.getLastId("Workflow");
                    
                    execlog.start_time = Convert.ToDateTime(BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random));
                    
                    execlog.end_time = Convert.ToDateTime(BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random));
                    
                    execlog.stdout = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                    
                    execlog.stderr = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing ExecLog API insert: " + execlog.ToString());
                var createdExecLog = await PostAsyncReturn("ExecLog", execlog);
                BaseTest.addLastId("ExecLog", createdExecLog.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var execlog = new ExecLog();


                        execlog.token = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                        
                        execlog.workflow_id = BaseTest.getLastId("Workflow");
                        
                        execlog.start_time = Convert.ToDateTime(BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random));
                        
                        execlog.end_time = Convert.ToDateTime(BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random));
                        
                Console.WriteLine("Testing ExecLog API insert (RWK only): " + execlog.ToString());
                var createdExecLog = await PostAsyncReturn("ExecLog", execlog);
                BaseTest.addLastId("ExecLog", createdExecLog.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("ExecLog");
            var execlogView = await GetByIdAsync<ExecLogView>("ExecLog", lastId);
            var execlog = new ExecLog(execlogView);


                        execlog.token = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                            execlog.workflow_id = BaseTest.getLastId("Workflow");
                        
                        execlog.start_time = (DateTime) BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random);
                    
                        execlog.end_time = (DateTime) BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random);
                    
                        execlog.stdout = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                        execlog.stderr = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ExecLog API update: " + execlog.ToString());
                await PutAsync("ExecLog", lastId, execlog);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("ExecLog");
            var execlogView = await GetByIdAsync<ExecLogView>("ExecLog", lastId);
            var execlog = new ExecLog(execlogView);


                        execlog.token = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                            execlog.workflow_id = BaseTest.getLastId("Workflow");
                        
                        execlog.start_time = (DateTime) BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random);
                    
                        execlog.end_time = (DateTime) BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random);
                    
                        execlog.stdout = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                        execlog.stderr = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ExecLog API update: " + execlog.ToString());
                await PutAsync("ExecLog", lastId, execlog);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing ExecLog API select (list):");
            
            try
            {
                var execlogList = await BaseTest.GetListAsync<ExecLog>("ExecLog");
                
                Console.WriteLine($"Retrieved {execlogList.Count} ExecLog records");
                
                if (execlogList.Count > 0)
                {
                    Console.WriteLine("First record: " + execlogList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed ExecLog records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < execlogList.Count; i++)
                    {
                        var execlog = execlogList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {execlog.id}");

                        Console.WriteLine($"  token: {execlog.token}");
                                
                        Console.WriteLine($"  workflow_id: {execlog.workflow_id}");
                                
                        Console.WriteLine($"  start_time: {execlog.start_time}");
                                
                        Console.WriteLine($"  end_time: {execlog.end_time}");
                                
                        Console.WriteLine($"  exec_status_id: {execlog.exec_status_id}");
                                
                        Console.WriteLine($"  stdout: {execlog.stdout}");
                                
                        Console.WriteLine($"  stderr: {execlog.stderr}");
                                
                        Console.WriteLine($"  is_active: {execlog.is_active}");
                                
                        Console.WriteLine($"  created_by: {execlog.created_by}");
                                
                        Console.WriteLine($"  last_updated: {execlog.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {execlog.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {execlog.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", execlogList[0].id);
                }
                else
                {
                    Console.WriteLine("No ExecLog records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing ExecLog select: {ex.Message}");
                throw;
            }
        }
    }
}
