using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class NavMenuTest : BaseTest
    {
        public static void testInsert()
        {
            var navmenu = new NavMenu();


                    navmenu.parent_id = (long) BaseTest.getLastId("navmenu");
                    
                    navmenu.ordinal = Convert.ToInt32(BaseTest.getTestData(navmenu, "INTEGER", TestDataType.random));
                    
                    navmenu.name = Convert.ToString(BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random));
                    
                    navmenu.link = Convert.ToString(BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing NavMenuLogic insert: " + navmenu.ToString());
                NavMenuLogic.Create().insert(navmenu);
                BaseTest.addLastId("navmenu", navmenu.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("navmenu");
            var navmenuView  = NavMenuLogic.Create().get(lastId);

            NavMenu navmenu = new NavMenu(navmenuView);

                            navmenu.parent_id = (long) BaseTest.getLastId("navmenu");
                        
                        navmenu.ordinal = (int) BaseTest.getTestData(navmenu, "INTEGER", TestDataType.random);
                    
                        navmenu.name = (string) BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random);
                    
                        navmenu.link = (string) BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing NavMenuLogic update: " + navmenu.ToString());
                NavMenuLogic.Create().update(lastId, navmenu);
                    }
    }
}
