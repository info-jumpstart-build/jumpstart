
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
    public partial class CronDomController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CronDomView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<CronDomView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<CronDomView> crondoms = CronDomLogic.Create().select<CronDomView>();

            return crondoms;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="CronDom"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public CronDom Get(long id)
        {
            //Console.WriteLine($"Processing CronDom GET ID={id}");

            CronDom crondom = CronDomLogic.Create().get(id);

            return crondom;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="CronDomView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public CronDomView View(long id)
        {
            //Console.WriteLine($"Processing CronDom View ID={id}");

            CronDomView crondomView = CronDomLogic.Create().view(id);

            return crondomView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<CronDom> crondoms = CronDomLogic.Create().select<CronDom>();

            return crondoms.Select(crondom => new EnumHelper(crondom.id, crondom.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="crondomView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="CronDomView"/> with the assigned id.</returns>
        [HttpPost]
        public CronDomView Post([FromBody] CronDomView crondomView)
        {
            //Console.WriteLine($"Processing CronDom POST: {crondom}");
            
            JsonHelper.ProcessJsonElements(crondomView);
            
            // Process any JsonElement values to ensure proper type conversion
            CronDom crondom = new CronDom(crondomView);

            
            
            CronDomLogic.Create().put(crondom); 

            crondomView.id = crondom.id;

            return crondomView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="crondomView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="CronDomView"/>.</returns>
        [HttpPut("{id}")]
        public CronDomView Put(long id, [FromBody] CronDomView crondomView)
        {
            //Console.WriteLine($"Processing CronDom PUT: ID = {id}\n{crondom}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(crondomView);
            
            CronDom crondom = new CronDom(crondomView);

            CronDomLogic.Create().update(id, crondom);

            crondomView.id = crondom.id;

            return crondomView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            CronDomLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="CronDomHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<CronDomHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<CronDomHistory> historyList = CronDomLogic.Create().history(id);

            return historyList;
        }
            

    }
}
