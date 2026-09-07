
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
    public partial class CronDowController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CronDowView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<CronDowView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<CronDowView> crondows = CronDowLogic.Create().select<CronDowView>();

            return crondows;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="CronDow"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public CronDow Get(long id)
        {
            //Console.WriteLine($"Processing CronDow GET ID={id}");

            CronDow crondow = CronDowLogic.Create().get(id);

            return crondow;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="CronDowView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public CronDowView View(long id)
        {
            //Console.WriteLine($"Processing CronDow View ID={id}");

            CronDowView crondowView = CronDowLogic.Create().view(id);

            return crondowView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<CronDow> crondows = CronDowLogic.Create().select<CronDow>();

            return crondows.Select(crondow => new EnumHelper(crondow.id, crondow.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="crondowView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="CronDowView"/> with the assigned id.</returns>
        [HttpPost]
        public CronDowView Post([FromBody] CronDowView crondowView)
        {
            //Console.WriteLine($"Processing CronDow POST: {crondow}");
            
            JsonHelper.ProcessJsonElements(crondowView);
            
            // Process any JsonElement values to ensure proper type conversion
            CronDow crondow = new CronDow(crondowView);

            
            
            CronDowLogic.Create().put(crondow); 

            crondowView.id = crondow.id;

            return crondowView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="crondowView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="CronDowView"/>.</returns>
        [HttpPut("{id}")]
        public CronDowView Put(long id, [FromBody] CronDowView crondowView)
        {
            //Console.WriteLine($"Processing CronDow PUT: ID = {id}\n{crondow}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(crondowView);
            
            CronDow crondow = new CronDow(crondowView);

            CronDowLogic.Create().update(id, crondow);

            crondowView.id = crondow.id;

            return crondowView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            CronDowLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="CronDowHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<CronDowHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<CronDowHistory> historyList = CronDowLogic.Create().history(id);

            return historyList;
        }
            

    }
}
