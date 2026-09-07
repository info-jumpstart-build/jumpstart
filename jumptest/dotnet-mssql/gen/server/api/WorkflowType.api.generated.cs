
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
    public partial class WorkflowTypeController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="WorkflowTypeView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<WorkflowTypeView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<WorkflowTypeView> workflowtypes = WorkflowTypeLogic.Create().select<WorkflowTypeView>();

            return workflowtypes;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="WorkflowType"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public WorkflowType Get(long id)
        {
            //Console.WriteLine($"Processing WorkflowType GET ID={id}");

            WorkflowType workflowtype = WorkflowTypeLogic.Create().get(id);

            return workflowtype;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="WorkflowTypeView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public WorkflowTypeView View(long id)
        {
            //Console.WriteLine($"Processing WorkflowType View ID={id}");

            WorkflowTypeView workflowtypeView = WorkflowTypeLogic.Create().view(id);

            return workflowtypeView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<WorkflowType> workflowtypes = WorkflowTypeLogic.Create().select<WorkflowType>();

            return workflowtypes.Select(workflowtype => new EnumHelper(workflowtype.id, workflowtype.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="workflowtypeView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="WorkflowTypeView"/> with the assigned id.</returns>
        [HttpPost]
        public WorkflowTypeView Post([FromBody] WorkflowTypeView workflowtypeView)
        {
            //Console.WriteLine($"Processing WorkflowType POST: {workflowtype}");
            
            JsonHelper.ProcessJsonElements(workflowtypeView);
            
            // Process any JsonElement values to ensure proper type conversion
            WorkflowType workflowtype = new WorkflowType(workflowtypeView);

            
            
            WorkflowTypeLogic.Create().put(workflowtype); 

            workflowtypeView.id = workflowtype.id;

            return workflowtypeView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="workflowtypeView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="WorkflowTypeView"/>.</returns>
        [HttpPut("{id}")]
        public WorkflowTypeView Put(long id, [FromBody] WorkflowTypeView workflowtypeView)
        {
            //Console.WriteLine($"Processing WorkflowType PUT: ID = {id}\n{workflowtype}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(workflowtypeView);
            
            WorkflowType workflowtype = new WorkflowType(workflowtypeView);

            WorkflowTypeLogic.Create().update(id, workflowtype);

            workflowtypeView.id = workflowtype.id;

            return workflowtypeView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            WorkflowTypeLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="WorkflowTypeHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<WorkflowTypeHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<WorkflowTypeHistory> historyList = WorkflowTypeLogic.Create().history(id);

            return historyList;
        }
            

    }
}
