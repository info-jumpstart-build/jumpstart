
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
    public partial class CronMinuteController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CronMinuteView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<CronMinuteView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<CronMinuteView> cronminutes = CronMinuteLogic.Create().select<CronMinuteView>();

            return cronminutes;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="CronMinute"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public CronMinute Get(long id)
        {
            //Console.WriteLine($"Processing CronMinute GET ID={id}");

            CronMinute cronminute = CronMinuteLogic.Create().get(id);

            return cronminute;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="CronMinuteView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public CronMinuteView View(long id)
        {
            //Console.WriteLine($"Processing CronMinute View ID={id}");

            CronMinuteView cronminuteView = CronMinuteLogic.Create().view(id);

            return cronminuteView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<CronMinute> cronminutes = CronMinuteLogic.Create().select<CronMinute>();

            return cronminutes.Select(cronminute => new EnumHelper(cronminute.id, cronminute.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="cronminuteView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="CronMinuteView"/> with the assigned id.</returns>
        [HttpPost]
        public CronMinuteView Post([FromBody] CronMinuteView cronminuteView)
        {
            //Console.WriteLine($"Processing CronMinute POST: {cronminute}");
            
            JsonHelper.ProcessJsonElements(cronminuteView);
            
            // Process any JsonElement values to ensure proper type conversion
            CronMinute cronminute = new CronMinute(cronminuteView);

            
            
            CronMinuteLogic.Create().put(cronminute); 

            cronminuteView.id = cronminute.id;

            return cronminuteView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="cronminuteView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="CronMinuteView"/>.</returns>
        [HttpPut("{id}")]
        public CronMinuteView Put(long id, [FromBody] CronMinuteView cronminuteView)
        {
            //Console.WriteLine($"Processing CronMinute PUT: ID = {id}\n{cronminute}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(cronminuteView);
            
            CronMinute cronminute = new CronMinute(cronminuteView);

            CronMinuteLogic.Create().update(id, cronminute);

            cronminuteView.id = cronminute.id;

            return cronminuteView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            CronMinuteLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="CronMinuteHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<CronMinuteHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<CronMinuteHistory> historyList = CronMinuteLogic.Create().history(id);

            return historyList;
        }
            

    }
}
