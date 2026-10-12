
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
    public partial class ServerNodeStatusController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ServerNodeStatusView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ServerNodeStatusView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ServerNodeStatusView> servernodestatuss = ServerNodeStatusLogic.Create().select<ServerNodeStatusView>();

            return servernodestatuss;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="ServerNodeStatus"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public ServerNodeStatus Get(long id)
        {
            //Console.WriteLine($"Processing ServerNodeStatus GET ID={id}");

            ServerNodeStatus servernodestatus = ServerNodeStatusLogic.Create().get(id);

            return servernodestatus;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ServerNodeStatusView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ServerNodeStatusView View(long id)
        {
            //Console.WriteLine($"Processing ServerNodeStatus View ID={id}");

            ServerNodeStatusView servernodestatusView = ServerNodeStatusLogic.Create().view(id);

            return servernodestatusView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<ServerNodeStatus> servernodestatuss = ServerNodeStatusLogic.Create().select<ServerNodeStatus>();

            return servernodestatuss.Select(servernodestatus => new EnumHelper(servernodestatus.id, servernodestatus.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="servernodestatusView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ServerNodeStatusView"/> with the assigned id.</returns>
        [HttpPost]
        public ServerNodeStatusView Post([FromBody] ServerNodeStatusView servernodestatusView)
        {
            //Console.WriteLine($"Processing ServerNodeStatus POST: {servernodestatus}");
            
            JsonHelper.ProcessJsonElements(servernodestatusView);
            
            // Process any JsonElement values to ensure proper type conversion
            ServerNodeStatus servernodestatus = new ServerNodeStatus(servernodestatusView);

            
            
            ServerNodeStatusLogic.Create().put(servernodestatus); 

            servernodestatusView.id = servernodestatus.id;

            return servernodestatusView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="servernodestatusView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ServerNodeStatusView"/>.</returns>
        [HttpPut("{id}")]
        public ServerNodeStatusView Put(long id, [FromBody] ServerNodeStatusView servernodestatusView)
        {
            //Console.WriteLine($"Processing ServerNodeStatus PUT: ID = {id}\n{servernodestatus}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(servernodestatusView);
            
            ServerNodeStatus servernodestatus = new ServerNodeStatus(servernodestatusView);

            ServerNodeStatusLogic.Create().update(id, servernodestatus);

            servernodestatusView.id = servernodestatus.id;

            return servernodestatusView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ServerNodeStatusLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ServerNodeStatusHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ServerNodeStatusHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ServerNodeStatusHistory> historyList = ServerNodeStatusLogic.Create().history(id);

            return historyList;
        }
            

    }
}
