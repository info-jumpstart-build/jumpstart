using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class WorkflowTest 
    {
        private static Dictionary<long, ServerNode> agents = new Dictionary<long, ServerNode>();
        private static List<Process> processes = new List<Process>();
        private static List<Workflow> workflows = new List<Workflow>();

        

        public static async Task testInsertWorkflows()
        {
            try
            {
                Console.WriteLine("WorkflowProcessTest: Deleting existing test workflows...");
                DBPersist.execCmd("delete from core.workflow where name like 'test%'", "default");
                Console.WriteLine("WorkflowProcessTest: Existing test workflows deleted");

                   void AgentSelectCallback(System.Data.Common.DbDataReader rdr)
                   {
                    var agent = new ServerNode();
                    DBPersist.autoAssign(rdr, agent);
                    agents.Add(agent.id, agent);
                   }
                   DBPersist.select(AgentSelectCallback, $"select * from core.server_node where server_node_type_id = {(long)ServerNode.ServerNodeType.Agent} and server_node_status_id = {(long)ServerNode.ServerNodeStatus.Online} and is_active=1");

                   void ProcessSelectCallback(System.Data.Common.DbDataReader rdr)
                   {
                    var process = new Process();
                    DBPersist.autoAssign(rdr, process);
                    processes.Add(process);
                   }
                   DBPersist.select(ProcessSelectCallback, "select * from core.process where script_id in (select id from core.script where name like 'test%' and is_active=1)");

                    // Insert parent test workflow folder

                    var parentWorkflow = new Workflow();
                    parentWorkflow.name = "test workflow folder";
                    parentWorkflow.parent_id = 0;
                    parentWorkflow.is_active = 1;
                    parentWorkflow.workflow_type_id = (long)Workflow.WorkflowType.Folder;
                    DBPersist.insert(parentWorkflow);
                    Console.WriteLine($"WorkflowProcessTest: Created parent workflow folder {parentWorkflow.id} - {parentWorkflow.name}");

                    foreach (ServerNode  agent in agents.Values)
                    {
                        int seq = 10;
                        foreach (Process process in processes)
                        {
                            Script script = ScriptLogic.Create().get(process.script_id);
                            var workflow = new Workflow();
                            workflow.name = $"test {script.name} {seq}";
                            workflow.workflow_type_id = (long)Workflow.WorkflowType.Process;
                            workflow.is_active = 1;
                            workflow.server_node_id = agent.id;
                            workflow.process_id = process.id;
                            workflow.parent_id = parentWorkflow.id;
                            workflow.seq = seq;
                            workflow.last_start_time = Util.MinDateValue();
                            workflow.last_end_time = Util.MinDateValue();
                            workflow.on_failure_action_id = (long)Workflow.OnFailureAction.Continue;
                            seq+=10;
                            workflows.Add(workflow);
                        }
                    }

               

                foreach (var workflow in workflows)
                {
                    Console.WriteLine($"WorkflowProcessTest: Inserting workflow {workflow.id} - {workflow.name}");
                    DBPersist.insert(workflow);
                    Console.WriteLine($"WorkflowProcessTest: Workflow {workflow.id} - {workflow.name} inserted");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing insert workflow process: {ex.Message}");
                throw;
            }
        }

        public static async Task selectTestWorkflows()
        {
            try
            {
                agents.Clear();
                Console.WriteLine("WorkflowProcessTest: Selecting workflows...");
                workflows = DBPersist.select<Workflow>($"select * from core.workflow where name like 'test%' and workflow_type_id = {(long)Workflow.WorkflowType.Process} and is_active=1 order by seq");
                Console.WriteLine($"WorkflowProcessTest: Selected {workflows.Count} workflows");

                foreach (var workflow in workflows)
                {
                    if (!agents.ContainsKey(workflow.server_node_id))
                    {
                        ServerNode agent = DBPersist.get<ServerNode>(workflow.server_node_id);
                        agents.Add(agent.id, agent);
                    }
                }
            }
        
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing select workflows: {ex.Message}");
                throw;
            }
        }
    
        public static async Task testExecWorkflows()
        {
            ServerNode agent = null;
            foreach (var workflow in workflows)
            {
                try
                {
                    if (workflow.workflow_type_id != (long)Workflow.WorkflowType.Process) continue;
                    agent = agents[workflow.server_node_id];    
                    
                    ScriptAgentClient agentClient = new ScriptAgentClient(agent);
                    await agentClient.ExecuteAsync(workflow.id);
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    while (ex2 != null)
                    {
                        Console.WriteLine($"Error testing exec workflows: {ex2.Message}");
                        Console.WriteLine(ex2.StackTrace);
                        ex2 = ex2.InnerException;
                    }
                }
            }
        }
    }
}
