
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;


namespace jumptest
{
   
   public interface IWorkflowCoreLogic : IWorkflowLogic
   {
        global::System.Threading.Tasks.Task run( long workflowId);
   }

    public class WorkflowCoreLogic : WorkflowUserLogic, IWorkflowCoreLogic
    {
        // Delegate type for execution status handlers
        public delegate void ExecutionStatusHandler(Workflow workflow);

        // Static dictionary mapping execution status id to handler delegates
        private static readonly Dictionary<long, ExecutionStatusHandler> _executionStatusHandlers = new Dictionary<long, ExecutionStatusHandler>
        {
            { (long) Execution.ExecStatus.Executing, workflow => new WorkflowCoreLogic().OnExecuting(workflow) },
            { (long) Execution.ExecStatus.Completed, workflow => new WorkflowCoreLogic().OnCompleted(workflow) },
            { (long) Execution.ExecStatus.Cancelled, workflow => new WorkflowCoreLogic().OnFailed(workflow) },
            { (long) Execution.ExecStatus.Timeout, workflow => new WorkflowCoreLogic().OnFailed(workflow) },
            { (long) Execution.ExecStatus.Interrupted, workflow => new WorkflowCoreLogic().OnFailed(workflow) },
            { (long) Execution.ExecStatus.Suspended, workflow => new WorkflowCoreLogic().OnFailed(workflow) },
            { (long) Execution.ExecStatus.Failed, workflow => new WorkflowCoreLogic().OnFailed(workflow) }
        };

        public WorkflowCoreLogic()
        {
           
        }

         /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="IWorkflowCoreLogic"/>.</returns>
        public static new IWorkflowCoreLogic Create()
        {
            WorkflowCoreLogic workflowCoreLogic = new WorkflowCoreLogic();

            var proxy = DispatchProxy.Create<IWorkflowCoreLogic, Proxy<IWorkflowCoreLogic>>();
            ((Proxy<IWorkflowCoreLogic>)proxy).Initialize();
            ((Proxy<IWorkflowCoreLogic>)proxy).Target = workflowCoreLogic ;
            ((Proxy<IWorkflowCoreLogic>)proxy).DomainObj = "Workflow";

            return proxy;
        }

        override public void update(long workflowId, Workflow workflow)
        {
            Logger.Debug($"WorkflowCoreLogic.update: workflowId={workflowId}");
            // Core logic for updating a workflow
            base.update(workflowId, workflow);
            InvokeExecutionStatusHandler(workflow.exec_status_id, workflow);
        }

        public virtual async global::System.Threading.Tasks.Task run( long workflowId)
        {
            Logger.Debug($"WorkflowCoreLogic.run: workflowId={workflowId}");
            var workflow = WorkflowLogic.Create().get(workflowId);
            if (workflow == null)
                throw new Exception($"Workflow {workflowId} not found");

            var schedulerNodes = DBPersist.select<ServerNode>(
                $"SELECT * FROM core.server_node WHERE server_node_type_id = {(long)ServerNode.ServerNodeType.Scheduler} " +
                $"AND server_node_status_id = {(long)ServerNode.ServerNodeStatus.Online} " +
                $"AND is_active = 1 " +
                $"ORDER BY registered_at DESC");

            if (schedulerNodes == null || schedulerNodes.Count == 0)
                throw new Exception("No online Scheduler server node available");

            using var schedulerClient = new SchedulerClient(schedulerNodes[0]);
            await schedulerClient.ExecuteAsync(workflowId);
        }
        /// <summary>
        /// Invokes the execution status handler for the given status and workflow.
        /// </summary>
        /// <param name="executionStatusId">The execution status id (1=Executing, 2=Completed, 3=Failed).</param>
        /// <param name="workflow">The workflow object to pass to the handler.</param>
        public void InvokeExecutionStatusHandler(long executionStatusId, Workflow workflow)
        {
            Logger.Debug($"WorkflowCoreLogic.InvokeExecutionStatusHandler: executionStatusId={executionStatusId}, workflowId={workflow.id}");
            if (_executionStatusHandlers.ContainsKey(executionStatusId))
            {
                _executionStatusHandlers[executionStatusId].Invoke(workflow);
            }
        }

        /// <summary>
        /// Handles workflow execution started status.
        /// </summary>
        /// <param name="workflow">The workflow being executed.</param>
        public virtual void OnExecuting(Workflow workflow)
        {
            Logger.Debug($"WorkflowCoreLogic.OnExecuting: workflowId={workflow.id}");
            ExecLog execLog = new ExecLog();
            ExecLog filter = new ExecLog();
            filter.workflow_id = workflow.id;
            filter.is_active = 1;

            
            execLog.is_active = 0;

            DBPersist.update(execLog, filter);

            execLog.workflow_id = workflow.id;
            execLog.start_time = workflow.last_start_time;
            execLog.end_time = Util.MinDateValue();
            execLog.exec_status_id = workflow.exec_status_id;
            execLog.is_active = 1;
            DBPersist.insert(execLog);
        }

        /// <summary>
        /// Handles workflow execution completed status.
        /// </summary>
        /// <param name="workflow">The completed workflow.</param>
        public virtual void OnCompleted(Workflow workflow)
        {
            Logger.Debug($"WorkflowCoreLogic.OnCompleted: workflowId={workflow.id}");
            OnCompletedOrFailed(workflow);
        }

        /// <summary>
        /// Handles workflow execution failed status.
        /// </summary>
        /// <param name="workflow">The failed workflow.</param>
        public virtual void OnFailed(Workflow workflow)
        {
            Logger.Debug($"WorkflowCoreLogic.OnFailed: workflowId={workflow.id}");
            OnCompletedOrFailed(workflow);
        }
        
        public virtual void OnCompletedOrFailed(Workflow workflow)
        {
            Logger.Debug($"WorkflowCoreLogic.OnCompletedOrFailed: workflowId={workflow.id}, exec_status_id={workflow.exec_status_id}");
            ExecLog execLog = new ExecLog();
            ExecLog filter = new ExecLog();
            filter.workflow_id = workflow.id;
            filter.is_active = 1;

            
            execLog.is_active = 1;
            execLog.workflow_id = workflow.id;
            execLog.start_time = workflow.last_start_time;
            execLog.end_time = workflow.last_end_time;
            execLog.exec_status_id = workflow.exec_status_id;

            DBPersist.update(execLog, filter);

        }
    }
}

