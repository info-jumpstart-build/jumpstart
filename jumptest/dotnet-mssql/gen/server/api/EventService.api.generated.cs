
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
    public partial class EventServiceController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EventServiceView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<EventServiceView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<EventServiceView> eventservices = EventServiceLogic.Create().select<EventServiceView>();

            return eventservices;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="EventService"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public EventService Get(long id)
        {
            //Console.WriteLine($"Processing EventService GET ID={id}");

            EventService eventservice = EventServiceLogic.Create().get(id);

            return eventservice;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="EventServiceView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public EventServiceView View(long id)
        {
            //Console.WriteLine($"Processing EventService View ID={id}");

            EventServiceView eventserviceView = EventServiceLogic.Create().view(id);

            return eventserviceView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<EventService> eventservices = EventServiceLogic.Create().select<EventService>();

            return eventservices.Select(eventservice => new EnumHelper(eventservice.id, eventservice.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="eventserviceView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="EventServiceView"/> with the assigned id.</returns>
        [HttpPost]
        public EventServiceView Post([FromBody] EventServiceView eventserviceView)
        {
            //Console.WriteLine($"Processing EventService POST: {eventservice}");
            
            JsonHelper.ProcessJsonElements(eventserviceView);
            
            // Process any JsonElement values to ensure proper type conversion
            EventService eventservice = new EventService(eventserviceView);

            
            
            EventServiceLogic.Create().put(eventservice); 

            eventserviceView.id = eventservice.id;

            return eventserviceView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="eventserviceView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="EventServiceView"/>.</returns>
        [HttpPut("{id}")]
        public EventServiceView Put(long id, [FromBody] EventServiceView eventserviceView)
        {
            //Console.WriteLine($"Processing EventService PUT: ID = {id}\n{eventservice}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(eventserviceView);
            
            EventService eventservice = new EventService(eventserviceView);

            EventServiceLogic.Create().update(id, eventservice);

            eventserviceView.id = eventservice.id;

            return eventserviceView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            EventServiceLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="EventServiceHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<EventServiceHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<EventServiceHistory> historyList = EventServiceLogic.Create().history(id);

            return historyList;
        }
            

    }
}
