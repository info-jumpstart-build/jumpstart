using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Concurrent;

namespace jumptest.core
{
    /// <summary>
    /// Interface for Agent logic operations
    /// </summary>
    public interface IScriptAgentLogic
    {
        // Process Management Operations
        void stop(long workflowId);
        void kill(long workflowId);
        void restart(long workflowId);
        void pause(long workflowId);
        void resume(long workflowId);

        // Status & Reporting Operations
        object status(long workflowId);
        void heartbeat(object heartbeatData);
        void report(object reportData);
        void log(object logData);
        void metrics(object metricsData);

        // Resource Management Operations
        long register(ServerNode registrationData);
        void unregister(long serverNodeId);
        object capabilities();
        void allocate(long workflowId);
        void release(long workflowId);

        // Job Execution Operations
        void execute(long workflowId);
        object validate(long workflowId);
        void prepare(long workflowId);
        void cleanup(long workflowId);
        void retry(long workflowId);

        // Communication Operations
        object ping();
        void acknowledge(long workflowId);
        void notify(object notificationData);
        object request(object requestData);

        // System Operations
        void shutdown();
        void reload();
        void update(object updateData);
        object diagnose();
        object health();

        // Security Operations
        object authenticate(object credentials);
        object authorize(object authorizationData);

        // Standard Operations
        List<long> select();
        object get(long workflowId);
        object getAgentInfo();
        long insert(object workflowDefinition);
        void update(long workflowId, object workflowData);
        void delete(long workflowId);


    }

    public partial class ScriptAgentLogic : IScriptAgentLogic
    {
        
        // Singleton thread-safe queue for executing workflows
        private static readonly ConcurrentQueue<long> _workflowQueue = new ConcurrentQueue<long>();
        private static readonly object _queueLock = new object();
        private static volatile bool _isProcessing = false;
        
        // Execution thread management
        private static Thread _executionThread;
        private static readonly object _threadLock = new object();
        private static volatile bool _shouldStop = false;

        // Singleton Agent instance
        private static ServerNode _thisServerNode;
        private static readonly object _serverNodeLock = new object();

        public static IScriptAgentLogic Create()
        {
            var agentLogic = new ScriptAgentLogic();

            var proxy = DispatchProxy.Create<IScriptAgentLogic, Proxy<IScriptAgentLogic>>();
            ((Proxy<IScriptAgentLogic>)proxy).Initialize();
            ((Proxy<IScriptAgentLogic>)proxy).Target = agentLogic;
            ((Proxy<IScriptAgentLogic>)proxy).DomainObj = "ScriptAgent";

            return proxy;
        }

        // =====================================
        // Queue Management Methods
        // =====================================

        /// <summary>
        /// Adds a workflow ID to the queue for processing
        /// </summary>
        /// <param name="workflowId">The workflow ID to queue</param>
        public static void EnqueueWorkflow(long workflowId)
        {
            _workflowQueue.Enqueue(workflowId);
            Console.WriteLine($"ScriptAgentLogic: Enqueued workflow {workflowId}");
        }

        /// <summary>
        /// Gets the current queue size
        /// </summary>
        /// <returns>Number of items in the queue</returns>
        public static int GetQueueSize()
        {
            return _workflowQueue.Count;
        }

        /// <summary>
        /// Processes all queued workflows
        /// </summary>
        public static async global::System.Threading.Tasks.Task ProcessQueueAsync()
        {
            lock (_queueLock)
            {
                if (_isProcessing)
                {
                    Console.WriteLine("ScriptAgentLogic: Queue is already being processed");
                    return;
                }
                _isProcessing = true;
            }

            try
            {
                Console.WriteLine($"ScriptAgentLogic: Starting to process {_workflowQueue.Count} workflows");
                
                while (_workflowQueue.TryDequeue(out long workflowId))
                {
                    try
                    {
                        ScriptAgentLogic.ExecuteWorkflow(workflowId);
                        Console.WriteLine($"ScriptAgentLogic: Successfully processed workflow {workflowId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ScriptAgentLogic: Error processing workflow {workflowId}: {ex.Message}");
                    }
                }
            }
            finally
            {
                lock (_queueLock)
                {
                    _isProcessing = false;
                }
            }
        }

