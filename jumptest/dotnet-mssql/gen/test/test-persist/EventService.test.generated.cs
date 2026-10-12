using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class EventServiceTest : BaseTest
    {
        public static void testInsert()
        {
            var eventservice = new EventService();


                    eventservice.event_type = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                    
                    eventservice.objectname_filter = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                    
                    eventservice.methodname_filter = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                    
                    eventservice.script_id = (long) BaseTest.getLastId("script");
                    
                Logger.Info("Testing EventServiceLogic insert: " + eventservice.ToString());
                EventServiceLogic.Create().insert(eventservice);
                BaseTest.addLastId("eventservice", eventservice.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("eventservice");
            var eventserviceView  = EventServiceLogic.Create().get(lastId);

            EventService eventservice = new EventService(eventserviceView);

                        eventservice.event_type = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                        eventservice.objectname_filter = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                        eventservice.methodname_filter = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                            eventservice.script_id = (long) BaseTest.getLastId("script");
                        
                Logger.Info("Testing EventServiceLogic update: " + eventservice.ToString());
                EventServiceLogic.Create().update(lastId, eventservice);
                    }
    }
}
