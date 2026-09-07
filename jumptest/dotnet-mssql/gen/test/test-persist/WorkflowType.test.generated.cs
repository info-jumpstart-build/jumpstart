using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class WorkflowTypeTest : BaseTest
    {
        public static void testInsert()
        {
            var workflowtype = new WorkflowType();


                    workflowtype.name = Convert.ToString(BaseTest.getTestData(workflowtype, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing WorkflowTypeLogic insert: " + workflowtype.ToString());
                WorkflowTypeLogic.Create().insert(workflowtype);
                BaseTest.addLastId("workflowtype", workflowtype.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("workflowtype");
            var workflowtypeView  = WorkflowTypeLogic.Create().get(lastId);

            WorkflowType workflowtype = new WorkflowType(workflowtypeView);

                        workflowtype.name = (string) BaseTest.getTestData(workflowtype, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing WorkflowTypeLogic update: " + workflowtype.ToString());
                WorkflowTypeLogic.Create().update(lastId, workflowtype);
                    }
    }
}
