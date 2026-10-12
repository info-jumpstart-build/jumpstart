using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ExecStatusTest : BaseTest
    {
        public static void testInsert()
        {
            var execstatus = new ExecStatus();


                    execstatus.name = Convert.ToString(BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random));
                    
                    execstatus.image = Convert.ToString(BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing ExecStatusLogic insert: " + execstatus.ToString());
                ExecStatusLogic.Create().insert(execstatus);
                BaseTest.addLastId("execstatus", execstatus.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("execstatus");
            var execstatusView  = ExecStatusLogic.Create().get(lastId);

            ExecStatus execstatus = new ExecStatus(execstatusView);

                        execstatus.name = (string) BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random);
                    
                        execstatus.image = (string) BaseTest.getTestData(execstatus, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing ExecStatusLogic update: " + execstatus.ToString());
                ExecStatusLogic.Create().update(lastId, execstatus);
                    }
    }
}
