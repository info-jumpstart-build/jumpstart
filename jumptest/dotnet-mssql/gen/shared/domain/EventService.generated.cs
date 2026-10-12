

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Events")]
    public partial class EventService : BaseObject
    {
        public EventService(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "EventService";
            tableName = "core.event_service";
            schemaName = "core";
            tableBaseName = "event_service";
            auditTableName = "core.event_service"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("event_type");
            
            rwk.Add("objectname_filter");
            
            rwk.Add("methodname_filter");
            
            rwk.Add("script_id");
            

            _defaults["id"] = default(long);
            
            _defaults["event_type"] = default(string);
            
            _defaults["objectname_filter"] = default(string);
            
            _defaults["methodname_filter"] = default(string);
            
            _defaults["script_id"] = default(long);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Event ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Event Type", "", "", "", "", false)]
            public string event_type
            {
                get
                {
                    string _event_type;

                            _event_type = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("event_type"))
                        {
                        _event_type = Convert.ToString(this["event_type"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting event_type: {e.Message}");
                        _event_type = default(string);
                    }
                    return _event_type;
                }
                set
                {
                   
                    this["event_type"] = value;
                }
            }
            
            [ColumnInfo("Object Filter", "", "", "", "", false)]
            public string objectname_filter
            {
                get
                {
                    string _objectname_filter;

                            _objectname_filter = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("objectname_filter"))
                        {
                        _objectname_filter = Convert.ToString(this["objectname_filter"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting objectname_filter: {e.Message}");
                        _objectname_filter = default(string);
                    }
                    return _objectname_filter;
                }
                set
                {
                   
                    this["objectname_filter"] = value;
                }
            }
            
            [ColumnInfo("Method Filter", "", "", "", "", false)]
            public string methodname_filter
            {
                get
                {
                    string _methodname_filter;

                            _methodname_filter = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("methodname_filter"))
                        {
                        _methodname_filter = Convert.ToString(this["methodname_filter"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting methodname_filter: {e.Message}");
                        _methodname_filter = default(string);
                    }
                    return _methodname_filter;
                }
                set
                {
                   
                    this["methodname_filter"] = value;
                }
            }
            
            [ColumnInfo("Script ID", "Script", "enum", "script", "script", false)]
            public long script_id
            {
                get
                {
                    long _script_id;

                             _script_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("script_id"))
                        {
                        _script_id = Convert.ToInt64(this["script_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting script_id: {e.Message}");
                        _script_id = default(long);
                    }
                    return _script_id;
                }
                set
                {
                   
                    this["script_id"] = value;
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

    public partial class EventServiceHistory : EventService
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "EventServiceHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class EventServiceView : EventService
    {

            [ColumnInfo("Script ID", "", "", "", "", false)]
            public string script_name
            {
                get
                {
                    string _script_name;

                            _script_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("script_name"))
                        {
                        _script_name = Convert.ToString(this["script_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting script_name: {e.Message}");
                        _script_name = default(string);
                    }
                    return _script_name;
                }
                set
                {
                   
                    this["script_name"] = value;
                }
            }
            
            [ColumnInfo("Script ID Script Type", "ScriptType", "enum", "script_type", "scripttype", false)]
            public long script_script_type_id
            {
                get
                {
                    long _script_script_type_id;

                             _script_script_type_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("script_script_type_id"))
                        {
                        _script_script_type_id = Convert.ToInt64(this["script_script_type_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting script_script_type_id: {e.Message}");
                        _script_script_type_id = default(long);
                    }
                    return _script_script_type_id;
                }
                set
                {
                   
                    this["script_script_type_id"] = value;
                }
            }
                }
        }
