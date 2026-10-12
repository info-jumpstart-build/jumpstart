
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
    public partial class OpRoleController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="OpRoleView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<OpRoleView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<OpRoleView> oproles = OpRoleLogic.Create().select<OpRoleView>();

            return oproles;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="OpRole"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public OpRole Get(long id)
        {
            //Console.WriteLine($"Processing OpRole GET ID={id}");

            OpRole oprole = OpRoleLogic.Create().get(id);

            return oprole;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="OpRoleView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public OpRoleView View(long id)
        {
            //Console.WriteLine($"Processing OpRole View ID={id}");

            OpRoleView oproleView = OpRoleLogic.Create().view(id);

            return oproleView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<OpRole> oproles = OpRoleLogic.Create().select<OpRole>();

            return oproles.Select(oprole => new EnumHelper(oprole.id, oprole.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="oproleView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="OpRoleView"/> with the assigned id.</returns>
        [HttpPost]
        public OpRoleView Post([FromBody] OpRoleView oproleView)
        {
            //Console.WriteLine($"Processing OpRole POST: {oprole}");
            
            JsonHelper.ProcessJsonElements(oproleView);
            
            // Process any JsonElement values to ensure proper type conversion
            OpRole oprole = new OpRole(oproleView);

            
            
            OpRoleLogic.Create().put(oprole); 

            oproleView.id = oprole.id;

            return oproleView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="oproleView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="OpRoleView"/>.</returns>
        [HttpPut("{id}")]
        public OpRoleView Put(long id, [FromBody] OpRoleView oproleView)
        {
            //Console.WriteLine($"Processing OpRole PUT: ID = {id}\n{oprole}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(oproleView);
            
            OpRole oprole = new OpRole(oproleView);

            OpRoleLogic.Create().update(id, oprole);

            oproleView.id = oprole.id;

            return oproleView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            OpRoleLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="OpRoleHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<OpRoleHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<OpRoleHistory> historyList = OpRoleLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves the "Operations" checklist (id/label/mapped rows) for a given OpRole id.
        [HttpGet("{id}/map/operation_op")]
        public IEnumerable<MapOption> Getoperation_op_Map(long id)
        {
            //Console.WriteLine($"Processing GET Operations map for OpRole ID={id}");

            List<MapOption> options = OpRoleLogic.Create().maplist(id, "operation-op");

            return options;
        }
<<<<<<< HEAD
=======

        // Applies the "Operations" checklist submitted from the Edit page: the full set of
        // Operations ids that should be mapped to this OpRole after the call.
        [HttpPut("{id}/map/operation_op")]
        public void Putoperation_op_Map(long id, [FromBody] List<long> checkedIds)
        {
            //Console.WriteLine($"Processing PUT Operations map for OpRole ID={id}");

            OpRoleLogic.Create().mapsync(id, "operation-op", checkedIds ?? new List<long>());
        }
>>>>>>> main
        
       // Retrieves the "Principal" checklist (id/label/mapped rows) for a given OpRole id.
        [HttpGet("{id}/map/principal_principal")]
        public IEnumerable<MapOption> Getprincipal_principal_Map(long id)
        {
            //Console.WriteLine($"Processing GET Principal map for OpRole ID={id}");

            List<MapOption> options = OpRoleLogic.Create().maplist(id, "principal-principal");

            return options;
        }
<<<<<<< HEAD
=======

        // Applies the "Principal" checklist submitted from the Edit page: the full set of
        // Principal ids that should be mapped to this OpRole after the call.
        [HttpPut("{id}/map/principal_principal")]
        public void Putprincipal_principal_Map(long id, [FromBody] List<long> checkedIds)
        {
            //Console.WriteLine($"Processing PUT Principal map for OpRole ID={id}");

            OpRoleLogic.Create().mapsync(id, "principal-principal", checkedIds ?? new List<long>());
        }
>>>>>>> main
            }
}
