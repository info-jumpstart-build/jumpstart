using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OpRoleTest : BaseTest
    {
        public static async Task testInsert()
        {
            var oprole = new OpRole();


                    oprole.name = Convert.ToString(BaseTest.getTestData(oprole, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing OpRole API insert: " + oprole.ToString());
                var createdOpRole = await PostAsyncReturn("OpRole", oprole);
                BaseTest.addLastId("OpRole", createdOpRole.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var oprole = new OpRole();


                        oprole.name = Convert.ToString(BaseTest.getTestData(oprole, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing OpRole API insert (RWK only): " + oprole.ToString());
                var createdOpRole = await PostAsyncReturn("OpRole", oprole);
                BaseTest.addLastId("OpRole", createdOpRole.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("OpRole");
            var oproleView = await GetByIdAsync<OpRoleView>("OpRole", lastId);
            var oprole = new OpRole(oproleView);


                        oprole.name = (string) BaseTest.getTestData(oprole, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing OpRole API update: " + oprole.ToString());
                await PutAsync("OpRole", lastId, oprole);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("OpRole");
            var oproleView = await GetByIdAsync<OpRoleView>("OpRole", lastId);
            var oprole = new OpRole(oproleView);


                        oprole.name = (string) BaseTest.getTestData(oprole, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing OpRole API update: " + oprole.ToString());
                await PutAsync("OpRole", lastId, oprole);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing OpRole API select (list):");
            
            try
            {
                var oproleList = await BaseTest.GetListAsync<OpRole>("OpRole");
                
                Console.WriteLine($"Retrieved {oproleList.Count} OpRole records");
                
                if (oproleList.Count > 0)
                {
                    Console.WriteLine("First record: " + oproleList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed OpRole records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < oproleList.Count; i++)
                    {
                        var oprole = oproleList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {oprole.id}");

                        Console.WriteLine($"  name: {oprole.name}");
                                
                        Console.WriteLine($"  is_active: {oprole.is_active}");
                                
                        Console.WriteLine($"  created_by: {oprole.created_by}");
                                
                        Console.WriteLine($"  last_updated: {oprole.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {oprole.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {oprole.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", oproleList[0].id);
                }
                else
                {
                    Console.WriteLine("No OpRole records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing OpRole select: {ex.Message}");
                throw;
            }
        }
    }
}
