using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using jumptest;
using jumptest.core;

namespace jumptest
{
    public partial class SchedulerTest 
    {
        private static ServerNode schedulerNode;
        private static Dictionary<long, ServerNode> agents = new Dictionary<long, ServerNode>();
        private static List<Process> processes = new List<Process>();
        private static List<Workflow> workflows = new List<Workflow>();
       
       /// <summary>
        /// Gets the singleton Scheduler instance
        /// </summary>
        /// <returns>The ServerNode instance of the scheduler</returns>
        public static void InitSchedulerNode()
        {
             schedulerNode = null;

            void SchedulerSelectCallback(System.Data.Common.DbDataReader rdr)
                   {
                    schedulerNode = new ServerNode()   ;
                    DBPersist.autoAssign(rdr, schedulerNode);
                   }
            DBPersist.select(SchedulerSelectCallback, $"select * from core.server_node where server_node_type_id = {(long)ServerNode.ServerNodeType.Scheduler} and server_node_status_id = {(long)ServerNode.ServerNodeStatus.Online} and is_active=1");

       }

        /// <summary>
        /// Delete all existing test workflows
        /// </summary>
        public static async Task testDeleteExistingWorkflows()
        {
            try
            {
                Console.WriteLine("SchedulerTest: Deleting existing test workflows...");
                DBPersist.execCmd("DELETE FROM core.workflow WHERE name LIKE 'test-scheduler%'", "default");
                Console.WriteLine("SchedulerTest: Existing test workflows deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SchedulerTest: Error deleting existing workflows: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Load agents and processes for use in test workflows
        /// </summary>
        private static void LoadAgentsAndProcesses()
        {
            agents.Clear();
            processes.Clear();

            void AgentSelectCallback(System.Data.Common.DbDataReader rdr)
            {
                var agent = new ServerNode();
                DBPersist.autoAssign(rdr, agent);
                agents.Add(agent.id, agent);
            }
            DBPersist.select(AgentSelectCallback, $"SELECT * FROM core.server_node WHERE server_node_type_id = {(long)ServerNode.ServerNodeType.Agent} and server_node_status_id = {(long)ServerNode.ServerNodeStatus.Online} and is_active=1");

            if (agents.Count == 0)
            {
                throw new Exception("No online agents found. Please ensure at least one agent is online.");
            }

            void ProcessSelectCallback(System.Data.Common.DbDataReader rdr)
            {
                var process = new Process();
                DBPersist.autoAssign(rdr, process);
                processes.Add(process);
            }
            DBPersist.select(ProcessSelectCallback, "SELECT * FROM core.process WHERE script_id IN (SELECT id FROM core.script WHERE name LIKE 'test%' AND is_active=1)");

            if (processes.Count == 0)
            {
                throw new Exception("No test processes found. Please ensure test scripts exist.");
            }

            Console.WriteLine($"SchedulerTest: Loaded {agents.Count} agents and {processes.Count} processes");

            InitSchedulerNode();

            if (schedulerNode == null)
            {
                throw new Exception("Scheduler node not found. Please ensure the scheduler is configured correctly.");  
            }
        }

        /// <summary>
        /// Test 1: Create and execute a simple 1 job workflow
        /// </summary>
        public static async Task testSingleJobWorkflow()
        {
            try
            {
                Console.WriteLine("\n=== Test 1: Single Job Workflow ===");
                LoadAgentsAndProcesses();

                // Create parent workflow folder
                var parentWorkflow = new Workflow();
                parentWorkflow.name = "test-scheduler-single-job";
                parentWorkflow.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                parentWorkflow.last_start_time = Util.MinDateValue();
                parentWorkflow.last_end_time = Util.MinDateValue();

                parentWorkflow.is_active = 1;
                DBPersist.insert(parentWorkflow, "default");
                Console.WriteLine($"SchedulerTest: Created parent workflow folder {parentWorkflow.id} - {parentWorkflow.name}");

                // Create single child workflow (Process type)
                var childWorkflow = new Workflow();
                childWorkflow.name = "test-scheduler-single-job-process";
                childWorkflow.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow.parent_id = parentWorkflow.id;
                childWorkflow.process_id = processes[0].id;
                childWorkflow.server_node_id = agents.Values.First().id;
                childWorkflow.seq = 10;
                childWorkflow.last_start_time = Util.MinDateValue();
                childWorkflow.last_end_time = Util.MinDateValue();
                childWorkflow.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow.is_active = 1;
                DBPersist.insert(childWorkflow, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow.id} with sequence {childWorkflow.seq}");

                // Execute parent workflow
                var schedulerClient = new SchedulerClient(schedulerNode);
                await schedulerClient.ExecuteAsync(parentWorkflow.id);
                Console.WriteLine($"SchedulerTest: Parent workflow {parentWorkflow.id} queued for execution");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SchedulerTest: Error in testSingleJobWorkflow: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Test 2: Create and execute a 2 job workflow (sequential execution)
        /// </summary>
        public static async Task testTwoJobWorkflow()
        {
            try
            {
                Console.WriteLine("\n=== Test 2: Two Job Workflow (Sequential) ===");
                LoadAgentsAndProcesses();

                if (processes.Count < 2)
                {
                    throw new Exception("Need at least 2 processes for this test");
                }

                // Create parent workflow folder
                var parentWorkflow = new Workflow();
                parentWorkflow.name = "test-scheduler-two-job";
                parentWorkflow.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                parentWorkflow.is_active = 1;
                parentWorkflow.last_start_time = Util.MinDateValue();
                parentWorkflow.last_end_time = Util.MinDateValue();
            
                DBPersist.insert(parentWorkflow, "default");
                Console.WriteLine($"SchedulerTest: Created parent workflow folder {parentWorkflow.id} - {parentWorkflow.name}");

                // Create first child workflow (Process type, sequence 10)
                var childWorkflow1 = new Workflow();
                childWorkflow1.name = "test-scheduler-two-job-process1";
                childWorkflow1.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow1.parent_id = parentWorkflow.id;
                childWorkflow1.process_id = processes[0].id;
                childWorkflow1.server_node_id = agents.Values.First().id;
                childWorkflow1.seq = 10;
                childWorkflow1.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow1.is_active = 1;
                childWorkflow1.last_start_time = Util.MinDateValue();
                childWorkflow1.last_end_time = Util.MinDateValue();

                DBPersist.insert(childWorkflow1, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow1.id} with sequence {childWorkflow1.seq}");

                // Create second child workflow (Process type, sequence 20)
                var childWorkflow2 = new Workflow();
                childWorkflow2.name = "test-scheduler-two-job-process2";
                childWorkflow2.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow2.parent_id = parentWorkflow.id;
                childWorkflow2.process_id = processes[1].id;
                childWorkflow2.server_node_id = agents.Values.First().id;
                childWorkflow2.seq = 20;
                childWorkflow2.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow2.is_active = 1;
                childWorkflow2.last_start_time = Util.MinDateValue();
                childWorkflow2.last_end_time = Util.MinDateValue();
                DBPersist.insert(childWorkflow2, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow2.id} with sequence {childWorkflow2.seq}");

                // Execute parent workflow
                var schedulerClient = new SchedulerClient(schedulerNode);
                await schedulerClient.ExecuteAsync(parentWorkflow.id);
                Console.WriteLine($"SchedulerTest: Parent workflow {parentWorkflow.id} queued for execution");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SchedulerTest: Error in testTwoJobWorkflow: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Test 3: Create and execute a 4 job workflow with the two middle jobs running concurrently
        /// Sequence: 10 (job 1), 20 (job 2 - concurrent), 20 (job 3 - concurrent), 30 (job 4)
        /// </summary>
        public static async Task testFourJobWorkflowWithConcurrency()
        {
            try
            {
                Console.WriteLine("\n=== Test 3: Four Job Workflow with Concurrency ===");
                LoadAgentsAndProcesses();

                if (processes.Count < 4)
                {
                    throw new Exception("Need at least 4 processes for this test");
                }

                // Create parent workflow folder
                var parentWorkflow = new Workflow();
                parentWorkflow.name = "test-scheduler-four-job-concurrent";
                parentWorkflow.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                parentWorkflow.last_start_time = Util.MinDateValue();
                parentWorkflow.last_end_time = Util.MinDateValue();
                parentWorkflow.is_active = 1;
                DBPersist.insert(parentWorkflow, "default");
                Console.WriteLine($"SchedulerTest: Created parent workflow folder {parentWorkflow.id} - {parentWorkflow.name}");

                // Create first child workflow (Process type, sequence 10)
                var childWorkflow1 = new Workflow();
                childWorkflow1.name = "test-scheduler-four-job-process1";
                childWorkflow1.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow1.parent_id = parentWorkflow.id;
                childWorkflow1.process_id = processes[0].id;
                childWorkflow1.server_node_id = agents.Values.First().id;
                childWorkflow1.seq = 10;
                childWorkflow1.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow1.is_active = 1;
                childWorkflow1.last_start_time = Util.MinDateValue();
                childWorkflow1.last_end_time = Util.MinDateValue();
                DBPersist.insert(childWorkflow1, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow1.id} with sequence {childWorkflow1.seq}");

                // Create second child workflow (Process type, sequence 20 - concurrent)
                var childWorkflow2 = new Workflow();
                childWorkflow2.name = "test-scheduler-four-job-process2";
                childWorkflow2.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow2.parent_id = parentWorkflow.id;
                childWorkflow2.process_id = processes[1].id;
                childWorkflow2.server_node_id = agents.Values.First().id;
                childWorkflow2.seq = 20;
                childWorkflow2.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow2.is_active = 1;
                childWorkflow2.last_start_time = Util.MinDateValue();
                childWorkflow2.last_end_time = Util.MinDateValue();
                DBPersist.insert(childWorkflow2, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow2.id} with sequence {childWorkflow2.seq} (concurrent)");

                // Create third child workflow (Process type, sequence 20 - concurrent with process2)
                var childWorkflow3 = new Workflow();
                childWorkflow3.name = "test-scheduler-four-job-process3";
                childWorkflow3.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow3.parent_id = parentWorkflow.id;
                childWorkflow3.process_id = processes[2].id;
                childWorkflow3.server_node_id = agents.Values.First().id;
                childWorkflow3.seq = 20;
                childWorkflow3.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow3.is_active = 1;
                childWorkflow3.last_start_time = Util.MinDateValue();
                childWorkflow3.last_end_time = Util.MinDateValue();
                DBPersist.insert(childWorkflow3, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow3.id} with sequence {childWorkflow3.seq} (concurrent)");

                // Create fourth child workflow (Process type, sequence 30)
                var childWorkflow4 = new Workflow();
                childWorkflow4.name = "test-scheduler-four-job-process4";
                childWorkflow4.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childWorkflow4.parent_id = parentWorkflow.id;
                childWorkflow4.process_id = processes[3].id;
                childWorkflow4.server_node_id = agents.Values.First().id;
                childWorkflow4.seq = 30;
                childWorkflow4.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childWorkflow4.is_active = 1;
                childWorkflow4.last_start_time = Util.MinDateValue();
                childWorkflow4.last_end_time = Util.MinDateValue();
                DBPersist.insert(childWorkflow4, "default");
                Console.WriteLine($"SchedulerTest: Created child workflow {childWorkflow4.id} with sequence {childWorkflow4.seq}");

                // Execute parent workflow
                var schedulerClient = new SchedulerClient(schedulerNode);
                await schedulerClient.ExecuteAsync(parentWorkflow.id);
                Console.WriteLine($"SchedulerTest: Parent workflow {parentWorkflow.id} queued for execution");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SchedulerTest: Error in testFourJobWorkflowWithConcurrency: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Test 4: Create and execute a nested workflow (parent folder with child folder workflows)
        /// </summary>
        public static async Task testNestedWorkflow()
        {
            try
            {
                Console.WriteLine("\n=== Test 4: Nested Workflow ===");
                LoadAgentsAndProcesses();

                if (processes.Count < 2)
                {
                    throw new Exception("Need at least 2 processes for this test");
                }

                // Create parent workflow folder
                var parentWorkflow = new Workflow();
                parentWorkflow.name = "test-scheduler-nested-parent";
                parentWorkflow.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                parentWorkflow.is_active = 1;
                parentWorkflow.last_start_time = Util.MinDateValue();
                parentWorkflow.last_end_time = Util.MinDateValue();
                DBPersist.insert(parentWorkflow, "default");
                Console.WriteLine($"SchedulerTest: Created parent workflow folder {parentWorkflow.id} - {parentWorkflow.name}");

                // Create first child workflow folder
                var childFolder1 = new Workflow();
                childFolder1.name = "test-scheduler-nested-child-folder1";
                childFolder1.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                childFolder1.parent_id = parentWorkflow.id;
                childFolder1.is_active = 1;
                childFolder1.last_start_time = Util.MinDateValue();
                childFolder1.last_end_time = Util.MinDateValue();
                DBPersist.insert(childFolder1, "default");
                Console.WriteLine($"SchedulerTest: Created child folder {childFolder1.id} - {childFolder1.name}");

                // Create process workflow for first child folder
                var childProcess1 = new Workflow();
                childProcess1.name = "test-scheduler-nested-child-process1";
                childProcess1.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childProcess1.parent_id = childFolder1.id;
                childProcess1.process_id = processes[0].id;
                childProcess1.server_node_id = agents.Values.First().id;
                childProcess1.seq = 10;
                childProcess1.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childProcess1.is_active = 1;
                childProcess1.last_start_time = Util.MinDateValue();
                childProcess1.last_end_time = Util.MinDateValue();
                DBPersist.insert(childProcess1, "default");
                Console.WriteLine($"SchedulerTest: Created child process {childProcess1.id} for child folder {childFolder1.id}");

                // Create second child workflow folder
                var childFolder2 = new Workflow();
                childFolder2.name = "test-scheduler-nested-child-folder2";
                childFolder2.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                childFolder2.parent_id = parentWorkflow.id;
                childFolder2.is_active = 1;
                childFolder2.last_start_time = Util.MinDateValue();
                childFolder2.last_end_time = Util.MinDateValue();
                DBPersist.insert(childFolder2, "default");
                Console.WriteLine($"SchedulerTest: Created child folder {childFolder2.id} - {childFolder2.name}");

                // Create process workflow for second child folder
                var childProcess2 = new Workflow();
                childProcess2.name = "test-scheduler-nested-child-process2";
                childProcess2.workflow_type_id = (long)Workflow.WorkflowType.Process;
                childProcess2.parent_id = childFolder2.id;
                childProcess2.process_id = processes[1].id;
                childProcess2.server_node_id = agents.Values.First().id;
                childProcess2.seq = 10;
                childProcess2.on_failure_action_id = (long)Workflow.OnFailureAction.Stop;
                childProcess2.is_active = 1;
                childProcess2.last_start_time = Util.MinDateValue();
                childProcess2.last_end_time = Util.MinDateValue();
                DBPersist.insert(childProcess2, "default");
                Console.WriteLine($"SchedulerTest: Created child process {childProcess2.id} for child folder {childFolder2.id}");

                // Execute parent workflow (should recursively execute child folders and their processes)
                var schedulerClient = new SchedulerClient(schedulerNode);
                await schedulerClient.ExecuteAsync(parentWorkflow.id);
                Console.WriteLine($"SchedulerTest: Parent workflow {parentWorkflow.id} queued for execution (will recursively execute child folders and processes)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SchedulerTest: Error in testNestedWorkflow: {ex.Message}");
                throw;
            }
        }
    }
}

