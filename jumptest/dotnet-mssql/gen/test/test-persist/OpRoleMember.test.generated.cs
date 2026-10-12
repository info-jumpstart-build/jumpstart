using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OpRoleMemberTest : BaseTest
    {
        public static void testInsert()
        {
            var oprolemember = new OpRoleMember();


                    oprolemember.principal_id = (long) BaseTest.getLastId("principal");
                    
                    oprolemember.op_role_id = (long) BaseTest.getLastId("oprole");
                    
                Logger.Info("Testing OpRoleMemberLogic insert: " + oprolemember.ToString());
                OpRoleMemberLogic.Create().insert(oprolemember);
                BaseTest.addLastId("oprolemember", oprolemember.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("oprolemember");
            var oprolememberView  = OpRoleMemberLogic.Create().get(lastId);

            OpRoleMember oprolemember = new OpRoleMember(oprolememberView);

                            oprolemember.principal_id = (long) BaseTest.getLastId("principal");
                        
                            oprolemember.op_role_id = (long) BaseTest.getLastId("oprole");
                        
                Logger.Info("Testing OpRoleMemberLogic update: " + oprolemember.ToString());
                OpRoleMemberLogic.Create().update(lastId, oprolemember);
                    }
    }
}
