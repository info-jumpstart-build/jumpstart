
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
    public partial class ExecStatusController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ExecStatusView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ExecStatusView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ExecStatusView> execstatuss = ExecStatusLogic.Create().select<ExecStatusView>();

            return execstatuss;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="ExecStatus"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public ExecStatus Get(long id)
        {
            //Console.WriteLine($"Processing ExecStatus GET ID={id}");

            ExecStatus execstatus = ExecStatusLogic.Create().get(id);

            return execstatus;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ExecStatusView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ExecStatusView View(long id)
        {
            //Console.WriteLine($"Processing ExecStatus View ID={id}");

            ExecStatusView execstatusView = ExecStatusLogic.Create().view(id);

            return execstatusView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<ExecStatus> execstatuss = ExecStatusLogic.Create().select<ExecStatus>();

            return execstatuss.Select(execstatus => new EnumHelper(execstatus.id, execstatus.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="execstatusView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ExecStatusView"/> with the assigned id.</returns>
        [HttpPost]
        public ExecStatusView Post([FromBody] ExecStatusView execstatusView)
        {
            //Console.WriteLine($"Processing ExecStatus POST: {execstatus}");
            
            JsonHelper.ProcessJsonElements(execstatusView);
            
            // Process any JsonElement values to ensure proper type conversion
            ExecStatus execstatus = new ExecStatus(execstatusView);

            
            
            ExecStatusLogic.Create().put(execstatus); 

            execstatusView.id = execstatus.id;

            return execstatusView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="execstatusView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ExecStatusView"/>.</returns>
        [HttpPut("{id}")]
        public ExecStatusView Put(long id, [FromBody] ExecStatusView execstatusView)
        {
            //Console.WriteLine($"Processing ExecStatus PUT: ID = {id}\n{execstatus}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(execstatusView);
            
            ExecStatus execstatus = new ExecStatus(execstatusView);

            ExecStatusLogic.Create().update(id, execstatus);

            execstatusView.id = execstatus.id;

            return execstatusView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ExecStatusLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ExecStatusHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ExecStatusHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ExecStatusHistory> historyList = ExecStatusLogic.Create().history(id);

            return historyList;
        }
            

    }
}
