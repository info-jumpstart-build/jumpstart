

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Schedule")]
    public partial class Schedule : BaseObject
    {
        public Schedule(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "Schedule";
            tableName = "core.schedule";
            schemaName = "core";
            tableBaseName = "schedule";
            auditTableName = "core.schedule"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("name");
            

            _defaults["id"] = default(long);
            
            _defaults["name"] = default(string);
            
            _defaults["cron_every_id"] = default(long);
            
            _defaults["cron_minute_id"] = default(long);
            
            _defaults["cron_hour_id"] = default(long);
            
            _defaults["cron_dom_id"] = default(long);
            
            _defaults["cron_month_id"] = default(long);
            
            _defaults["cron_dow_id"] = default(long);
            
            _defaults["schedule_label"] = default(string);
            
            _defaults["next_run_time"] = default(DateTime);
            
            _defaults["last_run_time"] = default(DateTime);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("", "", "", "", "", false)]
            public long id
            {
                get
                {
                    long _id;

                             _id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("id"))
                        {
                        _id = Convert.ToInt64(this["id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting id: {e.Message}");
                        _id = default(long);
                    }
                    return _id;
                }
                set
                {
                   
                    this["id"] = value;
                }
            }
            
            [ColumnInfo("Name", "", "", "", "", false)]
            public string name
            {
                get
                {
                    string _name;

                            _name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("name"))
                        {
                        _name = Convert.ToString(this["name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting name: {e.Message}");
                        _name = default(string);
                    }
                    return _name;
                }
                set
                {
                   
                    this["name"] = value;
                }
            }
            
            [ColumnInfo("Run At", "CronEvery", "enum", "cron_every", "cronevery", false)]
            public long cron_every_id
            {
                get
                {
                    long _cron_every_id;

                             _cron_every_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("cron_every_id"))
                        {
                        _cron_every_id = Convert.ToInt64(this["cron_every_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_every_id: {e.Message}");
                        _cron_every_id = default(long);
                    }
                    return _cron_every_id;
                }
                set
                {
                   
                    this["cron_every_id"] = value;
                }
            }
            
            [ColumnInfo("Minute", "CronMinute", "enum", "cron_minute", "cronminute", false)]
            public long cron_minute_id
            {
                get
                {
                    long _cron_minute_id;

                             _cron_minute_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("cron_minute_id"))
                        {
                        _cron_minute_id = Convert.ToInt64(this["cron_minute_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_minute_id: {e.Message}");
                        _cron_minute_id = default(long);
                    }
                    return _cron_minute_id;
                }
                set
                {
                   
                    this["cron_minute_id"] = value;
                }
            }
            
            [ColumnInfo("Hour", "CronHour", "enum", "cron_hour", "cronhour", false)]
            public long cron_hour_id
            {
                get
                {
                    long _cron_hour_id;

                             _cron_hour_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("cron_hour_id"))
                        {
                        _cron_hour_id = Convert.ToInt64(this["cron_hour_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_hour_id: {e.Message}");
                        _cron_hour_id = default(long);
                    }
                    return _cron_hour_id;
                }
                set
                {
                   
                    this["cron_hour_id"] = value;
                }
            }
            
            [ColumnInfo("Day Of Month", "CronDom", "enum", "cron_dom", "crondom", false)]
            public long cron_dom_id
            {
                get
                {
                    long _cron_dom_id;

                             _cron_dom_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("cron_dom_id"))
                        {
                        _cron_dom_id = Convert.ToInt64(this["cron_dom_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_dom_id: {e.Message}");
                        _cron_dom_id = default(long);
                    }
                    return _cron_dom_id;
                }
                set
                {
                   
                    this["cron_dom_id"] = value;
                }
            }
            
            [ColumnInfo("Month", "CronMonth", "enum", "cron_month", "cronmonth", false)]
            public long cron_month_id
            {
                get
                {
                    long _cron_month_id;

                             _cron_month_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("cron_month_id"))
                        {
                        _cron_month_id = Convert.ToInt64(this["cron_month_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_month_id: {e.Message}");
                        _cron_month_id = default(long);
                    }
                    return _cron_month_id;
                }
                set
                {
                   
                    this["cron_month_id"] = value;
                }
            }
            
            [ColumnInfo("Day Of Week", "CronDow", "enum", "cron_dow", "crondow", false)]
            public long cron_dow_id
            {
                get
                {
                    long _cron_dow_id;

                             _cron_dow_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("cron_dow_id"))
                        {
                        _cron_dow_id = Convert.ToInt64(this["cron_dow_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_dow_id: {e.Message}");
                        _cron_dow_id = default(long);
                    }
                    return _cron_dow_id;
                }
                set
                {
                   
                    this["cron_dow_id"] = value;
                }
            }
            
            [ColumnInfo("Schedule Label", "", "", "", "", false)]
            public string schedule_label
            {
                get
                {
                    string _schedule_label;

                            _schedule_label = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("schedule_label"))
                        {
                        _schedule_label = Convert.ToString(this["schedule_label"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting schedule_label: {e.Message}");
                        _schedule_label = default(string);
                    }
                    return _schedule_label;
                }
                set
                {
                   
                    this["schedule_label"] = value;
                }
            }
            
            [ColumnInfo("Next Run Time", "", "", "", "", false)]
            public DateTime next_run_time
            {
                get
                {
                    DateTime _next_run_time;

                             _next_run_time = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("next_run_time"))
                        {
                        _next_run_time = Convert.ToDateTime(this["next_run_time"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting next_run_time: {e.Message}");
                        _next_run_time = default(DateTime);
                    }
                    return _next_run_time;
                }
                set
                {
                   
                    this["next_run_time"] = value;
                }
            }
            
            [ColumnInfo("Last Run Time", "", "", "", "", false)]
            public DateTime last_run_time
            {
                get
                {
                    DateTime _last_run_time;

                             _last_run_time = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("last_run_time"))
                        {
                        _last_run_time = Convert.ToDateTime(this["last_run_time"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_run_time: {e.Message}");
                        _last_run_time = default(DateTime);
                    }
                    return _last_run_time;
                }
                set
                {
                   
                    this["last_run_time"] = value;
                }
            }
            
            [ColumnInfo("Active", "", "", "", "", true)]
            public int is_active
            {
                get
                {
                    int _is_active;

                             _is_active = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("is_active"))
                        {
                        _is_active = Convert.ToInt32(this["is_active"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting is_active: {e.Message}");
                        _is_active = default(int);
                    }
                    return _is_active;
                }
                set
                {
                   
                    this["is_active"] = value;
                }
            }
            
            [ColumnInfo("Created By", "", "", "", "", true)]
            public string created_by
            {
                get
                {
                    string _created_by;

                            _created_by = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("created_by"))
                        {
                        _created_by = Convert.ToString(this["created_by"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting created_by: {e.Message}");
                        _created_by = default(string);
                    }
                    return _created_by;
                }
                set
                {
                   
                    this["created_by"] = value;
                }
            }
            
            [ColumnInfo("Last Updated", "", "", "", "", true)]
            public DateTime last_updated
            {
                get
                {
                    DateTime _last_updated;

                             _last_updated = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("last_updated"))
                        {
                        _last_updated = Convert.ToDateTime(this["last_updated"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_updated: {e.Message}");
                        _last_updated = default(DateTime);
                    }
                    return _last_updated;
                }
                set
                {
                   
                    this["last_updated"] = value;
                }
            }
            
            [ColumnInfo("Last Updated By", "", "", "", "", true)]
            public string last_updated_by
            {
                get
                {
                    string _last_updated_by;

                            _last_updated_by = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("last_updated_by"))
                        {
                        _last_updated_by = Convert.ToString(this["last_updated_by"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_updated_by: {e.Message}");
                        _last_updated_by = default(string);
                    }
                    return _last_updated_by;
                }
                set
                {
                   
                    this["last_updated_by"] = value;
                }
            }
            
            [ColumnInfo("Txn Id", "", "", "", "", true)]
            public long txn_id
            {
                get
                {
                    long _txn_id;

                             _txn_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("txn_id"))
                        {
                        _txn_id = Convert.ToInt64(this["txn_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting txn_id: {e.Message}");
                        _txn_id = default(long);
                    }
                    return _txn_id;
                }
                set
                {
                   
                    this["txn_id"] = value;
                }
            }
                }

    public partial class ScheduleHistory : Schedule
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "ScheduleHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class ScheduleView : Schedule
    {

            [ColumnInfo("Run At", "", "", "", "", false)]
            public string cron_every_name
            {
                get
                {
                    string _cron_every_name;

                            _cron_every_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("cron_every_name"))
                        {
                        _cron_every_name = Convert.ToString(this["cron_every_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_every_name: {e.Message}");
                        _cron_every_name = default(string);
                    }
                    return _cron_every_name;
                }
                set
                {
                   
                    this["cron_every_name"] = value;
                }
            }
            
            [ColumnInfo("Minute", "", "", "", "", false)]
            public string cron_minute_name
            {
                get
                {
                    string _cron_minute_name;

                            _cron_minute_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("cron_minute_name"))
                        {
                        _cron_minute_name = Convert.ToString(this["cron_minute_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_minute_name: {e.Message}");
                        _cron_minute_name = default(string);
                    }
                    return _cron_minute_name;
                }
                set
                {
                   
                    this["cron_minute_name"] = value;
                }
            }
            
            [ColumnInfo("Hour", "", "", "", "", false)]
            public string cron_hour_name
            {
                get
                {
                    string _cron_hour_name;

                            _cron_hour_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("cron_hour_name"))
                        {
                        _cron_hour_name = Convert.ToString(this["cron_hour_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_hour_name: {e.Message}");
                        _cron_hour_name = default(string);
                    }
                    return _cron_hour_name;
                }
                set
                {
                   
                    this["cron_hour_name"] = value;
                }
            }
            
            [ColumnInfo("Day Of Month", "", "", "", "", false)]
            public string cron_dom_name
            {
                get
                {
                    string _cron_dom_name;

                            _cron_dom_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("cron_dom_name"))
                        {
                        _cron_dom_name = Convert.ToString(this["cron_dom_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_dom_name: {e.Message}");
                        _cron_dom_name = default(string);
                    }
                    return _cron_dom_name;
                }
                set
                {
                   
                    this["cron_dom_name"] = value;
                }
            }
            
            [ColumnInfo("Month", "", "", "", "", false)]
            public string cron_month_name
            {
                get
                {
                    string _cron_month_name;

                            _cron_month_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("cron_month_name"))
                        {
                        _cron_month_name = Convert.ToString(this["cron_month_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_month_name: {e.Message}");
                        _cron_month_name = default(string);
                    }
                    return _cron_month_name;
                }
                set
                {
                   
                    this["cron_month_name"] = value;
                }
            }
            
            [ColumnInfo("Day Of Week", "", "", "", "", false)]
            public string cron_dow_name
            {
                get
                {
                    string _cron_dow_name;

                            _cron_dow_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("cron_dow_name"))
                        {
                        _cron_dow_name = Convert.ToString(this["cron_dow_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting cron_dow_name: {e.Message}");
                        _cron_dow_name = default(string);
                    }
                    return _cron_dow_name;
                }
                set
                {
                   
                    this["cron_dow_name"] = value;
                }
            }
                }
        }
