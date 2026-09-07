using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest.core
{
    /// <summary>
    /// Interface for Scheduler logic operations
    /// </summary>
    public interface ISchedulerLogic
    {
        // Job Management Operations
       
        void execute(long workflowId);
        void cancel(long workflowId);
        void abort(long workflowId);

        // Job Status & Monitoring Operations
        object status(long workflowId);
       

        
     

        // System Management Operations
        long register(ServerNode serverNode);
        void unregister(long serverNodeId);
       
     
      
    }

    public partial class SchedulerLogic : BaseLogic, ISchedulerLogic
    {
        private static ServerNode _thisServerNode;
        private static readonly ConcurrentQueue<long> _workflowQueue = new ConcurrentQueue<long>();
        private static readonly ManualResetEventSlim _queueEvent = new ManualResetEventSlim(false);
        private static Thread _dispatcherThread;
        private static bool _dispatcherRunning = false;
        private static readonly object _dispatcherLock = new object();


        public static new ISchedulerLogic Create()
        {
            var schedulerLogic = new SchedulerLogic();

            var proxy = DispatchProxy.Create<ISchedulerLogic, Proxy<ISchedulerLogic>>();
            ((Proxy<ISchedulerLogic>)proxy).Initialize();
            ((Proxy<ISchedulerLogic>)proxy).Target = schedulerLogic;
            ((Proxy<ISchedulerLogic>)proxy).DomainObj = "Scheduler";
            
            return proxy;
        }


        public static ServerNode ThisServerNode
        {
            get { return _thisServerNode; }
            set { _thisServerNode = value; }
        }

        

        /// <summary>
        /// Sets the singleton Scheduler instance
        /// </summary>
        /// <param name="serverNode">The ServerNode instance to set</param>
        public static void SetThisServerNode(ServerNode serverNode)
        {
            _thisServerNode = serverNode;
            Console.WriteLine($"SchedulerLogic: Set singleton Scheduler instance (ID: {serverNode?.id})");
            StartDispatcherThread();
        }

        /// <summary>
        /// Starts the dispatcher thread if not already running
        /// </summary>
        private static void StartDispatcherThread()
        {
            lock (_dispatcherLock)
            {
                if (!_dispatcherRunning)
                {
                    _dispatcherRunning = true;
                    _dispatcherThread = new Thread(DispatcherThreadProc)
                    {
                        Name = "SchedulerDispatcher",
                        IsBackground = false
                    };
                    _dispatcherThread.Start();
                    Console.WriteLine("SchedulerLogic: Dispatcher thread started");
                }
            }
        }

        /// <summary>
        /// Dispatcher thread procedure that processes workflows from the queue
        /// </summary>
        private static void DispatcherThreadProc()
        {
            Console.WriteLine("SchedulerLogic: Dispatcher thread started");
            while (_dispatcherRunning)
            {
                try
                {
                    // Wait for workflow to be enqueued or timeout
                    if (_queueEvent.Wait(TimeSpan.FromSeconds(1)))
                    {
                        _queueEvent.Reset();
                    }

                    // Process all workflows in the queue
                    while (_workflowQueue.TryDequeue(out long workflowId))
                    {
                        try
                        {
                            Console.WriteLine($"SchedulerLogic: Processing workflow {workflowId}");
                            ExecuteWorkflowInternal(workflowId);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"SchedulerLogic: Error processing workflow {workflowId}: {ex.Message}", ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"SchedulerLogic: Error in dispatcher thread: {ex.Message}", ex);
                    Thread.Sleep(1000); // Wait before retrying
                }
            }
            Console.WriteLine("SchedulerLogic: Dispatcher thread stopped");
        }

        /// <summary>
        /// Registers a scheduler instance
        /// </summary>
        /// <param name="scheduler">The scheduler to register</param>
        /// <returns>The scheduler ID</returns>
        public long register(ServerNode serverNode)
        {
            Console.WriteLine($"SchedulerLogic: register - serverNode={serverNode}");
            ServerNodeLogic.Create().put(serverNode);
            SetThisServerNode(serverNode);
            return serverNode.id;
        }

        /// <summary>
        /// Sets the server node status to offline (typically called during shutdown or unregister)
        /// </summary>
        /// <param name="serverNodeId">The server node ID to update</param>
        public static void SetStatusOffline(long serverNodeId)
        {
            try
            {
                if (serverNodeId <= 0)
                {
                    Logger.Warn($"SchedulerLogic.SetStatusOffline: Invalid server node ID: {serverNodeId}");
                    return;
                }

                ServerNode serverNode = new ServerNode();
                serverNode.id = serverNodeId;
                DBPersist.get(serverNode, "default");
                
                if (serverNode != null && serverNode.id > 0)
                {
                    serverNode.server_node_status_id = (long)ServerNode.ServerNodeStatus.Offline;
                    ServerNodeLogic.Create().update(serverNode.id, serverNode);
                    Logger.Info($"SchedulerLogic.SetStatusOffline: Set server node {serverNodeId} status to Offline");
                }
                else
                {
                    Logger.Warn($"SchedulerLogic.SetStatusOffline: Server node {serverNodeId} not found");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"SchedulerLogic.SetStatusOffline: Error setting status to Offline for server node {serverNodeId}: {ex.Message}", ex);
            }
        }

     
        // =====================================
        // Job Management Operations
        // =====================================

        public void execute(long workflowId)
        {
            Console.WriteLine($"SchedulerLogic: execute - {workflowId}");
            
            // Ensure dispatcher thread is running
            StartDispatcherThread();
            
            // Enqueue workflow ID for processing
            _workflowQueue.Enqueue(workflowId);
            _queueEvent.Set();
            
            Console.WriteLine($"SchedulerLogic: Workflow {workflowId} enqueued for execution");
        }

        /// <summary>
        /// Internal method that executes a workflow by collecting child workflows and executing them by sequence
        /// </summary>
        /// <param name="workflowId">The workflow ID to execute</param>
        private static void ExecuteWorkflowInternal(long workflowId)
        {
            try
            {
                // Load the workflow
                IWorkflowLogic workflowLogic = WorkflowLogic.Create();
                Workflow workflow = workflowLogic.get(workflowId);
                if (workflow == null || workflow.id == 0)
                {
                    throw new Exception($"Workflow {workflowId} not found");
                }

                Console.WriteLine($"SchedulerLogic: Executing workflow {workflowId} - {workflow.name}");

                // Collect all child workflows recursively or run a single process workflow

                List<Workflow> childWorkflows = new List<Workflow>();

                if (workflow.workflow_type_id == (long) Workflow.WorkflowType.Folder) 
                {
                    CollectChildWorkflows(workflowId, childWorkflows);

                    if (childWorkflows.Count == 0)
                    {
                        Console.WriteLine($"SchedulerLogic: No child workflows found for workflow {workflowId}");
                        return;
                    }
                }
                else // executing a single process workflow
                {
                    childWorkflows.Add(workflow);
                }

                // Group by sequence and sort
                var workflowsBySequence = childWorkflows
                    .GroupBy(w => w.seq) 
                    .OrderBy(g => g.Key)
                    .ToList();

                // Execute each sequence group
                foreach (var sequenceGroup in workflowsBySequence)
                {
                    int seq = sequenceGroup.Key;
                    var sequenceWorkflows = sequenceGroup.ToList();
                    
                    Console.WriteLine($"SchedulerLogic: Executing sequence {seq} with {sequenceWorkflows.Count} child workflows");

                    // Execute all workflows in this sequence concurrently
                    List<global::System.Threading.Tasks.Task> tasks = new List<global::System.Threading.Tasks.Task>();
                    foreach (var sequenceWorkflow in sequenceWorkflows)
                    {
                        tasks.Add(global::System.Threading.Tasks.Task.Run(async () =>
                        {
                            try
                            {
                                await ExecuteChildWorkflowAsync(sequenceWorkflow);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error($"SchedulerLogic: Error executing child workflow {sequenceWorkflow.id}: {ex.Message}", ex);
                                
                                // Handle failure based on on_failure_action_id (from parent or child)
                                if (sequenceWorkflow.on_failure_action_id == (long)Workflow.OnFailureAction.Stop)
                                {
                                    throw; // Re-throw to stop execution
                                }
                                // Otherwise continue (OnFailureAction.Continue)
                            }
                        }));
                    }

                    // Wait for all workflows in this sequence to complete
                    global::System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
                    
                    Console.WriteLine($"SchedulerLogic: Sequence {seq} completed");
                }

                Console.WriteLine($"SchedulerLogic: Workflow {workflowId} execution completed");
            }
            catch (Exception ex)
            {
                Logger.Error($"SchedulerLogic: Error executing workflow {workflowId}: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// Recursively collects all child workflows from a parent workflow
        /// If a child is a Folder type, recursively collects its children as well
        /// </summary>
        /// <param name="parentWorkflowId">The parent workflow ID to collect children from</param>
        /// <param name="childWorkflows">List to collect child workflow nodes into</param>
        private static void CollectChildWorkflows(long parentWorkflowId, List<Workflow> childWorkflows)
        {
            // Get direct child workflows (via parent_id FK)
            List<Workflow> directChildren = new List<Workflow>();
            void WorkflowSelectCallback(System.Data.Common.DbDataReader rdr)
            {
                var wf = new Workflow();
                DBPersist.autoAssign(rdr, wf);
                directChildren.Add(wf);
            }
            DBPersist.select(WorkflowSelectCallback, 
                $"SELECT * FROM core.workflow WHERE parent_id = {parentWorkflowId} and is_active=1 ORDER BY seq");

            // Process each child workflow
            foreach (var childWorkflow in directChildren)
            {
                // Add the child to the collection
                childWorkflows.Add(childWorkflow);
                
                // If the child is a Folder type, recursively collect its children
                if (childWorkflow.workflow_type_id == (long)Workflow.WorkflowType.Folder)
                {
                    Console.WriteLine($"SchedulerLogic: Recursively collecting children from folder workflow {childWorkflow.id}");
                    CollectChildWorkflows(childWorkflow.id, childWorkflows);
                }
                // If Process type, it's a leaf node - no recursion needed
            }
        }

        /// <summary>
        /// Executes a child workflow node
        /// If it's a Process type, dispatches to the agent specified in server_node_id
        /// If it's a Folder type, dispatches back to the scheduler (recursive execution)
        /// </summary>
        /// <param name="childWorkflow">The child workflow to execute</param>
        private static async global::System.Threading.Tasks.Task ExecuteChildWorkflowAsync(Workflow childWorkflow)
        {
            try
            {
                IWorkflowLogic workflowLogic = WorkflowLogic.Create();
                
                // Update status to Dispatched
                childWorkflow.exec_status_id = (long)Execution.ExecStatus.Dispatched;
                workflowLogic.update(childWorkflow.id, childWorkflow);
                
                // Check workflow type
                if (childWorkflow.workflow_type_id == (long)Workflow.WorkflowType.Process)
                {
                    // Process type: dispatch to agent
                    Console.WriteLine($"SchedulerLogic: Dispatching process workflow {childWorkflow.id} to agent");
                    
                    // Load server node agent
                    IServerNodeLogic serverNodeLogic = ServerNodeLogic.Create();
                    ServerNode serverNode = serverNodeLogic.get(childWorkflow.server_node_id);
                    if (serverNode == null || serverNode.id == 0)
                    {
                        throw new Exception($"Server node {childWorkflow.server_node_id} not found for workflow {childWorkflow.id}");
                    }

                    Logger.Info($"SchedulerLogic: Dispatching process workflow {childWorkflow.id} to agent {serverNode.id}");

                    // Invoke server node agent via AgentClient
                    using (var agentClient = new ScriptAgentClient(serverNode))
                    {
                        // Use ExecuteAsync to send workflow to agent
                        await agentClient.ExecuteAsync(childWorkflow.id);
                        Logger.Info($"SchedulerLogic: Agent {serverNode.id} accepted process workflow {childWorkflow.id}");
                    }
                }
                else if (childWorkflow.workflow_type_id == (long)Workflow.WorkflowType.Folder)
                {
                    // Folder type: dispatch back to scheduler (recursive)
                    Console.WriteLine($"SchedulerLogic: Dispatching folder workflow {childWorkflow.id} back to scheduler");
                    
                    Logger.Info($"SchedulerLogic: Dispatching folder workflow {childWorkflow.id} back to scheduler for recursive execution");

                    // Dispatch folder workflow back to scheduler
                    _workflowQueue.Enqueue(childWorkflow.id);
                    _queueEvent.Set();
                    
                    Logger.Info($"SchedulerLogic: Folder workflow {childWorkflow.id} enqueued for recursive execution");
                }
                else
                {
                    throw new Exception($"Unknown workflow type {childWorkflow.workflow_type_id} for workflow {childWorkflow.id}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"SchedulerLogic: Error executing child workflow {childWorkflow.id}: {ex.Message}", ex);
                
                // Update status to Failed on error
                try
                {
                    IWorkflowLogic workflowLogic = WorkflowLogic.Create();
                    childWorkflow.exec_status_id = (long)Execution.ExecStatus.Failed;
                    workflowLogic.update(childWorkflow.id, childWorkflow);
                }
                catch
                {
                    // Ignore errors updating status
                }
                
                throw;
            }
        }

        public void cancel(long workflowId)
        {
            Console.WriteLine($"SchedulerLogic: cancel - ID={workflowId}");
            // TODO: Implement cancel logic
        }

        public void abort(long workflowId)
        {
            Console.WriteLine($"SchedulerLogic: abort - ID={workflowId}");
            // TODO: Implement abort logic
        }

        // =====================================
        // Job Status & Monitoring Operations
        // =====================================

        public object status(long workflowId)
        {
            Console.WriteLine($"SchedulerLogic: status - ID={workflowId}");
            // TODO: Implement status logic
            return new { workflowId, status = "unknown" };
        }

        // =====================================
        // System Management Operations
        // =====================================

        public void unregister(long serverNodeId)
        {
            Logger.Debug($"SchedulerLogic: unregister - serverNodeId={serverNodeId}");
            SetStatusOffline(serverNodeId);
        }
    }
}