        /// <summary>
        /// Clears all queued workflows
        /// </summary>
        public static void ClearQueue()
        {
            while (_workflowQueue.TryDequeue(out _)) { }
            Console.WriteLine("ScriptAgentLogic: Queue cleared");
        }

        // =====================================
        // Singleton Agent Management
        // =====================================

        /// <summary>
        /// Gets the singleton Agent instance
        /// </summary>
        public static ServerNode thisServerNode
        {
            get
            {
                if (_thisServerNode == null)
                {
                    lock (_serverNodeLock)
                    {
                        if (_thisServerNode == null)
                        {
                            _thisServerNode = new ServerNode();
                            Console.WriteLine("ScriptAgentLogic: Created singleton ServerNode instance");
                        }
                    }
                }
                return _thisServerNode;
            }
        }

        /// <summary>
        /// Sets the singleton Agent instance
        /// </summary>
        /// <param name="agent">The Agent instance to set</param>
        public static void SetThisServerNode(ServerNode serverNode)
        {
            lock (_serverNodeLock)
            {
                _thisServerNode = serverNode;
                Console.WriteLine($"ScriptAgentLogic: Set singleton ServerNode instance (ID: {serverNode?.id})");
            }
        }

        /// <summary>
        /// Clears the singleton Agent instance
        /// </summary>
        public static void ClearThisServerNode()
        {
            lock (_serverNodeLock)
            {
                _thisServerNode = null;
                Console.WriteLine("ScriptAgentLogic: Cleared singleton ServerNode instance");
            }
        }

        /// <summary>
        /// Updates the agent status in the database
        /// </summary>
        /// <param name="status">The new agent status</param>
        private static void UpdateAgentStatus(ServerNode.ServerNodeStatus status)
        {
            if (thisServerNode != null)
            {
                thisServerNode.server_node_status_id = (long)status;
                ServerNodeLogic.Create().update(thisServerNode.id, thisServerNode);
                Logger.Debug($"ScriptAgentLogic.UpdateAgentStatus: Set server node status to {status}");
            }
            else
            {
                Logger.Error("ScriptAgentLogic.UpdateAgentStatus: Server node is null");
            }
        }

