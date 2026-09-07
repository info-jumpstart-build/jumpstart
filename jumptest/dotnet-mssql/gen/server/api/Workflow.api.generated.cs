
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class WorkflowController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="WorkflowView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<WorkflowView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<WorkflowView> workflows = WorkflowLogic.Create().select<WorkflowView>();

            return workflows;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Workflow"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Workflow Get(long id)
        {
            //Console.WriteLine($"Processing Workflow GET ID={id}");

            Workflow workflow = WorkflowLogic.Create().get(id);

            return workflow;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="WorkflowView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public WorkflowView View(long id)
        {
            //Console.WriteLine($"Processing Workflow View ID={id}");

            WorkflowView workflowView = WorkflowLogic.Create().view(id);

            return workflowView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Workflow> workflows = WorkflowLogic.Create().select<Workflow>();

            return workflows.Select(workflow => new EnumHelper(workflow.id, workflow.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="workflowView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="WorkflowView"/> with the assigned id.</returns>
        [HttpPost]
        public WorkflowView Post([FromBody] WorkflowView workflowView)
        {
            //Console.WriteLine($"Processing Workflow POST: {workflow}");
            
            JsonHelper.ProcessJsonElements(workflowView);
            
            // Process any JsonElement values to ensure proper type conversion
            Workflow workflow = new Workflow(workflowView);

            
            
            WorkflowLogic.Create().put(workflow); 

            workflowView.id = workflow.id;

            return workflowView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="workflowView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="WorkflowView"/>.</returns>
        [HttpPut("{id}")]
        public WorkflowView Put(long id, [FromBody] WorkflowView workflowView)
        {
            //Console.WriteLine($"Processing Workflow PUT: ID = {id}\n{workflow}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(workflowView);
            
            Workflow workflow = new Workflow(workflowView);

            WorkflowLogic.Create().update(id, workflow);

            workflowView.id = workflow.id;

            return workflowView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            WorkflowLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="WorkflowHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<WorkflowHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<WorkflowHistory> historyList = WorkflowLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/execlog_workflow")]
        public IEnumerable<ExecLogView> Getexeclog_workflow(long id)
        {
            //Console.WriteLine($"Processing GET Process for Workflow ID={id}");

            List<ExecLogView> execlogs = WorkflowLogic.Create().children<ExecLogView>(id, "execlog-workflow");

            return execlogs;
        }
        
       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/workflow_parent")]
        public IEnumerable<WorkflowView> Getworkflow_parent(long id)
        {
            //Console.WriteLine($"Processing GET Parent for Workflow ID={id}");

            List<WorkflowView> workflows = WorkflowLogic.Create().children<WorkflowView>(id, "workflow-parent");

            return workflows;
        }
            }
}
