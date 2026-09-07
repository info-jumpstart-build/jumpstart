
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
    public partial class ScheduleController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ScheduleView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ScheduleView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ScheduleView> schedules = ScheduleLogic.Create().select<ScheduleView>();

            return schedules;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Schedule"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Schedule Get(long id)
        {
            //Console.WriteLine($"Processing Schedule GET ID={id}");

            Schedule schedule = ScheduleLogic.Create().get(id);

            return schedule;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ScheduleView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ScheduleView View(long id)
        {
            //Console.WriteLine($"Processing Schedule View ID={id}");

            ScheduleView scheduleView = ScheduleLogic.Create().view(id);

            return scheduleView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Schedule> schedules = ScheduleLogic.Create().select<Schedule>();

            return schedules.Select(schedule => new EnumHelper(schedule.id, schedule.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="scheduleView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ScheduleView"/> with the assigned id.</returns>
        [HttpPost]
        public ScheduleView Post([FromBody] ScheduleView scheduleView)
        {
            //Console.WriteLine($"Processing Schedule POST: {schedule}");
            
            JsonHelper.ProcessJsonElements(scheduleView);
            
            // Process any JsonElement values to ensure proper type conversion
            Schedule schedule = new Schedule(scheduleView);

            
            
            ScheduleLogic.Create().put(schedule); 

            scheduleView.id = schedule.id;

            return scheduleView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="scheduleView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ScheduleView"/>.</returns>
        [HttpPut("{id}")]
        public ScheduleView Put(long id, [FromBody] ScheduleView scheduleView)
        {
            //Console.WriteLine($"Processing Schedule PUT: ID = {id}\n{schedule}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(scheduleView);
            
            Schedule schedule = new Schedule(scheduleView);

            ScheduleLogic.Create().update(id, schedule);

            scheduleView.id = schedule.id;

            return scheduleView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ScheduleLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ScheduleHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ScheduleHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ScheduleHistory> historyList = ScheduleLogic.Create().history(id);

            return historyList;
        }
            

    }
}
