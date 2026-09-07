
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{


    public partial class NavMenuLogic : INavMenuLogic
    {


      
        public static List<NavMenu> selectByParentId(long parent_id, string orderby = "ordinal")
        {
            Console.WriteLine($"Processing NavMenuLogic selectByParentId parent_id={parent_id}, orderby={orderby}");

            List<NavMenu> navmenus = new List<NavMenu>();

            void navmenuCallback(System.Data.Common.DbDataReader rdr)
            {
                NavMenu navmenu = new NavMenu();

                DBPersist.autoAssign(rdr, navmenu);

                navmenus.Add(navmenu);
            };

            // Build the SQL query with WHERE clause and ORDER BY
            string sql = $"SELECT * FROM core.nav_menu WHERE parent_id = {parent_id} ORDER BY {orderby}";
            
            DBPersist.select(navmenuCallback, sql);

            return navmenus;
        }
    }
}
