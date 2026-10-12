using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ExecLogTest : BaseTest
    {
        public static void testInsert()
        {
            var execlog = new ExecLog();


                    execlog.token = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                    
                    execlog.workflow_id = (long) BaseTest.getLastId("workflow");
                    
                    execlog.start_time = Convert.ToDateTime(BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random));
                    
                    execlog.end_time = Convert.ToDateTime(BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random));
                    
                    execlog.stdout = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                    
                    execlog.stderr = Convert.ToString(BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing ExecLogLogic insert: " + execlog.ToString());
                ExecLogLogic.Create().insert(execlog);
                BaseTest.addLastId("execlog", execlog.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("execlog");
            var execlogView  = ExecLogLogic.Create().get(lastId);

            ExecLog execlog = new ExecLog(execlogView);

                        execlog.token = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                            execlog.workflow_id = (long) BaseTest.getLastId("workflow");
                        
                        execlog.start_time = (DateTime) BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random);
                    
                        execlog.end_time = (DateTime) BaseTest.getTestData(execlog, "TIMESTAMP", TestDataType.random);
                    
                        execlog.stdout = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                        execlog.stderr = (string) BaseTest.getTestData(execlog, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing ExecLogLogic update: " + execlog.ToString());
                ExecLogLogic.Create().update(lastId, execlog);
                    }
    }
}
