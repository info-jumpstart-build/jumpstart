using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class OnFailureTest : BaseTest
    {
        public static void testInsert()
        {
            var onfailure = new OnFailure();


                    onfailure.action = Convert.ToString(BaseTest.getTestData(onfailure, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing OnFailureLogic insert: " + onfailure.ToString());
                OnFailureLogic.Create().insert(onfailure);
                BaseTest.addLastId("onfailure", onfailure.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("onfailure");
            var onfailureView  = OnFailureLogic.Create().get(lastId);

            OnFailure onfailure = new OnFailure(onfailureView);

                        onfailure.action = (string) BaseTest.getTestData(onfailure, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing OnFailureLogic update: " + onfailure.ToString());
                OnFailureLogic.Create().update(lastId, onfailure);
                    }
    }
}
