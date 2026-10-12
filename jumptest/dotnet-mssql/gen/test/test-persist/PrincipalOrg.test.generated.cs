using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class PrincipalOrgTest : BaseTest
    {
        public static void testInsert()
        {
            var principalorg = new PrincipalOrg();


                    principalorg.org_id = (long) BaseTest.getLastId("org");
                    
                    principalorg.principal_id = (long) BaseTest.getLastId("principal");
                    
                Logger.Info("Testing PrincipalOrgLogic insert: " + principalorg.ToString());
                PrincipalOrgLogic.Create().insert(principalorg);
                BaseTest.addLastId("principalorg", principalorg.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("principalorg");
            var principalorgView  = PrincipalOrgLogic.Create().get(lastId);

            PrincipalOrg principalorg = new PrincipalOrg(principalorgView);

                            principalorg.org_id = (long) BaseTest.getLastId("org");
                        
                            principalorg.principal_id = (long) BaseTest.getLastId("principal");
                        
                Logger.Info("Testing PrincipalOrgLogic update: " + principalorg.ToString());
                PrincipalOrgLogic.Create().update(lastId, principalorg);
                    }
    }
}
