using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class EventServiceTest : BaseTest
    {
        public static async Task testInsert()
        {
            var eventservice = new EventService();


                    eventservice.event_type = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                    
                    eventservice.objectname_filter = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                    
                    eventservice.methodname_filter = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                    
                    eventservice.script_id = BaseTest.getLastId("Script");
                    
                Console.WriteLine("Testing EventService API insert: " + eventservice.ToString());
                var createdEventService = await PostAsyncReturn("EventService", eventservice);
                BaseTest.addLastId("EventService", createdEventService.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var eventservice = new EventService();


                        eventservice.event_type = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                        
                        eventservice.objectname_filter = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                        
                        eventservice.methodname_filter = Convert.ToString(BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random));
                        
                        eventservice.script_id = BaseTest.getLastId("Script");
                        
                Console.WriteLine("Testing EventService API insert (RWK only): " + eventservice.ToString());
                var createdEventService = await PostAsyncReturn("EventService", eventservice);
                BaseTest.addLastId("EventService", createdEventService.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("EventService");
            var eventserviceView = await GetByIdAsync<EventServiceView>("EventService", lastId);
            var eventservice = new EventService(eventserviceView);


                        eventservice.event_type = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                        eventservice.objectname_filter = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                        eventservice.methodname_filter = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                            eventservice.script_id = BaseTest.getLastId("Script");
                        
                Console.WriteLine("Testing EventService API update: " + eventservice.ToString());
                await PutAsync("EventService", lastId, eventservice);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("EventService");
            var eventserviceView = await GetByIdAsync<EventServiceView>("EventService", lastId);
            var eventservice = new EventService(eventserviceView);


                        eventservice.event_type = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                        eventservice.objectname_filter = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                        eventservice.methodname_filter = (string) BaseTest.getTestData(eventservice, "VARCHAR", TestDataType.random);
                    
                            eventservice.script_id = BaseTest.getLastId("Script");
                        
                Console.WriteLine("Testing EventService API update: " + eventservice.ToString());
                await PutAsync("EventService", lastId, eventservice);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing EventService API select (list):");
            
            try
            {
                var eventserviceList = await BaseTest.GetListAsync<EventService>("EventService");
                
                Console.WriteLine($"Retrieved {eventserviceList.Count} EventService records");
                
                if (eventserviceList.Count > 0)
                {
                    Console.WriteLine("First record: " + eventserviceList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed EventService records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < eventserviceList.Count; i++)
                    {
                        var eventservice = eventserviceList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {eventservice.id}");

                        Console.WriteLine($"  event_type: {eventservice.event_type}");
                                
                        Console.WriteLine($"  objectname_filter: {eventservice.objectname_filter}");
                                
                        Console.WriteLine($"  methodname_filter: {eventservice.methodname_filter}");
                                
                        Console.WriteLine($"  script_id: {eventservice.script_id}");
                                
                        Console.WriteLine($"  is_active: {eventservice.is_active}");
                                
                        Console.WriteLine($"  created_by: {eventservice.created_by}");
                                
                        Console.WriteLine($"  last_updated: {eventservice.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {eventservice.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {eventservice.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", eventserviceList[0].id);
                }
                else
                {
                    Console.WriteLine("No EventService records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing EventService select: {ex.Message}");
                throw;
            }
        }
    }
}
