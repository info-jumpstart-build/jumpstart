using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ScriptTypeTest : BaseTest
    {
        public static void testInsert()
        {
            var scripttype = new ScriptType();


                    scripttype.name = Convert.ToString(BaseTest.getTestData(scripttype, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing ScriptTypeLogic insert: " + scripttype.ToString());
                ScriptTypeLogic.Create().insert(scripttype);
                BaseTest.addLastId("scripttype", scripttype.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("scripttype");
            var scripttypeView  = ScriptTypeLogic.Create().get(lastId);

            ScriptType scripttype = new ScriptType(scripttypeView);

                        scripttype.name = (string) BaseTest.getTestData(scripttype, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing ScriptTypeLogic update: " + scripttype.ToString());
                ScriptTypeLogic.Create().update(lastId, scripttype);
                    }
    }
}
