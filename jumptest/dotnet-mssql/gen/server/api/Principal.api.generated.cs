
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
    public partial class PrincipalController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="PrincipalView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<PrincipalView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<PrincipalView> principals = PrincipalLogic.Create().select<PrincipalView>();

            return principals;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Principal"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Principal Get(long id)
        {
            //Console.WriteLine($"Processing Principal GET ID={id}");

            Principal principal = PrincipalLogic.Create().get(id);

            return principal;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="PrincipalView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public PrincipalView View(long id)
        {
            //Console.WriteLine($"Processing Principal View ID={id}");

            PrincipalView principalView = PrincipalLogic.Create().view(id);

            return principalView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Principal> principals = PrincipalLogic.Create().select<Principal>();

            return principals.Select(principal => new EnumHelper(principal.id, principal.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="principalView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="PrincipalView"/> with the assigned id.</returns>
        [HttpPost]
        public PrincipalView Post([FromBody] PrincipalView principalView)
        {
            //Console.WriteLine($"Processing Principal POST: {principal}");
            
            JsonHelper.ProcessJsonElements(principalView);
            
            // Process any JsonElement values to ensure proper type conversion
            Principal principal = new Principal(principalView);

            
            
            PrincipalLogic.Create().put(principal); 

            principalView.id = principal.id;

            return principalView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="principalView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="PrincipalView"/>.</returns>
        [HttpPut("{id}")]
        public PrincipalView Put(long id, [FromBody] PrincipalView principalView)
        {
            //Console.WriteLine($"Processing Principal PUT: ID = {id}\n{principal}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(principalView);
            
            Principal principal = new Principal(principalView);

            PrincipalLogic.Create().update(id, principal);

            principalView.id = principal.id;

            return principalView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            PrincipalLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="PrincipalHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<PrincipalHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<PrincipalHistory> historyList = PrincipalLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves the "Organization" checklist (id/label/mapped rows) for a given Principal id.
        [HttpGet("{id}/map/org_org")]
        public IEnumerable<MapOption> Getorg_org_Map(long id)
        {
            //Console.WriteLine($"Processing GET Organization map for Principal ID={id}");

            List<MapOption> options = PrincipalLogic.Create().maplist(id, "org-org");

            return options;
        }
<<<<<<< HEAD
=======

        // Applies the "Organization" checklist submitted from the Edit page: the full set of
        // Organization ids that should be mapped to this Principal after the call.
        [HttpPut("{id}/map/org_org")]
        public void Putorg_org_Map(long id, [FromBody] List<long> checkedIds)
        {
            //Console.WriteLine($"Processing PUT Organization map for Principal ID={id}");

            PrincipalLogic.Create().mapsync(id, "org-org", checkedIds ?? new List<long>());
        }
>>>>>>> main
        
       // Retrieves the "Operation Role" checklist (id/label/mapped rows) for a given Principal id.
        [HttpGet("{id}/map/oprole_op_role")]
        public IEnumerable<MapOption> Getoprole_op_role_Map(long id)
        {
            //Console.WriteLine($"Processing GET Operation Role map for Principal ID={id}");

            List<MapOption> options = PrincipalLogic.Create().maplist(id, "oprole-op_role");

            return options;
        }
<<<<<<< HEAD
=======

        // Applies the "Operation Role" checklist submitted from the Edit page: the full set of
        // Operation Role ids that should be mapped to this Principal after the call.
        [HttpPut("{id}/map/oprole_op_role")]
        public void Putoprole_op_role_Map(long id, [FromBody] List<long> checkedIds)
        {
            //Console.WriteLine($"Processing PUT Operation Role map for Principal ID={id}");

            PrincipalLogic.Create().mapsync(id, "oprole-op_role", checkedIds ?? new List<long>());
        }
>>>>>>> main
            }
}
