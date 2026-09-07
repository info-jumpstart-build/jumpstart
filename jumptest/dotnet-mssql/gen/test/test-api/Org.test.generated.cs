using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OrgTest : BaseTest
    {
        public static async Task testInsert()
        {
            var org = new Org();


                    org.name = Convert.ToString(BaseTest.getTestData(org, "VARCHAR", TestDataType.companies));
                    
                Console.WriteLine("Testing Org API insert: " + org.ToString());
                var createdOrg = await PostAsyncReturn("Org", org);
                BaseTest.addLastId("Org", createdOrg.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var org = new Org();


                        org.name = Convert.ToString(BaseTest.getTestData(org, "VARCHAR", TestDataType.companies));
                        
                Console.WriteLine("Testing Org API insert (RWK only): " + org.ToString());
                var createdOrg = await PostAsyncReturn("Org", org);
                BaseTest.addLastId("Org", createdOrg.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Org");
            var orgView = await GetByIdAsync<OrgView>("Org", lastId);
            var org = new Org(orgView);


                        org.name = (string) BaseTest.getTestData(org, "VARCHAR", TestDataType.companies);
                    
                Console.WriteLine("Testing Org API update: " + org.ToString());
                await PutAsync("Org", lastId, org);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Org");
            var orgView = await GetByIdAsync<OrgView>("Org", lastId);
            var org = new Org(orgView);


                        org.name = (string) BaseTest.getTestData(org, "VARCHAR", TestDataType.companies);
                    
                Console.WriteLine("Testing Org API update: " + org.ToString());
                await PutAsync("Org", lastId, org);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Org API select (list):");
            
            try
            {
                var orgList = await BaseTest.GetListAsync<Org>("Org");
                
                Console.WriteLine($"Retrieved {orgList.Count} Org records");
                
                if (orgList.Count > 0)
                {
                    Console.WriteLine("First record: " + orgList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Org records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < orgList.Count; i++)
                    {
                        var org = orgList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {org.id}");

                        Console.WriteLine($"  name: {org.name}");
                                
                        Console.WriteLine($"  is_active: {org.is_active}");
                                
                        Console.WriteLine($"  created_by: {org.created_by}");
                                
                        Console.WriteLine($"  last_updated: {org.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {org.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {org.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", orgList[0].id);
                }
                else
                {
                    Console.WriteLine("No Org records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Org select: {ex.Message}");
                throw;
            }
        }
    }
}
