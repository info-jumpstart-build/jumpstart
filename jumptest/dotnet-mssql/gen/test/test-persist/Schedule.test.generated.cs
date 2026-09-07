using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class ScheduleTest : BaseTest
    {
        public static void testInsert()
        {
            var schedule = new Schedule();


                    schedule.name = Convert.ToString(BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random));
                    
                    schedule.cron_every_id = (long) BaseTest.getLastId("cronevery");
                    
                    schedule.cron_minute_id = (long) BaseTest.getLastId("cronminute");
                    
                    schedule.cron_hour_id = (long) BaseTest.getLastId("cronhour");
                    
                    schedule.cron_dom_id = (long) BaseTest.getLastId("crondom");
                    
                    schedule.cron_month_id = (long) BaseTest.getLastId("cronmonth");
                    
                    schedule.cron_dow_id = (long) BaseTest.getLastId("crondow");
                    
                    schedule.schedule_label = Convert.ToString(BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random));
                    
                    schedule.next_run_time = Convert.ToDateTime(BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random));
                    
                    schedule.last_run_time = Convert.ToDateTime(BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random));
                    
                Logger.Info("Testing ScheduleLogic insert: " + schedule.ToString());
                ScheduleLogic.Create().insert(schedule);
                BaseTest.addLastId("schedule", schedule.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("schedule");
            var scheduleView  = ScheduleLogic.Create().get(lastId);

            Schedule schedule = new Schedule(scheduleView);

                        schedule.name = (string) BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random);
                    
                            schedule.cron_every_id = (long) BaseTest.getLastId("cronevery");
                        
                            schedule.cron_minute_id = (long) BaseTest.getLastId("cronminute");
                        
                            schedule.cron_hour_id = (long) BaseTest.getLastId("cronhour");
                        
                            schedule.cron_dom_id = (long) BaseTest.getLastId("crondom");
                        
                            schedule.cron_month_id = (long) BaseTest.getLastId("cronmonth");
                        
                            schedule.cron_dow_id = (long) BaseTest.getLastId("crondow");
                        
                        schedule.schedule_label = (string) BaseTest.getTestData(schedule, "VARCHAR", TestDataType.random);
                    
                        schedule.next_run_time = (DateTime) BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random);
                    
                        schedule.last_run_time = (DateTime) BaseTest.getTestData(schedule, "TIMESTAMP", TestDataType.random);
                    
                Logger.Info("Testing ScheduleLogic update: " + schedule.ToString());
                ScheduleLogic.Create().update(lastId, schedule);
                    }
    }
}
