
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
    public partial class CronHourController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CronHourView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<CronHourView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<CronHourView> cronhours = CronHourLogic.Create().select<CronHourView>();

            return cronhours;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="CronHour"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public CronHour Get(long id)
        {
            //Console.WriteLine($"Processing CronHour GET ID={id}");

            CronHour cronhour = CronHourLogic.Create().get(id);

            return cronhour;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="CronHourView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public CronHourView View(long id)
        {
            //Console.WriteLine($"Processing CronHour View ID={id}");

            CronHourView cronhourView = CronHourLogic.Create().view(id);

            return cronhourView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<CronHour> cronhours = CronHourLogic.Create().select<CronHour>();

            return cronhours.Select(cronhour => new EnumHelper(cronhour.id, cronhour.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="cronhourView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="CronHourView"/> with the assigned id.</returns>
        [HttpPost]
        public CronHourView Post([FromBody] CronHourView cronhourView)
        {
            //Console.WriteLine($"Processing CronHour POST: {cronhour}");
            
            JsonHelper.ProcessJsonElements(cronhourView);
            
            // Process any JsonElement values to ensure proper type conversion
            CronHour cronhour = new CronHour(cronhourView);

            
            
            CronHourLogic.Create().put(cronhour); 

            cronhourView.id = cronhour.id;

            return cronhourView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="cronhourView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="CronHourView"/>.</returns>
        [HttpPut("{id}")]
        public CronHourView Put(long id, [FromBody] CronHourView cronhourView)
        {
            //Console.WriteLine($"Processing CronHour PUT: ID = {id}\n{cronhour}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(cronhourView);
            
            CronHour cronhour = new CronHour(cronhourView);

            CronHourLogic.Create().update(id, cronhour);

            cronhourView.id = cronhour.id;

            return cronhourView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            CronHourLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="CronHourHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<CronHourHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<CronHourHistory> historyList = CronHourLogic.Create().history(id);

            return historyList;
        }
            

    }
}
