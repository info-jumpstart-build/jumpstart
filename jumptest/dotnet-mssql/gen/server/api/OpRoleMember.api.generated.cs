
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
    public partial class OpRoleMemberController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="OpRoleMemberView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<OpRoleMemberView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<OpRoleMemberView> oprolemembers = OpRoleMemberLogic.Create().select<OpRoleMemberView>();

            return oprolemembers;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="OpRoleMember"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public OpRoleMember Get(long id)
        {
            //Console.WriteLine($"Processing OpRoleMember GET ID={id}");

            OpRoleMember oprolemember = OpRoleMemberLogic.Create().get(id);

            return oprolemember;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="OpRoleMemberView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public OpRoleMemberView View(long id)
        {
            //Console.WriteLine($"Processing OpRoleMember View ID={id}");

            OpRoleMemberView oprolememberView = OpRoleMemberLogic.Create().view(id);

            return oprolememberView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<OpRoleMember> oprolemembers = OpRoleMemberLogic.Create().select<OpRoleMember>();

            return oprolemembers.Select(oprolemember => new EnumHelper(oprolemember.id, oprolemember.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="oprolememberView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="OpRoleMemberView"/> with the assigned id.</returns>
        [HttpPost]
        public OpRoleMemberView Post([FromBody] OpRoleMemberView oprolememberView)
        {
            //Console.WriteLine($"Processing OpRoleMember POST: {oprolemember}");
            
            JsonHelper.ProcessJsonElements(oprolememberView);
            
            // Process any JsonElement values to ensure proper type conversion
            OpRoleMember oprolemember = new OpRoleMember(oprolememberView);

            
            
            OpRoleMemberLogic.Create().put(oprolemember); 

            oprolememberView.id = oprolemember.id;

            return oprolememberView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="oprolememberView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="OpRoleMemberView"/>.</returns>
        [HttpPut("{id}")]
        public OpRoleMemberView Put(long id, [FromBody] OpRoleMemberView oprolememberView)
        {
            //Console.WriteLine($"Processing OpRoleMember PUT: ID = {id}\n{oprolemember}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(oprolememberView);
            
            OpRoleMember oprolemember = new OpRoleMember(oprolememberView);

            OpRoleMemberLogic.Create().update(id, oprolemember);

            oprolememberView.id = oprolemember.id;

            return oprolememberView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            OpRoleMemberLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="OpRoleMemberHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<OpRoleMemberHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<OpRoleMemberHistory> historyList = OpRoleMemberLogic.Create().history(id);

            return historyList;
        }
            

    }
}
