
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
    public partial class ScriptController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ScriptView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ScriptView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ScriptView> scripts = ScriptLogic.Create().select<ScriptView>();

            return scripts;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Script"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Script Get(long id)
        {
            //Console.WriteLine($"Processing Script GET ID={id}");

            Script script = ScriptLogic.Create().get(id);

            return script;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ScriptView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ScriptView View(long id)
        {
            //Console.WriteLine($"Processing Script View ID={id}");

            ScriptView scriptView = ScriptLogic.Create().view(id);

            return scriptView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Script> scripts = ScriptLogic.Create().select<Script>();

            return scripts.Select(script => new EnumHelper(script.id, script.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="scriptView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ScriptView"/> with the assigned id.</returns>
        [HttpPost]
        public ScriptView Post([FromBody] ScriptView scriptView)
        {
            //Console.WriteLine($"Processing Script POST: {script}");
            
            JsonHelper.ProcessJsonElements(scriptView);
            
            // Process any JsonElement values to ensure proper type conversion
            Script script = new Script(scriptView);

            
            
            ScriptLogic.Create().put(script); 

            scriptView.id = script.id;

            return scriptView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="scriptView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ScriptView"/>.</returns>
        [HttpPut("{id}")]
        public ScriptView Put(long id, [FromBody] ScriptView scriptView)
        {
            //Console.WriteLine($"Processing Script PUT: ID = {id}\n{script}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(scriptView);
            
            Script script = new Script(scriptView);

            ScriptLogic.Create().update(id, script);

            scriptView.id = script.id;

            return scriptView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ScriptLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ScriptHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ScriptHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ScriptHistory> historyList = ScriptLogic.Create().history(id);

            return historyList;
        }
            

    }
}
