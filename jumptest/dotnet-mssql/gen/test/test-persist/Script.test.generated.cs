using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ScriptTest : BaseTest
    {
        public static void testInsert()
        {
            var script = new Script();


                    script.name = Convert.ToString(BaseTest.getTestData(script, "VARCHAR", TestDataType.random));
                    
                    script.source = Convert.ToString(BaseTest.getTestData(script, "VARCHAR", TestDataType.random));
                    
                    script.script_type_id = (long) BaseTest.getLastId("scripttype");
                    
                Logger.Info("Testing ScriptLogic insert: " + script.ToString());
                ScriptLogic.Create().insert(script);
                BaseTest.addLastId("script", script.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("script");
            var scriptView  = ScriptLogic.Create().get(lastId);

            Script script = new Script(scriptView);

                        script.name = (string) BaseTest.getTestData(script, "VARCHAR", TestDataType.random);
                    
                        script.source = (string) BaseTest.getTestData(script, "VARCHAR", TestDataType.random);
                    
                            script.script_type_id = (long) BaseTest.getLastId("scripttype");
                        
                Logger.Info("Testing ScriptLogic update: " + script.ToString());
                ScriptLogic.Create().update(lastId, script);
                    }
    }
}
