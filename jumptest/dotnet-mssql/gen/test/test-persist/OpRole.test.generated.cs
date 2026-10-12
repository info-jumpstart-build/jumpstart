using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OpRoleTest : BaseTest
    {
        public static void testInsert()
        {
            var oprole = new OpRole();


                    oprole.name = Convert.ToString(BaseTest.getTestData(oprole, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing OpRoleLogic insert: " + oprole.ToString());
                OpRoleLogic.Create().insert(oprole);
                BaseTest.addLastId("oprole", oprole.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("oprole");
            var oproleView  = OpRoleLogic.Create().get(lastId);

            OpRole oprole = new OpRole(oproleView);

                        oprole.name = (string) BaseTest.getTestData(oprole, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing OpRoleLogic update: " + oprole.ToString());
                OpRoleLogic.Create().update(lastId, oprole);
                    }
    }
}