        /// <summary>
        /// Sets the server node status to offline (typically called during shutdown)
        /// </summary>
        /// <param name="serverNodeId">The server node ID to update</param>
        public static void SetStatusOffline(long serverNodeId)
        {
            try
            {
                if (serverNodeId <= 0)
                {
                    Logger.Warn($"ScriptAgentLogic.SetStatusOffline: Invalid server node ID: {serverNodeId}");
                    return;
                }

                ServerNode serverNode = new ServerNode();
                serverNode.id = serverNodeId;
                DBPersist.get(serverNode, "default");
                
                if (serverNode != null && serverNode.id > 0)
                {
                    serverNode.server_node_status_id = (long)ServerNode.ServerNodeStatus.Offline;
                    ServerNodeLogic.Create().update(serverNode.id, serverNode);
                    Logger.Info($"ScriptAgentLogic.SetStatusOffline: Set server node {serverNodeId} status to Offline");
                }
                else
                {
                    Logger.Warn($"ScriptAgentLogic.SetStatusOffline: Server node {serverNodeId} not found");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ScriptAgentLogic.SetStatusOffline: Error setting status to Offline for server node {serverNodeId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Starts the execution thread if it's not already running
        /// </summary>
        public static void StartExecutionThread()
        {
            lock (_threadLock)
            {
                if (_executionThread != null && _executionThread.IsAlive)
                {
                    Console.WriteLine("ScriptAgentLogic: Execution thread is already running");
                    return;
                }

                _shouldStop = false;
                _executionThread = new Thread(ExecutionThreadCallback)
                {
                    Name = "ScriptAgentLogic-ExecutionThread",
                    IsBackground = false
                };
                _executionThread.Start();
                Console.WriteLine("ScriptAgentLogic: Execution thread started");
            }
        }

        /// <summary>
        /// Stops the execution thread gracefully
        /// </summary>
        public static void StopExecutionThread()
        {
            lock (_threadLock)
            {
                if (_executionThread == null || !_executionThread.IsAlive)
                {
                    Console.WriteLine("ScriptAgentLogic: Execution thread is not running");
                    return;
                }

                _shouldStop = true;
                _executionThread.Join(TimeSpan.FromSeconds(30)); // Wait up to 30 seconds
                
                if (_executionThread.IsAlive)
                {
                    Console.WriteLine("ScriptAgentLogic: Execution thread did not stop gracefully");
                }
                else
                {
                    Console.WriteLine("ScriptAgentLogic: Execution thread stopped");
                }
            }
        }

        /// <summary>
        /// Thread callback that continuously processes the workflow queue
        /// </summary>
        private static void ExecutionThreadCallback()
        {
            Console.WriteLine("ScriptAgentLogic: Execution thread started processing");
            
            while (!_shouldStop)
            {
                try
                {
                    if (_workflowQueue.TryDequeue(out long workflowId))
                    {
                        Console.WriteLine($"ScriptAgentLogic: Processing workflow {workflowId}");
                        
                        // Set agent status to Busy before execution
                        UpdateAgentStatus(ServerNode.ServerNodeStatus.Busy);
                        
                        try
                        {
                            // Create a new instance and execute the script (blocking)
                            ScriptAgentLogic.ExecuteWorkflow(workflowId);
                            
                            Console.WriteLine($"ScriptAgentLogic: Completed workflow {workflowId}");
                        }
                        finally
                        {
                            // Set agent status back to Online after execution (success or failure)
                            UpdateAgentStatus(ServerNode.ServerNodeStatus.Online);
                        }
                    }
                    else
                    {
                        // No items in queue, sleep briefly to avoid busy waiting
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ScriptAgentLogic: Error in execution thread: {ex.Message}");
                    // Continue processing other workflows even if one fails
                }
            }
            
            Console.WriteLine("ScriptAgentLogic: Execution thread stopped");
        }

        // =====================================
        // Process Management Operations
        // =====================================

        protected static void ExecuteWorkflow(long workflowId)
        {
            IWorkflowLogic workflowLogic = WorkflowCoreLogic.Create();

            Workflow workflow = workflowLogic.get(workflowId);
            if (workflow == null || workflow.id == 0)
            {
                throw new Exception("Workflow not found");
            }

            if (workflow.workflow_type_id != (long)Workflow.WorkflowType.Process)
            {
                throw new Exception("Workflow is not a process");
            }

            IProcessLogic processLogic = ProcessLogic.Create();
            Process process = processLogic.get(workflow.process_id);
            if (process == null || process.id == 0)
            {
                throw new Exception("Process not found");
            }

            if (process.script_id == 0)
            {
                throw new Exception("Process has no script");
            }

            Execution.ExecStatus execStatus = Execution.ExecStatus.Executing;
            workflow.exec_status_id = (long)execStatus;
            workflow.last_start_time = DateTime.UtcNow;
            workflow.last_end_time = Util.MinDateValue();
            workflowLogic.update(workflow.id, workflow);

            IScriptLogic scriptLogic = ScriptLogic.Create();
            Script script = scriptLogic.get(process.script_id);
            Exception scriptException = null;
            if (script == null || script.id == 0)
            {
                workflow.exec_status_id = (long)Execution.ExecStatus.Failed;
                workflowLogic.update(workflow.id, workflow);
                throw new Exception("Script not found");
            }
            try 
            {
                ScriptHost scriptHost = new ScriptHost();

                ScriptContext scriptContext = new ScriptContext(workflow);
                scriptContext.Initialize();
                // Use InvokeSync instead of Invoke to ensure exceptions are properly caught
                scriptHost.InvokeSync<ScriptContext>(scriptContext, script);
                
                execStatus = Execution.ExecStatus.Completed;

                // Check for script-level exceptions or error codes
                if (scriptContext.ScriptException != null)
                {
                    scriptException = new Exception($"Script execution failed: {scriptContext.ScriptException.Message}", scriptContext.ScriptException);
                    execStatus = Execution.ExecStatus.Failed;
                }
                if (scriptContext.retCode != null && scriptContext.retCode != 0)
                {
                    scriptException = new Exception($"Script returned error code: {scriptContext.retCode}");
                    execStatus = Execution.ExecStatus.Failed;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ScriptAgentLogic: Error in ExecuteWorkflow: {ex.Message}", ex);
                execStatus = Execution.ExecStatus.Failed;
            }
            workflow.exec_status_id = (long)execStatus;
            workflow.last_end_time = DateTime.UtcNow;
            workflowLogic.update(workflow.id, workflow);

            if(scriptException != null)
            {
                throw scriptException;
            }
        }
    

        public void stop(long workflowId)
        {
            Logger.Info($"ScriptAgentLogic: stop - workflowId={workflowId}");
            // TODO: Implement stop logic
        }

        public void kill(long workflowId)
        {
            Logger.Info($"ScriptAgentLogic: kill - workflowId={workflowId}");
            // TODO: Implement kill logic
        }

        public void restart(long workflowId)
        {
            Logger.Info($"ScriptAgentLogic: restart - workflowId={workflowId}");
            // TODO: Implement restart logic
        }

        public void pause(long workflowId)
        {
            Logger.Info($"ScriptAgentLogic: pause - workflowId={workflowId}");
            // TODO: Implement pause logic
        }

        public void resume(long workflowId)
        {
            Logger.Info($"ScriptAgentLogic: resume - workflowId={workflowId}");
            // TODO: Implement resume logic
        }

        // =====================================
        // Status & Reporting Operations
        // =====================================

        public object status(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: status - workflowId={workflowId}");
            // TODO: Implement status logic
            return new { workflowId, status = "unknown" };
        }

        public void heartbeat(object heartbeatData)
        {
            Console.WriteLine($"ScriptAgentLogic: heartbeat");
            // TODO: Implement heartbeat logic
            // - Update agent last-seen timestamp
            // - Report current load/capacity
            // - Send to scheduler
        }

        public void report(object reportData)
        {
            Console.WriteLine($"ScriptAgentLogic: report");
            // TODO: Implement report logic
            // - Send execution reports to scheduler
            // - Update execution status
        }

        public void log(object logData)
        {
            Console.WriteLine($"ScriptAgentLogic: log");
            // TODO: Implement log logic
            // - Send log entries to centralized logging
        }

        public void metrics(object metricsData)
        {
            Console.WriteLine($"ScriptAgentLogic: metrics");
            // TODO: Implement metrics logic
            // - Collect performance metrics
            // - Send to monitoring system
        }

        // =====================================
        // Resource Management Operations
        // =====================================

        public long register(ServerNode registrationData)
        {
            Console.WriteLine($"ScriptAgentLogic: register");
            ServerNodeLogic.Create().put(registrationData);
            SetThisServerNode(registrationData);
            return registrationData.id;
        }

        public void unregister(long serverNodeId)
        {
            Logger.Debug($"ScriptAgentLogic: unregister - serverNodeId={serverNodeId}");
            SetStatusOffline(serverNodeId);
            
        }

        public object capabilities()
        {
            Console.WriteLine($"ScriptAgentLogic: capabilities");
            // TODO: Implement capabilities logic
            // - Return server node capabilities (CPU, memory, etc.)
            return new { cpu = "unknown", memory = "unknown", capabilities = new List<string>() };
        }

        public void allocate(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: allocate - workflowId={workflowId}");
            // TODO: Implement allocate logic
            // - Allocate resources for workflow
        }

        public void release(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: release - workflowId={workflowId}");
            // TODO: Implement release logic
            // - Release resources after workflow
        }

        // =====================================
        // Job Execution Operations
        // =====================================

        public void execute(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: execute - workflowId={workflowId}");
            EnqueueWorkflow(workflowId);
            StartExecutionThread();
        }

        public object validate(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: validate - workflowId={workflowId}");
            // TODO: Implement validate logic
            // - Validate workflow requirements
            // - Check resource availability
            return new { valid = true };
        }

        public void prepare(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: prepare - workflowId={workflowId}");
            // TODO: Implement prepare logic
            // - Prepare environment for workflow
            // - Download dependencies
            // - Set up working directory
        }

        public void cleanup(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: cleanup - workflowId={workflowId}");
            // TODO: Implement cleanup logic
            // - Clean up temporary files
            // - Release resources
        }

        public void retry(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: retry - workflowId={workflowId}");
            // TODO: Implement retry logic
            // - Retry failed workflow
        }

        // =====================================
        // Communication Operations
        // =====================================

        public object ping()
        {
            Console.WriteLine($"ScriptAgentLogic: ping");
            // TODO: Implement ping logic
            return new { status = "alive", timestamp = DateTime.UtcNow };
        }

        public void acknowledge(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: acknowledge - workflowId={workflowId}");
            // TODO: Implement acknowledge logic
            // - Acknowledge receipt of workflow request
        }

        public void notify(object notificationData)
        {
            Console.WriteLine($"ScriptAgentLogic: notify");
            // TODO: Implement notify logic
            // - Send notification to scheduler or other systems
        }

        public object request(object requestData)
        {
            Console.WriteLine($"ScriptAgentLogic: request");
            // TODO: Implement request logic
            // - Request resources or information
            return new { status = "pending" };
        }

        // =====================================
        // System Operations
        // =====================================

        public void shutdown()
        {
            Console.WriteLine($"ScriptAgentLogic: shutdown");
            // TODO: Implement shutdown logic
            // - Complete running executions
            // - Unregister from scheduler
            // - Shut down gracefully
        }

        public void reload()
        {
            Console.WriteLine($"ScriptAgentLogic: reload");
            // TODO: Implement reload logic
            // - Reload configuration
            // - Refresh capabilities
        }

        public void update(object updateData)
        {
            Console.WriteLine($"ScriptAgentLogic: update");
            // TODO: Implement update logic
            // - Update agent software
            // - Apply configuration changes
        }

        public object diagnose()
        {
            Console.WriteLine($"ScriptAgentLogic: diagnose");
            // TODO: Implement diagnose logic
            // - Run system diagnostics
            // - Check health status
            // - Return diagnostic information
            return new { status = "healthy", diagnostics = new List<string>() };
        }

        public object health()
        {
            Console.WriteLine($"ScriptAgentLogic: health");
            // TODO: Implement health logic
            // - Check system health
            // - Return health status
            return new { status = "healthy", uptime = TimeSpan.Zero };
        }

        // =====================================
        // Security Operations
        // =====================================

        public object authenticate(object credentials)
        {
            Console.WriteLine($"ScriptAgentLogic: authenticate");
            // TODO: Implement authenticate logic
            // - Verify agent credentials
            // - Generate authentication token
            return new { authenticated = true, token = "placeholder" };
        }

        public object authorize(object authorizationData)
        {
            Console.WriteLine($"ScriptAgentLogic: authorize");
            // TODO: Implement authorize logic
            // - Check authorization for operation
            return new { authorized = true };
        }

        // =====================================
        // Standard Operations
        // =====================================

        public List<long> select()
        {
            Console.WriteLine("ScriptAgentLogic: select all workflows");
            // TODO: Implement select logic
            // - Get all workflow IDs for this agent
            return new List<long>();
        }

        public object get(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: get - workflowId={workflowId}");
            // TODO: Implement get logic
            // - Get workflow details
            return new { workflowId, status = "unknown" };
        }

        public object getAgentInfo()
        {
            Console.WriteLine($"ScriptAgentLogic: getAgentInfo");
            // TODO: Implement getAgentInfo logic
            // - Return agent information (hostname, port, capabilities, status)
            return new 
            { 
                hostname = Environment.MachineName,
                status = "online",
                capabilities = new List<string>()
            };
        }

        public long insert(object workflowDefinition)
        {
            Console.WriteLine($"ScriptAgentLogic: insert - create workflow");
            // TODO: Implement insert logic
            // - Create new workflow record
            // - Return workflow ID
            return 0;
        }

        public void update(long workflowId, object workflowData)
        {
            Console.WriteLine($"ScriptAgentLogic: update - workflowId={workflowId}");
            // TODO: Implement update logic
            // - Update workflow details
        }

        public void delete(long workflowId)
        {
            Console.WriteLine($"ScriptAgentLogic: delete - workflowId={workflowId}");
            // TODO: Implement delete logic
            // - Delete workflow record
        }
    }
}

