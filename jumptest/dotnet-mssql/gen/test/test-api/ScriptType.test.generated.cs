using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ScriptTypeTest : BaseTest
    {
        public static async Task testInsert()
        {
            var scripttype = new ScriptType();


                    scripttype.name = Convert.ToString(BaseTest.getTestData(scripttype, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing ScriptType API insert: " + scripttype.ToString());
                var createdScriptType = await PostAsyncReturn("ScriptType", scripttype);
                BaseTest.addLastId("ScriptType", createdScriptType.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var scripttype = new ScriptType();


                        scripttype.name = Convert.ToString(BaseTest.getTestData(scripttype, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing ScriptType API insert (RWK only): " + scripttype.ToString());
                var createdScriptType = await PostAsyncReturn("ScriptType", scripttype);
                BaseTest.addLastId("ScriptType", createdScriptType.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("ScriptType");
            var scripttypeView = await GetByIdAsync<ScriptTypeView>("ScriptType", lastId);
            var scripttype = new ScriptType(scripttypeView);


                        scripttype.name = (string) BaseTest.getTestData(scripttype, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ScriptType API update: " + scripttype.ToString());
                await PutAsync("ScriptType", lastId, scripttype);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("ScriptType");
            var scripttypeView = await GetByIdAsync<ScriptTypeView>("ScriptType", lastId);
            var scripttype = new ScriptType(scripttypeView);


                        scripttype.name = (string) BaseTest.getTestData(scripttype, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing ScriptType API update: " + scripttype.ToString());
                await PutAsync("ScriptType", lastId, scripttype);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing ScriptType API select (list):");
            
            try
            {
                var scripttypeList = await BaseTest.GetListAsync<ScriptType>("ScriptType");
                
                Console.WriteLine($"Retrieved {scripttypeList.Count} ScriptType records");
                
                if (scripttypeList.Count > 0)
                {
                    Console.WriteLine("First record: " + scripttypeList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed ScriptType records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < scripttypeList.Count; i++)
                    {
                        var scripttype = scripttypeList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {scripttype.id}");

                        Console.WriteLine($"  name: {scripttype.name}");
                                
                        Console.WriteLine($"  is_active: {scripttype.is_active}");
                                
                        Console.WriteLine($"  created_by: {scripttype.created_by}");
                                
                        Console.WriteLine($"  last_updated: {scripttype.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {scripttype.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {scripttype.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", scripttypeList[0].id);
                }
                else
                {
                    Console.WriteLine("No ScriptType records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing ScriptType select: {ex.Message}");
                throw;
            }
        }
    }
}
