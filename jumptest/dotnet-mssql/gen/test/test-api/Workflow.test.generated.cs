using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class WorkflowTest : BaseTest
    {
        public static async Task testInsert()
        {
            var workflow = new Workflow();


                    workflow.workflow_type_id = BaseTest.getLastId("WorkflowType");
                    
                    workflow.parent_id = BaseTest.getLastId("Workflow");
                    
                    workflow.name = Convert.ToString(BaseTest.getTestData(workflow, "VARCHAR", TestDataType.random));
                    
                    workflow.seq = Convert.ToInt32(BaseTest.getTestData(workflow, "INTEGER", TestDataType.random));
                    
                    workflow.server_node_id = BaseTest.getLastId("ServerNode");
                    
                    workflow.process_id = BaseTest.getLastId("Process");
                    
                    workflow.exec_status_id = BaseTest.getLastId("ExecStatus");
                    
                    workflow.last_start_time = Convert.ToDateTime(BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random));
                    
                    workflow.last_end_time = Convert.ToDateTime(BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random));
                    
                    workflow.schedule_id = BaseTest.getLastId("Schedule");
                    
                    workflow.on_failure_action_id = BaseTest.getLastId("OnFailure");
                    
                Console.WriteLine("Testing Workflow API insert: " + workflow.ToString());
                var createdWorkflow = await PostAsyncReturn("Workflow", workflow);
                BaseTest.addLastId("Workflow", createdWorkflow.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var workflow = new Workflow();


                        workflow.parent_id = BaseTest.getLastId("Workflow");
                        
                        workflow.name = Convert.ToString(BaseTest.getTestData(workflow, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing Workflow API insert (RWK only): " + workflow.ToString());
                var createdWorkflow = await PostAsyncReturn("Workflow", workflow);
                BaseTest.addLastId("Workflow", createdWorkflow.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Workflow");
            var workflowView = await GetByIdAsync<WorkflowView>("Workflow", lastId);
            var workflow = new Workflow(workflowView);


                            workflow.workflow_type_id = BaseTest.getLastId("WorkflowType");
                        
                            workflow.parent_id = BaseTest.getLastId("Workflow");
                        
                        workflow.name = (string) BaseTest.getTestData(workflow, "VARCHAR", TestDataType.random);
                    
                        workflow.seq = (int) BaseTest.getTestData(workflow, "INTEGER", TestDataType.random);
                    
                            workflow.server_node_id = BaseTest.getLastId("ServerNode");
                        
                            workflow.process_id = BaseTest.getLastId("Process");
                        
                            workflow.exec_status_id = BaseTest.getLastId("ExecStatus");
                        
                        workflow.last_start_time = (DateTime) BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random);
                    
                        workflow.last_end_time = (DateTime) BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random);
                    
                            workflow.schedule_id = BaseTest.getLastId("Schedule");
                        
                            workflow.on_failure_action_id = BaseTest.getLastId("OnFailure");
                        
                Console.WriteLine("Testing Workflow API update: " + workflow.ToString());
                await PutAsync("Workflow", lastId, workflow);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Workflow");
            var workflowView = await GetByIdAsync<WorkflowView>("Workflow", lastId);
            var workflow = new Workflow(workflowView);


                            workflow.workflow_type_id = BaseTest.getLastId("WorkflowType");
                        
                            workflow.parent_id = BaseTest.getLastId("Workflow");
                        
                        workflow.name = (string) BaseTest.getTestData(workflow, "VARCHAR", TestDataType.random);
                    
                        workflow.seq = (int) BaseTest.getTestData(workflow, "INTEGER", TestDataType.random);
                    
                            workflow.server_node_id = BaseTest.getLastId("ServerNode");
                        
                            workflow.process_id = BaseTest.getLastId("Process");
                        
                            workflow.exec_status_id = BaseTest.getLastId("ExecStatus");
                        
                        workflow.last_start_time = (DateTime) BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random);
                    
                        workflow.last_end_time = (DateTime) BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random);
                    
                            workflow.schedule_id = BaseTest.getLastId("Schedule");
                        
                            workflow.on_failure_action_id = BaseTest.getLastId("OnFailure");
                        
                Console.WriteLine("Testing Workflow API update: " + workflow.ToString());
                await PutAsync("Workflow", lastId, workflow);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Workflow API select (list):");
            
            try
            {
                var workflowList = await BaseTest.GetListAsync<Workflow>("Workflow");
                
                Console.WriteLine($"Retrieved {workflowList.Count} Workflow records");
                
                if (workflowList.Count > 0)
                {
                    Console.WriteLine("First record: " + workflowList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Workflow records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < workflowList.Count; i++)
                    {
                        var workflow = workflowList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {workflow.id}");

                        Console.WriteLine($"  workflow_type_id: {workflow.workflow_type_id}");
                                
                        Console.WriteLine($"  parent_id: {workflow.parent_id}");
                                
                        Console.WriteLine($"  name: {workflow.name}");
                                
                        Console.WriteLine($"  seq: {workflow.seq}");
                                
                        Console.WriteLine($"  server_node_id: {workflow.server_node_id}");
                                
                        Console.WriteLine($"  process_id: {workflow.process_id}");
                                
                        Console.WriteLine($"  exec_status_id: {workflow.exec_status_id}");
                                
                        Console.WriteLine($"  last_start_time: {workflow.last_start_time}");
                                
                        Console.WriteLine($"  last_end_time: {workflow.last_end_time}");
                                
                        Console.WriteLine($"  schedule_id: {workflow.schedule_id}");
                                
                        Console.WriteLine($"  on_failure_action_id: {workflow.on_failure_action_id}");
                                
                        Console.WriteLine($"  is_active: {workflow.is_active}");
                                
                        Console.WriteLine($"  created_by: {workflow.created_by}");
                                
                        Console.WriteLine($"  last_updated: {workflow.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {workflow.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {workflow.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", workflowList[0].id);
                }
                else
                {
                    Console.WriteLine("No Workflow records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Workflow select: {ex.Message}");
                throw;
            }
        }
    }
}
