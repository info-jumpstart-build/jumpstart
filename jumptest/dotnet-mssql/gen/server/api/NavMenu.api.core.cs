
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest.Controllers
{
    
    public partial class NavMenuController : ControllerBase
    {
   
        // GET api/<NavMenuController>/byparent?parent_id=0&orderby=ordinal
        [HttpGet("byparent")]
        public IEnumerable<NavMenu> GetByParentId([FromQuery] long parent_id, [FromQuery] string orderby = "ordinal")
        {
            //Console.WriteLine($"Processing NavMenu GET parent_id={parent_id}, orderby={orderby}");

            List<NavMenu> navmenus = NavMenuLogic.selectByParentId(parent_id, orderby);

            return navmenus;
        }

        
    }
}
