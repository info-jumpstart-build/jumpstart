
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
    public partial class ScriptTypeController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ScriptTypeView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ScriptTypeView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ScriptTypeView> scripttypes = ScriptTypeLogic.Create().select<ScriptTypeView>();

            return scripttypes;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="ScriptType"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public ScriptType Get(long id)
        {
            //Console.WriteLine($"Processing ScriptType GET ID={id}");

            ScriptType scripttype = ScriptTypeLogic.Create().get(id);

            return scripttype;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ScriptTypeView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ScriptTypeView View(long id)
        {
            //Console.WriteLine($"Processing ScriptType View ID={id}");

            ScriptTypeView scripttypeView = ScriptTypeLogic.Create().view(id);

            return scripttypeView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<ScriptType> scripttypes = ScriptTypeLogic.Create().select<ScriptType>();

            return scripttypes.Select(scripttype => new EnumHelper(scripttype.id, scripttype.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="scripttypeView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ScriptTypeView"/> with the assigned id.</returns>
        [HttpPost]
        public ScriptTypeView Post([FromBody] ScriptTypeView scripttypeView)
        {
            //Console.WriteLine($"Processing ScriptType POST: {scripttype}");
            
            JsonHelper.ProcessJsonElements(scripttypeView);
            
            // Process any JsonElement values to ensure proper type conversion
            ScriptType scripttype = new ScriptType(scripttypeView);

            
            
            ScriptTypeLogic.Create().put(scripttype); 

            scripttypeView.id = scripttype.id;

            return scripttypeView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="scripttypeView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ScriptTypeView"/>.</returns>
        [HttpPut("{id}")]
        public ScriptTypeView Put(long id, [FromBody] ScriptTypeView scripttypeView)
        {
            //Console.WriteLine($"Processing ScriptType PUT: ID = {id}\n{scripttype}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(scripttypeView);
            
            ScriptType scripttype = new ScriptType(scripttypeView);

            ScriptTypeLogic.Create().update(id, scripttype);

            scripttypeView.id = scripttype.id;

            return scripttypeView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ScriptTypeLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ScriptTypeHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ScriptTypeHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ScriptTypeHistory> historyList = ScriptTypeLogic.Create().history(id);

            return historyList;
        }
            

    }
}
