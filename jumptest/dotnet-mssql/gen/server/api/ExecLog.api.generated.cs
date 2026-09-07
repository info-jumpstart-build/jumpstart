
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
    public partial class ExecLogController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ExecLogView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ExecLogView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ExecLogView> execlogs = ExecLogLogic.Create().select<ExecLogView>();

            return execlogs;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="ExecLog"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public ExecLog Get(long id)
        {
            //Console.WriteLine($"Processing ExecLog GET ID={id}");

            ExecLog execlog = ExecLogLogic.Create().get(id);

            return execlog;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ExecLogView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ExecLogView View(long id)
        {
            //Console.WriteLine($"Processing ExecLog View ID={id}");

            ExecLogView execlogView = ExecLogLogic.Create().view(id);

            return execlogView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<ExecLog> execlogs = ExecLogLogic.Create().select<ExecLog>();

            return execlogs.Select(execlog => new EnumHelper(execlog.id, execlog.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="execlogView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ExecLogView"/> with the assigned id.</returns>
        [HttpPost]
        public ExecLogView Post([FromBody] ExecLogView execlogView)
        {
            //Console.WriteLine($"Processing ExecLog POST: {execlog}");
            
            JsonHelper.ProcessJsonElements(execlogView);
            
            // Process any JsonElement values to ensure proper type conversion
            ExecLog execlog = new ExecLog(execlogView);

            
            
            ExecLogLogic.Create().put(execlog); 

            execlogView.id = execlog.id;

            return execlogView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="execlogView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ExecLogView"/>.</returns>
        [HttpPut("{id}")]
        public ExecLogView Put(long id, [FromBody] ExecLogView execlogView)
        {
            //Console.WriteLine($"Processing ExecLog PUT: ID = {id}\n{execlog}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(execlogView);
            
            ExecLog execlog = new ExecLog(execlogView);

            ExecLogLogic.Create().update(id, execlog);

            execlogView.id = execlog.id;

            return execlogView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ExecLogLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ExecLogHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ExecLogHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ExecLogHistory> historyList = ExecLogLogic.Create().history(id);

            return historyList;
        }
            

    }
}
