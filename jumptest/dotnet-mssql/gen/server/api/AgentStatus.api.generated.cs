
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
    public partial class AgentStatusController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="AgentStatusView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<AgentStatusView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<AgentStatusView> agentstatuss = AgentStatusLogic.Create().select<AgentStatusView>();

            return agentstatuss;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="AgentStatus"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public AgentStatus Get(long id)
        {
            //Console.WriteLine($"Processing AgentStatus GET ID={id}");

            AgentStatus agentstatus = AgentStatusLogic.Create().get(id);

            return agentstatus;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="AgentStatusView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public AgentStatusView View(long id)
        {
            //Console.WriteLine($"Processing AgentStatus View ID={id}");

            AgentStatusView agentstatusView = AgentStatusLogic.Create().view(id);

            return agentstatusView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<AgentStatus> agentstatuss = AgentStatusLogic.Create().select<AgentStatus>();

            return agentstatuss.Select(agentstatus => new EnumHelper(agentstatus.id, agentstatus.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="agentstatusView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="AgentStatusView"/> with the assigned id.</returns>
        [HttpPost]
        public AgentStatusView Post([FromBody] AgentStatusView agentstatusView)
        {
            //Console.WriteLine($"Processing AgentStatus POST: {agentstatus}");
            
            JsonHelper.ProcessJsonElements(agentstatusView);
            
            // Process any JsonElement values to ensure proper type conversion
            AgentStatus agentstatus = new AgentStatus(agentstatusView);

            
            
            AgentStatusLogic.Create().put(agentstatus); 

            agentstatusView.id = agentstatus.id;

            return agentstatusView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="agentstatusView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="AgentStatusView"/>.</returns>
        [HttpPut("{id}")]
        public AgentStatusView Put(long id, [FromBody] AgentStatusView agentstatusView)
        {
            //Console.WriteLine($"Processing AgentStatus PUT: ID = {id}\n{agentstatus}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(agentstatusView);
            
            AgentStatus agentstatus = new AgentStatus(agentstatusView);

            AgentStatusLogic.Create().update(id, agentstatus);

            agentstatusView.id = agentstatus.id;

            return agentstatusView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            AgentStatusLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="AgentStatusHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<AgentStatusHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<AgentStatusHistory> historyList = AgentStatusLogic.Create().history(id);

            return historyList;
        }
            

    }
}
