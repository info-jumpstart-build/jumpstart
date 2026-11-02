
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
    public partial class OrgController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="OrgView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<OrgView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<OrgView> orgs = OrgLogic.Create().select<OrgView>();

            return orgs;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Org"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Org Get(long id)
        {
            //Console.WriteLine($"Processing Org GET ID={id}");

            Org org = OrgLogic.Create().get(id);

            return org;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="OrgView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public OrgView View(long id)
        {
            //Console.WriteLine($"Processing Org View ID={id}");

            OrgView orgView = OrgLogic.Create().view(id);

            return orgView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Org> orgs = OrgLogic.Create().select<Org>();

            return orgs.Select(org => new EnumHelper(org.id, org.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="orgView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="OrgView"/> with the assigned id.</returns>
        [HttpPost]
        public OrgView Post([FromBody] OrgView orgView)
        {
            //Console.WriteLine($"Processing Org POST: {org}");
            
            JsonHelper.ProcessJsonElements(orgView);
            
            // Process any JsonElement values to ensure proper type conversion
            Org org = new Org(orgView);

            
            
            OrgLogic.Create().put(org); 

            orgView.id = org.id;

            return orgView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="orgView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="OrgView"/>.</returns>
        [HttpPut("{id}")]
        public OrgView Put(long id, [FromBody] OrgView orgView)
        {
            //Console.WriteLine($"Processing Org PUT: ID = {id}\n{org}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(orgView);
            
            Org org = new Org(orgView);

            OrgLogic.Create().update(id, org);

            orgView.id = org.id;

            return orgView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            OrgLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="OrgHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<OrgHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<OrgHistory> historyList = OrgLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves the "Principal" checklist (id/label/mapped rows) for a given Org id.
        [HttpGet("{id}/map/principal_principal")]
        public IEnumerable<MapOption> Getprincipal_principal_Map(long id)
        {
            //Console.WriteLine($"Processing GET Principal map for Org ID={id}");

            List<MapOption> options = OrgLogic.Create().maplist(id, "principal-principal");

            return options;
        }

        // Applies the "Principal" checklist submitted from the Edit page: the full set of
        // Principal ids that should be mapped to this Org after the call.
        [HttpPut("{id}/map/principal_principal")]
        public void Putprincipal_principal_Map(long id, [FromBody] List<long> checkedIds)
        {
            //Console.WriteLine($"Processing PUT Principal map for Org ID={id}");

            OrgLogic.Create().mapsync(id, "principal-principal", checkedIds ?? new List<long>());
        }
            }
}
