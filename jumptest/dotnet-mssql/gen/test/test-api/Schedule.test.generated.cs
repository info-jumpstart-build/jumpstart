using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ScheduleTest : BaseTest
    {
        public static async Task testInsert()
        {
            var schedule = new Schedule();


                    schedule.name = Convert.ToString(BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random));
                    
                    schedule.cron_every_id = BaseTest.getLastId("CronEvery");
                    
                    schedule.cron_minute_id = BaseTest.getLastId("CronMinute");
                    
                    schedule.cron_hour_id = BaseTest.getLastId("CronHour");
                    
                    schedule.cron_dom_id = BaseTest.getLastId("CronDom");
                    
                    schedule.cron_month_id = BaseTest.getLastId("CronMonth");
                    
                    schedule.cron_dow_id = BaseTest.getLastId("CronDow");
                    
                    schedule.schedule_label = Convert.ToString(BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random));
                    
                    schedule.next_run_time = Convert.ToDateTime(BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random));
                    
                    schedule.last_run_time = Convert.ToDateTime(BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random));
                    
                Console.WriteLine("Testing Schedule API insert: " + schedule.ToString());
                var createdSchedule = await PostAsyncReturn("Schedule", schedule);
                BaseTest.addLastId("Schedule", createdSchedule.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var schedule = new Schedule();


                        schedule.name = Convert.ToString(BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing Schedule API insert (RWK only): " + schedule.ToString());
                var createdSchedule = await PostAsyncReturn("Schedule", schedule);
                BaseTest.addLastId("Schedule", createdSchedule.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Schedule");
            var scheduleView = await GetByIdAsync<ScheduleView>("Schedule", lastId);
            var schedule = new Schedule(scheduleView);


                        schedule.name = (string) BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random);
                    
                            schedule.cron_every_id = BaseTest.getLastId("CronEvery");
                        
                            schedule.cron_minute_id = BaseTest.getLastId("CronMinute");
                        
                            schedule.cron_hour_id = BaseTest.getLastId("CronHour");
                        
                            schedule.cron_dom_id = BaseTest.getLastId("CronDom");
                        
                            schedule.cron_month_id = BaseTest.getLastId("CronMonth");
                        
                            schedule.cron_dow_id = BaseTest.getLastId("CronDow");
                        
                        schedule.schedule_label = (string) BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random);
                    
                        schedule.next_run_time = (DateTime) BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random);
                    
                        schedule.last_run_time = (DateTime) BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random);
                    
                Console.WriteLine("Testing Schedule API update: " + schedule.ToString());
                await PutAsync("Schedule", lastId, schedule);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Schedule");
            var scheduleView = await GetByIdAsync<ScheduleView>("Schedule", lastId);
            var schedule = new Schedule(scheduleView);


                        schedule.name = (string) BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random);
                    
                            schedule.cron_every_id = BaseTest.getLastId("CronEvery");
                        
                            schedule.cron_minute_id = BaseTest.getLastId("CronMinute");
                        
                            schedule.cron_hour_id = BaseTest.getLastId("CronHour");
                        
                            schedule.cron_dom_id = BaseTest.getLastId("CronDom");
                        
                            schedule.cron_month_id = BaseTest.getLastId("CronMonth");
                        
                            schedule.cron_dow_id = BaseTest.getLastId("CronDow");
                        
                        schedule.schedule_label = (string) BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random);
                    
                        schedule.next_run_time = (DateTime) BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random);
                    
                        schedule.last_run_time = (DateTime) BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random);
                    
                Console.WriteLine("Testing Schedule API update: " + schedule.ToString());
                await PutAsync("Schedule", lastId, schedule);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Schedule API select (list):");
            
            try
            {
                var scheduleList = await BaseTest.GetListAsync<Schedule>("Schedule");
                
                Console.WriteLine($"Retrieved {scheduleList.Count} Schedule records");
                
                if (scheduleList.Count > 0)
                {
                    Console.WriteLine("First record: " + scheduleList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Schedule records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < scheduleList.Count; i++)
                    {
                        var schedule = scheduleList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {schedule.id}");

                        Console.WriteLine($"  name: {schedule.name}");
                                
                        Console.WriteLine($"  cron_every_id: {schedule.cron_every_id}");
                                
                        Console.WriteLine($"  cron_minute_id: {schedule.cron_minute_id}");
                                
                        Console.WriteLine($"  cron_hour_id: {schedule.cron_hour_id}");
                                
                        Console.WriteLine($"  cron_dom_id: {schedule.cron_dom_id}");
                                
                        Console.WriteLine($"  cron_month_id: {schedule.cron_month_id}");
                                
                        Console.WriteLine($"  cron_dow_id: {schedule.cron_dow_id}");
                                
                        Console.WriteLine($"  schedule_label: {schedule.schedule_label}");
                                
                        Console.WriteLine($"  next_run_time: {schedule.next_run_time}");
                                
                        Console.WriteLine($"  last_run_time: {schedule.last_run_time}");
                                
                        Console.WriteLine($"  is_active: {schedule.is_active}");
                                
                        Console.WriteLine($"  created_by: {schedule.created_by}");
                                
                        Console.WriteLine($"  last_updated: {schedule.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {schedule.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {schedule.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", scheduleList[0].id);
                }
                else
                {
                    Console.WriteLine("No Schedule records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Schedule select: {ex.Message}");
                throw;
            }
        }
    }
}
