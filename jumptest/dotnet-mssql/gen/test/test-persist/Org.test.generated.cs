using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OrgTest : BaseTest
    {
        public static void testInsert()
        {
            var org = new Org();


                    org.name = Convert.ToString(BaseTest.getTestData(org, "VARCHAR", TestDataType.companies));
                    
                Logger.Info("Testing OrgLogic insert: " + org.ToString());
                OrgLogic.Create().insert(org);
                BaseTest.addLastId("org", org.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("org");
            var orgView  = OrgLogic.Create().get(lastId);

            Org org = new Org(orgView);

                        org.name = (string) BaseTest.getTestData(org, "VARCHAR", TestDataType.companies);
                    
                Logger.Info("Testing OrgLogic update: " + org.ToString());
                OrgLogic.Create().update(lastId, org);
                    }
    }
}
