

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Execution Log")]
    public partial class ExecLog : BaseObject
    {
        public ExecLog(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "ExecLog";
            tableName = "core.exec_log";
            schemaName = "core";
            tableBaseName = "exec_log";
            auditTableName = "core.exec_log"; // history rows live in the same table (is_active=0)
            isAudited = false;


            rwk.Add("token");
            
            rwk.Add("workflow_id");
            
            rwk.Add("start_time");
            
            rwk.Add("end_time");
            

            _defaults["id"] = default(long);
            
            _defaults["token"] = default(string);
            
            _defaults["workflow_id"] = default(long);
            
            _defaults["start_time"] = default(DateTime);
            
            _defaults["end_time"] = default(DateTime);
            
            _defaults["exec_status_id"] = default(long);
            
            _defaults["stdout"] = default(string);
            
            _defaults["stderr"] = default(string);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Execution ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Token", "", "", "", "", false)]
            public string token
            {
                get
                {
                    string _token;

                            _token = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("token"))
                        {
                        _token = Convert.ToString(this["token"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting token: {e.Message}");
                        _token = default(string);
                    }
                    return _token;
                }
                set
                {
                   
                    this["token"] = value;
                }
            }
            
            [ColumnInfo("Process", "Workflow", "parent", "workflow", "workflow", false)]
            public long workflow_id
            {
                get
                {
                    long _workflow_id;

                             _workflow_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("workflow_id"))
                        {
                        _workflow_id = Convert.ToInt64(this["workflow_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting workflow_id: {e.Message}");
                        _workflow_id = default(long);
                    }
                    return _workflow_id;
                }
                set
                {
                   
                    this["workflow_id"] = value;
                }
            }
            
            [ColumnInfo("Start Time", "", "", "", "", false)]
            public DateTime start_time
            {
                get
                {
                    DateTime _start_time;

                             _start_time = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("start_time"))
                        {
                        _start_time = Convert.ToDateTime(this["start_time"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting start_time: {e.Message}");
                        _start_time = default(DateTime);
                    }
                    return _start_time;
                }
                set
                {
                   
                    this["start_time"] = value;
                }
            }
            
            [ColumnInfo("End Time", "", "", "", "", false)]
            public DateTime end_time
            {
                get
                {
                    DateTime _end_time;

                             _end_time = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("end_time"))
                        {
                        _end_time = Convert.ToDateTime(this["end_time"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting end_time: {e.Message}");
                        _end_time = default(DateTime);
                    }
                    return _end_time;
                }
                set
                {
                   
                    this["end_time"] = value;
                }
            }
            
            [ColumnInfo("Status", "", "", "", "", false)]
            public long exec_status_id
            {
                get
                {
                    long _exec_status_id;

                             _exec_status_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("exec_status_id"))
                        {
                        _exec_status_id = Convert.ToInt64(this["exec_status_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting exec_status_id: {e.Message}");
                        _exec_status_id = default(long);
                    }
                    return _exec_status_id;
                }
                set
                {
                   
                    this["exec_status_id"] = value;
                }
            }
            
            [ColumnInfo("Stdout", "", "", "", "", false)]
            public string stdout
            {
                get
                {
                    string _stdout;

                            _stdout = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("stdout"))
                        {
                        _stdout = Convert.ToString(this["stdout"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting stdout: {e.Message}");
                        _stdout = default(string);
                    }
                    return _stdout;
                }
                set
                {
                   
                    this["stdout"] = value;
                }
            }
            
            [ColumnInfo("Stderr", "", "", "", "", false)]
            public string stderr
            {
                get
                {
                    string _stderr;

                            _stderr = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("stderr"))
                        {
                        _stderr = Convert.ToString(this["stderr"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting stderr: {e.Message}");
                        _stderr = default(string);
                    }
                    return _stderr;
                }
                set
                {
                   
                    this["stderr"] = value;
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

    public partial class ExecLogHistory : ExecLog
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "ExecLogHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class ExecLogView : ExecLog
    {

            [ColumnInfo("Process Parent", "Workflow", "parent", "workflow", "workflow", false)]
            public long workflow_parent_id
            {
                get
                {
                    long _workflow_parent_id;

                             _workflow_parent_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("workflow_parent_id"))
                        {
                        _workflow_parent_id = Convert.ToInt64(this["workflow_parent_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting workflow_parent_id: {e.Message}");
                        _workflow_parent_id = default(long);
                    }
                    return _workflow_parent_id;
                }
                set
                {
                   
                    this["workflow_parent_id"] = value;
                }
            }
            
            [ColumnInfo("Process", "", "", "", "", false)]
            public string workflow_name
            {
                get
                {
                    string _workflow_name;

                            _workflow_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("workflow_name"))
                        {
                        _workflow_name = Convert.ToString(this["workflow_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting workflow_name: {e.Message}");
                        _workflow_name = default(string);
                    }
                    return _workflow_name;
                }
                set
                {
                   
                    this["workflow_name"] = value;
                }
            }
                }
        }
