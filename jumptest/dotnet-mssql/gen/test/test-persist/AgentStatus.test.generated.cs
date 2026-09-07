using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class AgentStatusTest : BaseTest
    {
        public static void testInsert()
        {
            var agentstatus = new AgentStatus();


                    agentstatus.name = Convert.ToString(BaseTest.getTestData(agentstatus, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing AgentStatusLogic insert: " + agentstatus.ToString());
                AgentStatusLogic.Create().insert(agentstatus);
                BaseTest.addLastId("agentstatus", agentstatus.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("agentstatus");
            var agentstatusView  = AgentStatusLogic.Create().get(lastId);

            AgentStatus agentstatus = new AgentStatus(agentstatusView);

                        agentstatus.name = (string) BaseTest.getTestData(agentstatus, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing AgentStatusLogic update: " + agentstatus.ToString());
                AgentStatusLogic.Create().update(lastId, agentstatus);
                    }
    }
}
