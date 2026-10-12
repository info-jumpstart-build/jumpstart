using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class WorkflowTypeTest : BaseTest
    {
        public static async Task testInsert()
        {
            var workflowtype = new WorkflowType();


                    workflowtype.name = Convert.ToString(BaseTest.getTestData(workflowtype, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing WorkflowType API insert: " + workflowtype.ToString());
                var createdWorkflowType = await PostAsyncReturn("WorkflowType", workflowtype);
                BaseTest.addLastId("WorkflowType", createdWorkflowType.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var workflowtype = new WorkflowType();


                        workflowtype.name = Convert.ToString(BaseTest.getTestData(workflowtype, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing WorkflowType API insert (RWK only): " + workflowtype.ToString());
                var createdWorkflowType = await PostAsyncReturn("WorkflowType", workflowtype);
                BaseTest.addLastId("WorkflowType", createdWorkflowType.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("WorkflowType");
            var workflowtypeView = await GetByIdAsync<WorkflowTypeView>("WorkflowType", lastId);
            var workflowtype = new WorkflowType(workflowtypeView);


                        workflowtype.name = (string) BaseTest.getTestData(workflowtype, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing WorkflowType API update: " + workflowtype.ToString());
                await PutAsync("WorkflowType", lastId, workflowtype);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("WorkflowType");
            var workflowtypeView = await GetByIdAsync<WorkflowTypeView>("WorkflowType", lastId);
            var workflowtype = new WorkflowType(workflowtypeView);


                        workflowtype.name = (string) BaseTest.getTestData(workflowtype, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing WorkflowType API update: " + workflowtype.ToString());
                await PutAsync("WorkflowType", lastId, workflowtype);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing WorkflowType API select (list):");
            
            try
            {
                var workflowtypeList = await BaseTest.GetListAsync<WorkflowType>("WorkflowType");
                
                Console.WriteLine($"Retrieved {workflowtypeList.Count} WorkflowType records");
                
                if (workflowtypeList.Count > 0)
                {
                    Console.WriteLine("First record: " + workflowtypeList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed WorkflowType records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < workflowtypeList.Count; i++)
                    {
                        var workflowtype = workflowtypeList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {workflowtype.id}");

                        Console.WriteLine($"  name: {workflowtype.name}");
                                
                        Console.WriteLine($"  is_active: {workflowtype.is_active}");
                                
                        Console.WriteLine($"  created_by: {workflowtype.created_by}");
                                
                        Console.WriteLine($"  last_updated: {workflowtype.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {workflowtype.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {workflowtype.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", workflowtypeList[0].id);
                }
                else
                {
                    Console.WriteLine("No WorkflowType records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing WorkflowType select: {ex.Message}");
                throw;
            }
        }
    }
}
