
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
    public partial class ServerNodeTypeController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="ServerNodeTypeView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<ServerNodeTypeView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<ServerNodeTypeView> servernodetypes = ServerNodeTypeLogic.Create().select<ServerNodeTypeView>();

            return servernodetypes;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="ServerNodeType"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public ServerNodeType Get(long id)
        {
            //Console.WriteLine($"Processing ServerNodeType GET ID={id}");

            ServerNodeType servernodetype = ServerNodeTypeLogic.Create().get(id);

            return servernodetype;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="ServerNodeTypeView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public ServerNodeTypeView View(long id)
        {
            //Console.WriteLine($"Processing ServerNodeType View ID={id}");

            ServerNodeTypeView servernodetypeView = ServerNodeTypeLogic.Create().view(id);

            return servernodetypeView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<ServerNodeType> servernodetypes = ServerNodeTypeLogic.Create().select<ServerNodeType>();

            return servernodetypes.Select(servernodetype => new EnumHelper(servernodetype.id, servernodetype.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="servernodetypeView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="ServerNodeTypeView"/> with the assigned id.</returns>
        [HttpPost]
        public ServerNodeTypeView Post([FromBody] ServerNodeTypeView servernodetypeView)
        {
            //Console.WriteLine($"Processing ServerNodeType POST: {servernodetype}");
            
            JsonHelper.ProcessJsonElements(servernodetypeView);
            
            // Process any JsonElement values to ensure proper type conversion
            ServerNodeType servernodetype = new ServerNodeType(servernodetypeView);

            
            
            ServerNodeTypeLogic.Create().put(servernodetype); 

            servernodetypeView.id = servernodetype.id;

            return servernodetypeView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="servernodetypeView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="ServerNodeTypeView"/>.</returns>
        [HttpPut("{id}")]
        public ServerNodeTypeView Put(long id, [FromBody] ServerNodeTypeView servernodetypeView)
        {
            //Console.WriteLine($"Processing ServerNodeType PUT: ID = {id}\n{servernodetype}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(servernodetypeView);
            
            ServerNodeType servernodetype = new ServerNodeType(servernodetypeView);

            ServerNodeTypeLogic.Create().update(id, servernodetype);

            servernodetypeView.id = servernodetype.id;

            return servernodetypeView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ServerNodeTypeLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="ServerNodeTypeHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<ServerNodeTypeHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<ServerNodeTypeHistory> historyList = ServerNodeTypeLogic.Create().history(id);

            return historyList;
        }
            

    }
}
