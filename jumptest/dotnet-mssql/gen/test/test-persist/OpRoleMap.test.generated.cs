using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OpRoleMapTest : BaseTest
    {
        public static void testInsert()
        {
            var oprolemap = new OpRoleMap();


                    oprolemap.op_id = (long) BaseTest.getLastId("operation");
                    
                    oprolemap.op_role_id = (long) BaseTest.getLastId("oprole");
                    
                Logger.Info("Testing OpRoleMapLogic insert: " + oprolemap.ToString());
                OpRoleMapLogic.Create().insert(oprolemap);
                BaseTest.addLastId("oprolemap", oprolemap.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("oprolemap");
            var oprolemapView  = OpRoleMapLogic.Create().get(lastId);

            OpRoleMap oprolemap = new OpRoleMap(oprolemapView);

                            oprolemap.op_id = (long) BaseTest.getLastId("operation");
                        
                            oprolemap.op_role_id = (long) BaseTest.getLastId("oprole");
                        
                Logger.Info("Testing OpRoleMapLogic update: " + oprolemap.ToString());
                OpRoleMapLogic.Create().update(lastId, oprolemap);
                    }
    }
}
