using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ProcessTest : BaseTest
    {
        public static void testInsert()
        {
            var process = new Process();


                    process.name = Convert.ToString(BaseTest.getTestData(process, "VARCHAR", TestDataType.random));
                    
                    process.script_id = (long) BaseTest.getLastId("script");
                    
                Logger.Info("Testing ProcessLogic insert: " + process.ToString());
                ProcessLogic.Create().insert(process);
                BaseTest.addLastId("process", process.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("process");
            var processView  = ProcessLogic.Create().get(lastId);

            Process process = new Process(processView);

                        process.name = (string) BaseTest.getTestData(process, "VARCHAR", TestDataType.random);
                    
                            process.script_id = (long) BaseTest.getLastId("script");
                        
                Logger.Info("Testing ProcessLogic update: " + process.ToString());
                ProcessLogic.Create().update(lastId, process);
                    }
    }
}
