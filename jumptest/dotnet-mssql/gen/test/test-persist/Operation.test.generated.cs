using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OperationTest : BaseTest
    {
        public static void testInsert()
        {
            var operation = new Operation();


                    operation.objectname = Convert.ToString(BaseTest.getTestData(operation, "VARCHAR", TestDataType.random));
                    
                    operation.methodname = Convert.ToString(BaseTest.getTestData(operation, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing OperationLogic insert: " + operation.ToString());
                OperationLogic.Create().insert(operation);
                BaseTest.addLastId("operation", operation.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("operation");
            var operationView  = OperationLogic.Create().get(lastId);

            Operation operation = new Operation(operationView);

                        operation.objectname = (string) BaseTest.getTestData(operation, "VARCHAR", TestDataType.random);
                    
                        operation.methodname = (string) BaseTest.getTestData(operation, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing OperationLogic update: " + operation.ToString());
                OperationLogic.Create().update(lastId, operation);
                    }
    }
}
