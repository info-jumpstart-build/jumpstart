
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
    public partial class OpRoleMapController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="OpRoleMapView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<OpRoleMapView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<OpRoleMapView> oprolemaps = OpRoleMapLogic.Create().select<OpRoleMapView>();

            return oprolemaps;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="OpRoleMap"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public OpRoleMap Get(long id)
        {
            //Console.WriteLine($"Processing OpRoleMap GET ID={id}");

            OpRoleMap oprolemap = OpRoleMapLogic.Create().get(id);

            return oprolemap;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="OpRoleMapView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public OpRoleMapView View(long id)
        {
            //Console.WriteLine($"Processing OpRoleMap View ID={id}");

            OpRoleMapView oprolemapView = OpRoleMapLogic.Create().view(id);

            return oprolemapView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<OpRoleMap> oprolemaps = OpRoleMapLogic.Create().select<OpRoleMap>();

            return oprolemaps.Select(oprolemap => new EnumHelper(oprolemap.id, oprolemap.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="oprolemapView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="OpRoleMapView"/> with the assigned id.</returns>
        [HttpPost]
        public OpRoleMapView Post([FromBody] OpRoleMapView oprolemapView)
        {
            //Console.WriteLine($"Processing OpRoleMap POST: {oprolemap}");
            
            JsonHelper.ProcessJsonElements(oprolemapView);
            
            // Process any JsonElement values to ensure proper type conversion
            OpRoleMap oprolemap = new OpRoleMap(oprolemapView);

            
            
            OpRoleMapLogic.Create().put(oprolemap); 

            oprolemapView.id = oprolemap.id;

            return oprolemapView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="oprolemapView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="OpRoleMapView"/>.</returns>
        [HttpPut("{id}")]
        public OpRoleMapView Put(long id, [FromBody] OpRoleMapView oprolemapView)
        {
            //Console.WriteLine($"Processing OpRoleMap PUT: ID = {id}\n{oprolemap}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(oprolemapView);
            
            OpRoleMap oprolemap = new OpRoleMap(oprolemapView);

            OpRoleMapLogic.Create().update(id, oprolemap);

            oprolemapView.id = oprolemap.id;

            return oprolemapView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            OpRoleMapLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="OpRoleMapHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<OpRoleMapHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<OpRoleMapHistory> historyList = OpRoleMapLogic.Create().history(id);

            return historyList;
        }
            

    }
}
