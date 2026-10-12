
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
    public partial class ProcessController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ProcessView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ProcessView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ProcessView> processs = ProcessLogic.Create().select<ProcessView>();

            return processs;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Process"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Process Get(long id)
        {
            //Console.WriteLine($"Processing Process GET ID={id}");

            Process process = ProcessLogic.Create().get(id);

            return process;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ProcessView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ProcessView View(long id)
        {
            //Console.WriteLine($"Processing Process View ID={id}");

            ProcessView processView = ProcessLogic.Create().view(id);

            return processView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Process> processs = ProcessLogic.Create().select<Process>();

            return processs.Select(process => new EnumHelper(process.id, process.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="processView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ProcessView"/> with the assigned id.</returns>
        [HttpPost]
        public ProcessView Post([FromBody] ProcessView processView)
        {
            //Console.WriteLine($"Processing Process POST: {process}");
            
            JsonHelper.ProcessJsonElements(processView);
            
            // Process any JsonElement values to ensure proper type conversion
            Process process = new Process(processView);

            
            
            ProcessLogic.Create().put(process); 

            processView.id = process.id;

            return processView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="processView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ProcessView"/>.</returns>
        [HttpPut("{id}")]
        public ProcessView Put(long id, [FromBody] ProcessView processView)
        {
            //Console.WriteLine($"Processing Process PUT: ID = {id}\n{process}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(processView);
            
            Process process = new Process(processView);

            ProcessLogic.Create().update(id, process);

            processView.id = process.id;

            return processView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ProcessLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ProcessHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ProcessHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ProcessHistory> historyList = ProcessLogic.Create().history(id);

            return historyList;
        }
            

    }
}
