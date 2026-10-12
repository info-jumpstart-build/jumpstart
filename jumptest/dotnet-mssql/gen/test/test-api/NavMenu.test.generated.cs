using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class NavMenuTest : BaseTest
    {
        public static async Task testInsert()
        {
            var navmenu = new NavMenu();


                    navmenu.parent_id = BaseTest.getLastId("NavMenu");
                    
                    navmenu.ordinal = Convert.ToInt32(BaseTest.getTestData(navmenu, "INTEGER", TestDataType.random));
                    
                    navmenu.name = Convert.ToString(BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random));
                    
                    navmenu.link = Convert.ToString(BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing NavMenu API insert: " + navmenu.ToString());
                var createdNavMenu = await PostAsyncReturn("NavMenu", navmenu);
                BaseTest.addLastId("NavMenu", createdNavMenu.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var navmenu = new NavMenu();


                        navmenu.name = Convert.ToString(BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing NavMenu API insert (RWK only): " + navmenu.ToString());
                var createdNavMenu = await PostAsyncReturn("NavMenu", navmenu);
                BaseTest.addLastId("NavMenu", createdNavMenu.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("NavMenu");
            var navmenuView = await GetByIdAsync<NavMenuView>("NavMenu", lastId);
            var navmenu = new NavMenu(navmenuView);


                            navmenu.parent_id = BaseTest.getLastId("NavMenu");
                        
                        navmenu.ordinal = (int) BaseTest.getTestData(navmenu, "INTEGER", TestDataType.random);
                    
                        navmenu.name = (string) BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random);
                    
                        navmenu.link = (string) BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing NavMenu API update: " + navmenu.ToString());
                await PutAsync("NavMenu", lastId, navmenu);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("NavMenu");
            var navmenuView = await GetByIdAsync<NavMenuView>("NavMenu", lastId);
            var navmenu = new NavMenu(navmenuView);


                            navmenu.parent_id = BaseTest.getLastId("NavMenu");
                        
                        navmenu.ordinal = (int) BaseTest.getTestData(navmenu, "INTEGER", TestDataType.random);
                    
                        navmenu.name = (string) BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random);
                    
                        navmenu.link = (string) BaseTest.getTestData(navmenu, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing NavMenu API update: " + navmenu.ToString());
                await PutAsync("NavMenu", lastId, navmenu);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing NavMenu API select (list):");
            
            try
            {
                var navmenuList = await BaseTest.GetListAsync<NavMenu>("NavMenu");
                
                Console.WriteLine($"Retrieved {navmenuList.Count} NavMenu records");
                
                if (navmenuList.Count > 0)
                {
                    Console.WriteLine("First record: " + navmenuList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed NavMenu records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < navmenuList.Count; i++)
                    {
                        var navmenu = navmenuList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {navmenu.id}");

                        Console.WriteLine($"  parent_id: {navmenu.parent_id}");
                                
                        Console.WriteLine($"  ordinal: {navmenu.ordinal}");
                                
                        Console.WriteLine($"  name: {navmenu.name}");
                                
                        Console.WriteLine($"  link: {navmenu.link}");
                                
                        Console.WriteLine($"  is_active: {navmenu.is_active}");
                                
                        Console.WriteLine($"  created_by: {navmenu.created_by}");
                                
                        Console.WriteLine($"  last_updated: {navmenu.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {navmenu.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {navmenu.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", navmenuList[0].id);
                }
                else
                {
                    Console.WriteLine("No NavMenu records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing NavMenu select: {ex.Message}");
                throw;
            }
        }
    }
}
