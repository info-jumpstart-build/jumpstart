
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
    public partial class CronMonthController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CronMonthView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<CronMonthView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<CronMonthView> cronmonths = CronMonthLogic.Create().select<CronMonthView>();

            return cronmonths;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="CronMonth"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public CronMonth Get(long id)
        {
            //Console.WriteLine($"Processing CronMonth GET ID={id}");

            CronMonth cronmonth = CronMonthLogic.Create().get(id);

            return cronmonth;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="CronMonthView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public CronMonthView View(long id)
        {
            //Console.WriteLine($"Processing CronMonth View ID={id}");

            CronMonthView cronmonthView = CronMonthLogic.Create().view(id);

            return cronmonthView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<CronMonth> cronmonths = CronMonthLogic.Create().select<CronMonth>();

            return cronmonths.Select(cronmonth => new EnumHelper(cronmonth.id, cronmonth.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="cronmonthView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="CronMonthView"/> with the assigned id.</returns>
        [HttpPost]
        public CronMonthView Post([FromBody] CronMonthView cronmonthView)
        {
            //Console.WriteLine($"Processing CronMonth POST: {cronmonth}");
            
            JsonHelper.ProcessJsonElements(cronmonthView);
            
            // Process any JsonElement values to ensure proper type conversion
            CronMonth cronmonth = new CronMonth(cronmonthView);

            
            
            CronMonthLogic.Create().put(cronmonth); 

            cronmonthView.id = cronmonth.id;

            return cronmonthView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="cronmonthView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="CronMonthView"/>.</returns>
        [HttpPut("{id}")]
        public CronMonthView Put(long id, [FromBody] CronMonthView cronmonthView)
        {
            //Console.WriteLine($"Processing CronMonth PUT: ID = {id}\n{cronmonth}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(cronmonthView);
            
            CronMonth cronmonth = new CronMonth(cronmonthView);

            CronMonthLogic.Create().update(id, cronmonth);

            cronmonthView.id = cronmonth.id;

            return cronmonthView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            CronMonthLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="CronMonthHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<CronMonthHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<CronMonthHistory> historyList = CronMonthLogic.Create().history(id);

            return historyList;
        }
            

    }
}
