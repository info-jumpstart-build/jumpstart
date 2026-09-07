
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
    public partial class NavMenuController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="NavMenuView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<NavMenuView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<NavMenuView> navmenus = NavMenuLogic.Create().select<NavMenuView>();

            return navmenus;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="NavMenu"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public NavMenu Get(long id)
        {
            //Console.WriteLine($"Processing NavMenu GET ID={id}");

            NavMenu navmenu = NavMenuLogic.Create().get(id);

            return navmenu;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="NavMenuView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public NavMenuView View(long id)
        {
            //Console.WriteLine($"Processing NavMenu View ID={id}");

            NavMenuView navmenuView = NavMenuLogic.Create().view(id);

            return navmenuView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<NavMenu> navmenus = NavMenuLogic.Create().select<NavMenu>();

            return navmenus.Select(navmenu => new EnumHelper(navmenu.id, navmenu.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="navmenuView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="NavMenuView"/> with the assigned id.</returns>
        [HttpPost]
        public NavMenuView Post([FromBody] NavMenuView navmenuView)
        {
            //Console.WriteLine($"Processing NavMenu POST: {navmenu}");
            
            JsonHelper.ProcessJsonElements(navmenuView);
            
            // Process any JsonElement values to ensure proper type conversion
            NavMenu navmenu = new NavMenu(navmenuView);

            
            
            NavMenuLogic.Create().put(navmenu); 

            navmenuView.id = navmenu.id;

            return navmenuView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="navmenuView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="NavMenuView"/>.</returns>
        [HttpPut("{id}")]
        public NavMenuView Put(long id, [FromBody] NavMenuView navmenuView)
        {
            //Console.WriteLine($"Processing NavMenu PUT: ID = {id}\n{navmenu}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(navmenuView);
            
            NavMenu navmenu = new NavMenu(navmenuView);

            NavMenuLogic.Create().update(id, navmenu);

            navmenuView.id = navmenu.id;

            return navmenuView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            NavMenuLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="NavMenuHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<NavMenuHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<NavMenuHistory> historyList = NavMenuLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/navmenu_parent")]
        public IEnumerable<NavMenuView> Getnavmenu_parent(long id)
        {
            //Console.WriteLine($"Processing GET Parent Menu ID for NavMenu ID={id}");

            List<NavMenuView> navmenus = NavMenuLogic.Create().children<NavMenuView>(id, "navmenu-parent");

            return navmenus;
        }
            }
}
