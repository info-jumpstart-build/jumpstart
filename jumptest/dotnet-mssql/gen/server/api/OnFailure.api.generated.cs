
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
    public partial class OnFailureController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="OnFailureView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<OnFailureView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<OnFailureView> onfailures = OnFailureLogic.Create().select<OnFailureView>();

            return onfailures;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="OnFailure"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public OnFailure Get(long id)
        {
            //Console.WriteLine($"Processing OnFailure GET ID={id}");

            OnFailure onfailure = OnFailureLogic.Create().get(id);

            return onfailure;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="OnFailureView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public OnFailureView View(long id)
        {
            //Console.WriteLine($"Processing OnFailure View ID={id}");

            OnFailureView onfailureView = OnFailureLogic.Create().view(id);

            return onfailureView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<OnFailure> onfailures = OnFailureLogic.Create().select<OnFailure>();

            return onfailures.Select(onfailure => new EnumHelper(onfailure.id, onfailure.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="onfailureView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="OnFailureView"/> with the assigned id.</returns>
        [HttpPost]
        public OnFailureView Post([FromBody] OnFailureView onfailureView)
        {
            //Console.WriteLine($"Processing OnFailure POST: {onfailure}");
            
            JsonHelper.ProcessJsonElements(onfailureView);
            
            // Process any JsonElement values to ensure proper type conversion
            OnFailure onfailure = new OnFailure(onfailureView);

            
            
            OnFailureLogic.Create().put(onfailure); 

            onfailureView.id = onfailure.id;

            return onfailureView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="onfailureView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="OnFailureView"/>.</returns>
        [HttpPut("{id}")]
        public OnFailureView Put(long id, [FromBody] OnFailureView onfailureView)
        {
            //Console.WriteLine($"Processing OnFailure PUT: ID = {id}\n{onfailure}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(onfailureView);
            
            OnFailure onfailure = new OnFailure(onfailureView);

            OnFailureLogic.Create().update(id, onfailure);

            onfailureView.id = onfailure.id;

            return onfailureView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            OnFailureLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="OnFailureHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<OnFailureHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<OnFailureHistory> historyList = OnFailureLogic.Create().history(id);

            return historyList;
        }
            

    }
}
