
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
    public partial class CronEveryController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CronEveryView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<CronEveryView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<CronEveryView> croneverys = CronEveryLogic.Create().select<CronEveryView>();

            return croneverys;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="CronEvery"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public CronEvery Get(long id)
        {
            //Console.WriteLine($"Processing CronEvery GET ID={id}");

            CronEvery cronevery = CronEveryLogic.Create().get(id);

            return cronevery;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="CronEveryView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public CronEveryView View(long id)
        {
            //Console.WriteLine($"Processing CronEvery View ID={id}");

            CronEveryView croneveryView = CronEveryLogic.Create().view(id);

            return croneveryView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<CronEvery> croneverys = CronEveryLogic.Create().select<CronEvery>();

            return croneverys.Select(cronevery => new EnumHelper(cronevery.id, cronevery.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="croneveryView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="CronEveryView"/> with the assigned id.</returns>
        [HttpPost]
        public CronEveryView Post([FromBody] CronEveryView croneveryView)
        {
            //Console.WriteLine($"Processing CronEvery POST: {cronevery}");
            
            JsonHelper.ProcessJsonElements(croneveryView);
            
            // Process any JsonElement values to ensure proper type conversion
            CronEvery cronevery = new CronEvery(croneveryView);

            
            
            CronEveryLogic.Create().put(cronevery); 

            croneveryView.id = cronevery.id;

            return croneveryView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="croneveryView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="CronEveryView"/>.</returns>
        [HttpPut("{id}")]
        public CronEveryView Put(long id, [FromBody] CronEveryView croneveryView)
        {
            //Console.WriteLine($"Processing CronEvery PUT: ID = {id}\n{cronevery}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(croneveryView);
            
            CronEvery cronevery = new CronEvery(croneveryView);

            CronEveryLogic.Create().update(id, cronevery);

            croneveryView.id = cronevery.id;

            return croneveryView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            CronEveryLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="CronEveryHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<CronEveryHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<CronEveryHistory> historyList = CronEveryLogic.Create().history(id);

            return historyList;
        }
            

    }
}
