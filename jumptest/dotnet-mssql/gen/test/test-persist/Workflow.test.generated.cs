using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class WorkflowTest : BaseTest
    {
        public static void testInsert()
        {
            var workflow = new Workflow();


                    workflow.workflow_type_id = (long) BaseTest.getLastId("workflowtype");
                    
                    workflow.parent_id = (long) BaseTest.getLastId("workflow");
                    
                    workflow.name = Convert.ToString(BaseTest.getTestData(workflow, "VARCHAR", TestDataType.random));
                    
                    workflow.seq = Convert.ToInt32(BaseTest.getTestData(workflow, "INTEGER", TestDataType.random));
                    
                    workflow.server_node_id = (long) BaseTest.getLastId("servernode");
                    
                    workflow.process_id = (long) BaseTest.getLastId("process");
                    
                    workflow.exec_status_id = (long) BaseTest.getLastId("execstatus");
                    
                    workflow.last_start_time = Convert.ToDateTime(BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random));
                    
                    workflow.last_end_time = Convert.ToDateTime(BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random));
                    
                    workflow.schedule_id = (long) BaseTest.getLastId("schedule");
                    
                    workflow.on_failure_action_id = (long) BaseTest.getLastId("onfailure");
                    
                Logger.Info("Testing WorkflowLogic insert: " + workflow.ToString());
                WorkflowLogic.Create().insert(workflow);
                BaseTest.addLastId("workflow", workflow.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("workflow");
            var workflowView  = WorkflowLogic.Create().get(lastId);

            Workflow workflow = new Workflow(workflowView);

                            workflow.workflow_type_id = (long) BaseTest.getLastId("workflowtype");
                        
                            workflow.parent_id = (long) BaseTest.getLastId("workflow");
                        
                        workflow.name = (string) BaseTest.getTestData(workflow, "VARCHAR", TestDataType.random);
                    
                        workflow.seq = (int) BaseTest.getTestData(workflow, "INTEGER", TestDataType.random);
                    
                            workflow.server_node_id = (long) BaseTest.getLastId("servernode");
                        
                            workflow.process_id = (long) BaseTest.getLastId("process");
                        
                            workflow.exec_status_id = (long) BaseTest.getLastId("execstatus");
                        
                        workflow.last_start_time = (DateTime) BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random);
                    
                        workflow.last_end_time = (DateTime) BaseTest.getTestData(workflow, "TIMESTAMP", TestDataType.random);
                    
                            workflow.schedule_id = (long) BaseTest.getLastId("schedule");
                        
                            workflow.on_failure_action_id = (long) BaseTest.getLastId("onfailure");
                        
                Logger.Info("Testing WorkflowLogic update: " + workflow.ToString());
                WorkflowLogic.Create().update(lastId, workflow);
                    }
    }
}
