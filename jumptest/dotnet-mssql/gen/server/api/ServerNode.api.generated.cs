
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
    public partial class ServerNodeController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ServerNodeView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ServerNodeView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ServerNodeView> servernodes = ServerNodeLogic.Create().select<ServerNodeView>();

            return servernodes;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="ServerNode"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public ServerNode Get(long id)
        {
            //Console.WriteLine($"Processing ServerNode GET ID={id}");

            ServerNode servernode = ServerNodeLogic.Create().get(id);

            return servernode;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ServerNodeView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ServerNodeView View(long id)
        {
            //Console.WriteLine($"Processing ServerNode View ID={id}");

            ServerNodeView servernodeView = ServerNodeLogic.Create().view(id);

            return servernodeView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<ServerNode> servernodes = ServerNodeLogic.Create().select<ServerNode>();

            return servernodes.Select(servernode => new EnumHelper(servernode.id, servernode.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="servernodeView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ServerNodeView"/> with the assigned id.</returns>
        [HttpPost]
        public ServerNodeView Post([FromBody] ServerNodeView servernodeView)
        {
            //Console.WriteLine($"Processing ServerNode POST: {servernode}");
            
            JsonHelper.ProcessJsonElements(servernodeView);
            
            // Process any JsonElement values to ensure proper type conversion
            ServerNode servernode = new ServerNode(servernodeView);

            
            
            ServerNodeLogic.Create().put(servernode); 

            servernodeView.id = servernode.id;

            return servernodeView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="servernodeView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ServerNodeView"/>.</returns>
        [HttpPut("{id}")]
        public ServerNodeView Put(long id, [FromBody] ServerNodeView servernodeView)
        {
            //Console.WriteLine($"Processing ServerNode PUT: ID = {id}\n{servernode}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(servernodeView);
            
            ServerNode servernode = new ServerNode(servernodeView);

            ServerNodeLogic.Create().update(id, servernode);

            servernodeView.id = servernode.id;

            return servernodeView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ServerNodeLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ServerNodeHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ServerNodeHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ServerNodeHistory> historyList = ServerNodeLogic.Create().history(id);

            return historyList;
        }
            

    }
}
