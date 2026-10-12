using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ScriptTest : BaseTest
    {
        public static async Task testInsert()
        {
            var script = new Script();


                    script.name = Convert.ToString(BaseTest.getTestData(script, "VARCHAR", TestDataType.random));
                    
                    script.source = Convert.ToString(BaseTest.getTestData(script, "VARCHAR", TestDataType.random));
                    
                    script.script_type_id = BaseTest.getLastId("ScriptType");
                    
                Console.WriteLine("Testing Script API insert: " + script.ToString());
                var createdScript = await PostAsyncReturn("Script", script);
                BaseTest.addLastId("Script", createdScript.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var script = new Script();


                        script.name = Convert.ToString(BaseTest.getTestData(script, "VARCHAR", TestDataType.random));
                        
                        script.script_type_id = BaseTest.getLastId("ScriptType");
                        
                Console.WriteLine("Testing Script API insert (RWK only): " + script.ToString());
                var createdScript = await PostAsyncReturn("Script", script);
                BaseTest.addLastId("Script", createdScript.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Script");
            var scriptView = await GetByIdAsync<ScriptView>("Script", lastId);
            var script = new Script(scriptView);


                        script.name = (string) BaseTest.getTestData(script, "VARCHAR", TestDataType.random);
                    
                        script.source = (string) BaseTest.getTestData(script, "VARCHAR", TestDataType.random);
                    
                            script.script_type_id = BaseTest.getLastId("ScriptType");
                        
                Console.WriteLine("Testing Script API update: " + script.ToString());
                await PutAsync("Script", lastId, script);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Script");
            var scriptView = await GetByIdAsync<ScriptView>("Script", lastId);
            var script = new Script(scriptView);


                        script.name = (string) BaseTest.getTestData(script, "VARCHAR", TestDataType.random);
                    
                        script.source = (string) BaseTest.getTestData(script, "VARCHAR", TestDataType.random);
                    
                            script.script_type_id = BaseTest.getLastId("ScriptType");
                        
                Console.WriteLine("Testing Script API update: " + script.ToString());
                await PutAsync("Script", lastId, script);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Script API select (list):");
            
            try
            {
                var scriptList = await BaseTest.GetListAsync<Script>("Script");
                
                Console.WriteLine($"Retrieved {scriptList.Count} Script records");
                
                if (scriptList.Count > 0)
                {
                    Console.WriteLine("First record: " + scriptList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Script records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < scriptList.Count; i++)
                    {
                        var script = scriptList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {script.id}");

                        Console.WriteLine($"  name: {script.name}");
                                
                        Console.WriteLine($"  source: {script.source}");
                                
                        Console.WriteLine($"  script_type_id: {script.script_type_id}");
                                
                        Console.WriteLine($"  is_active: {script.is_active}");
                                
                        Console.WriteLine($"  created_by: {script.created_by}");
                                
                        Console.WriteLine($"  last_updated: {script.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {script.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {script.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", scriptList[0].id);
                }
                else
                {
                    Console.WriteLine("No Script records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Script select: {ex.Message}");
                throw;
            }
        }
    }
}
