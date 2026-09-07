using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OpRoleMapTest : BaseTest
    {
        public static async Task testInsert()
        {
            var oprolemap = new OpRoleMap();


                    oprolemap.op_id = BaseTest.getLastId("Operation");
                    
                    oprolemap.op_role_id = BaseTest.getLastId("OpRole");
                    
                Console.WriteLine("Testing OpRoleMap API insert: " + oprolemap.ToString());
                var createdOpRoleMap = await PostAsyncReturn("OpRoleMap", oprolemap);
                BaseTest.addLastId("OpRoleMap", createdOpRoleMap.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var oprolemap = new OpRoleMap();


                        oprolemap.op_id = BaseTest.getLastId("Operation");
                        
                        oprolemap.op_role_id = BaseTest.getLastId("OpRole");
                        
                Console.WriteLine("Testing OpRoleMap API insert (RWK only): " + oprolemap.ToString());
                var createdOpRoleMap = await PostAsyncReturn("OpRoleMap", oprolemap);
                BaseTest.addLastId("OpRoleMap", createdOpRoleMap.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("OpRoleMap");
            var oprolemapView = await GetByIdAsync<OpRoleMapView>("OpRoleMap", lastId);
            var oprolemap = new OpRoleMap(oprolemapView);


                            oprolemap.op_id = BaseTest.getLastId("Operation");
                        
                            oprolemap.op_role_id = BaseTest.getLastId("OpRole");
                        
                Console.WriteLine("Testing OpRoleMap API update: " + oprolemap.ToString());
                await PutAsync("OpRoleMap", lastId, oprolemap);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("OpRoleMap");
            var oprolemapView = await GetByIdAsync<OpRoleMapView>("OpRoleMap", lastId);
            var oprolemap = new OpRoleMap(oprolemapView);


                            oprolemap.op_id = BaseTest.getLastId("Operation");
                        
                            oprolemap.op_role_id = BaseTest.getLastId("OpRole");
                        
                Console.WriteLine("Testing OpRoleMap API update: " + oprolemap.ToString());
                await PutAsync("OpRoleMap", lastId, oprolemap);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing OpRoleMap API select (list):");
            
            try
            {
                var oprolemapList = await BaseTest.GetListAsync<OpRoleMap>("OpRoleMap");
                
                Console.WriteLine($"Retrieved {oprolemapList.Count} OpRoleMap records");
                
                if (oprolemapList.Count > 0)
                {
                    Console.WriteLine("First record: " + oprolemapList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed OpRoleMap records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < oprolemapList.Count; i++)
                    {
                        var oprolemap = oprolemapList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {oprolemap.id}");

                        Console.WriteLine($"  op_id: {oprolemap.op_id}");
                                
                        Console.WriteLine($"  op_role_id: {oprolemap.op_role_id}");
                                
                        Console.WriteLine($"  is_active: {oprolemap.is_active}");
                                
                        Console.WriteLine($"  created_by: {oprolemap.created_by}");
                                
                        Console.WriteLine($"  last_updated: {oprolemap.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {oprolemap.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {oprolemap.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", oprolemapList[0].id);
                }
                else
                {
                    Console.WriteLine("No OpRoleMap records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing OpRoleMap select: {ex.Message}");
                throw;
            }
        }
    }
}
