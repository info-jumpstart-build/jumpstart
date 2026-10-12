

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Workflow")]
    public partial class Workflow : BaseObject
    {
        public Workflow(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "Workflow";
            tableName = "core.workflow";
            schemaName = "core";
            tableBaseName = "workflow";
            auditTableName = "core.workflow"; // history rows live in the same table (is_active=0)
            isAudited = false;


            rwk.Add("parent_id");
            
            rwk.Add("name");
            

            _defaults["id"] = default(long);
            
            _defaults["workflow_type_id"] = default(long);
            
            _defaults["parent_id"] = default(long);
            
            _defaults["name"] = default(string);
            
            _defaults["seq"] = default(int);
            
            _defaults["server_node_id"] = default(long);
            
            _defaults["process_id"] = default(long);
            
            _defaults["exec_status_id"] = default(long);
            
            _defaults["last_start_time"] = default(DateTime);
            
            _defaults["last_end_time"] = default(DateTime);
            
            _defaults["schedule_id"] = default(long);
            
            _defaults["on_failure_action_id"] = default(long);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Workflow ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Type", "WorkflowType", "enum", "workflow_type", "workflowtype", false)]
            public long workflow_type_id
            {
                get
                {
                    long _workflow_type_id;

                             _workflow_type_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("workflow_type_id"))
                        {
                        _workflow_type_id = Convert.ToInt64(this["workflow_type_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting workflow_type_id: {e.Message}");
                        _workflow_type_id = default(long);
                    }
                    return _workflow_type_id;
                }
                set
                {
                   
                    this["workflow_type_id"] = value;
                }
            }
            
            [ColumnInfo("Parent", "Workflow", "parent", "workflow", "workflow", false)]
            public long parent_id
            {
                get
                {
                    long _parent_id;

                             _parent_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("parent_id"))
                        {
                        _parent_id = Convert.ToInt64(this["parent_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting parent_id: {e.Message}");
                        _parent_id = default(long);
                    }
                    return _parent_id;
                }
                set
                {
                   
                    this["parent_id"] = value;
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
            
            [ColumnInfo("Sequence", "", "", "", "", false)]
            public int seq
            {
                get
                {
                    int _seq;

                             _seq = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("seq"))
                        {
                        _seq = Convert.ToInt32(this["seq"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting seq: {e.Message}");
                        _seq = default(int);
                    }
                    return _seq;
                }
                set
                {
                   
                    this["seq"] = value;
                }
            }
            
            [ColumnInfo("Agent", "ServerNode", "enum", "server_node", "servernode", false)]
            public long server_node_id
            {
                get
                {
                    long _server_node_id;

                             _server_node_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("server_node_id"))
                        {
                        _server_node_id = Convert.ToInt64(this["server_node_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_id: {e.Message}");
                        _server_node_id = default(long);
                    }
                    return _server_node_id;
                }
                set
                {
                   
                    this["server_node_id"] = value;
                }
            }
            
            [ColumnInfo("Process", "Process", "enum", "process", "process", false)]
            public long process_id
            {
                get
                {
                    long _process_id;

                             _process_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("process_id"))
                        {
                        _process_id = Convert.ToInt64(this["process_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting process_id: {e.Message}");
                        _process_id = default(long);
                    }
                    return _process_id;
                }
                set
                {
                   
                    this["process_id"] = value;
                }
            }
            
            [ColumnInfo("Status", "ExecStatus", "enum", "exec_status", "execstatus", false)]
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
            
            [ColumnInfo("Last Start Time", "", "", "", "", false)]
            public DateTime last_start_time
            {
                get
                {
                    DateTime _last_start_time;

                             _last_start_time = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("last_start_time"))
                        {
                        _last_start_time = Convert.ToDateTime(this["last_start_time"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_start_time: {e.Message}");
                        _last_start_time = default(DateTime);
                    }
                    return _last_start_time;
                }
                set
                {
                   
                    this["last_start_time"] = value;
                }
            }
            
            [ColumnInfo("LastEnd Time", "", "", "", "", false)]
            public DateTime last_end_time
            {
                get
                {
                    DateTime _last_end_time;

                             _last_end_time = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("last_end_time"))
                        {
                        _last_end_time = Convert.ToDateTime(this["last_end_time"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_end_time: {e.Message}");
                        _last_end_time = default(DateTime);
                    }
                    return _last_end_time;
                }
                set
                {
                   
                    this["last_end_time"] = value;
                }
            }
            
            [ColumnInfo("Schedule", "Schedule", "enum", "schedule", "schedule", false)]
            public long schedule_id
            {
                get
                {
                    long _schedule_id;

                             _schedule_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("schedule_id"))
                        {
                        _schedule_id = Convert.ToInt64(this["schedule_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting schedule_id: {e.Message}");
                        _schedule_id = default(long);
                    }
                    return _schedule_id;
                }
                set
                {
                   
                    this["schedule_id"] = value;
                }
            }
            
            [ColumnInfo("On Failure", "OnFailure", "enum", "on_failure", "onfailure", false)]
            public long on_failure_action_id
            {
                get
                {
                    long _on_failure_action_id;

                             _on_failure_action_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("on_failure_action_id"))
                        {
                        _on_failure_action_id = Convert.ToInt64(this["on_failure_action_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting on_failure_action_id: {e.Message}");
                        _on_failure_action_id = default(long);
                    }
                    return _on_failure_action_id;
                }
                set
                {
                   
                    this["on_failure_action_id"] = value;
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

    public partial class WorkflowHistory : Workflow
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "WorkflowHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class WorkflowView : Workflow
    {

            [ColumnInfo("Type", "", "", "", "", false)]
            public string workflow_type_name
            {
                get
                {
                    string _workflow_type_name;

                            _workflow_type_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("workflow_type_name"))
                        {
                        _workflow_type_name = Convert.ToString(this["workflow_type_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting workflow_type_name: {e.Message}");
                        _workflow_type_name = default(string);
                    }
                    return _workflow_type_name;
                }
                set
                {
                   
                    this["workflow_type_name"] = value;
                }
            }
            
            [ColumnInfo("Parent Parent", "Workflow", "parent", "workflow", "workflow", false)]
            public long parent_parent_id
            {
                get
                {
                    long _parent_parent_id;

                             _parent_parent_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("parent_parent_id"))
                        {
                        _parent_parent_id = Convert.ToInt64(this["parent_parent_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting parent_parent_id: {e.Message}");
                        _parent_parent_id = default(long);
                    }
                    return _parent_parent_id;
                }
                set
                {
                   
                    this["parent_parent_id"] = value;
                }
            }
            
            [ColumnInfo("Parent", "", "", "", "", false)]
            public string parent_name
            {
                get
                {
                    string _parent_name;

                            _parent_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("parent_name"))
                        {
                        _parent_name = Convert.ToString(this["parent_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting parent_name: {e.Message}");
                        _parent_name = default(string);
                    }
                    return _parent_name;
                }
                set
                {
                   
                    this["parent_name"] = value;
                }
            }
            
            [ColumnInfo("Agent Hostname", "", "", "", "", false)]
            public string server_node_hostname
            {
                get
                {
                    string _server_node_hostname;

                            _server_node_hostname = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("server_node_hostname"))
                        {
                        _server_node_hostname = Convert.ToString(this["server_node_hostname"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_hostname: {e.Message}");
                        _server_node_hostname = default(string);
                    }
                    return _server_node_hostname;
                }
                set
                {
                   
                    this["server_node_hostname"] = value;
                }
            }
            
            [ColumnInfo("Agent Port", "", "", "", "", false)]
            public int server_node_port
            {
                get
                {
                    int _server_node_port;

                             _server_node_port = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("server_node_port"))
                        {
                        _server_node_port = Convert.ToInt32(this["server_node_port"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_port: {e.Message}");
                        _server_node_port = default(int);
                    }
                    return _server_node_port;
                }
                set
                {
                   
                    this["server_node_port"] = value;
                }
            }
            
            [ColumnInfo("Process", "", "", "", "", false)]
            public string process_name
            {
                get
                {
                    string _process_name;

                            _process_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("process_name"))
                        {
                        _process_name = Convert.ToString(this["process_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting process_name: {e.Message}");
                        _process_name = default(string);
                    }
                    return _process_name;
                }
                set
                {
                   
                    this["process_name"] = value;
                }
            }
            
            [ColumnInfo("Status", "", "", "", "", false)]
            public string exec_status_name
            {
                get
                {
                    string _exec_status_name;

                            _exec_status_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("exec_status_name"))
                        {
                        _exec_status_name = Convert.ToString(this["exec_status_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting exec_status_name: {e.Message}");
                        _exec_status_name = default(string);
                    }
                    return _exec_status_name;
                }
                set
                {
                   
                    this["exec_status_name"] = value;
                }
            }
            
            [ColumnInfo("Status Image", "", "", "", "", false)]
            public string exec_status_image
            {
                get
                {
                    string _exec_status_image;

                            _exec_status_image = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("exec_status_image"))
                        {
                        _exec_status_image = Convert.ToString(this["exec_status_image"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting exec_status_image: {e.Message}");
                        _exec_status_image = default(string);
                    }
                    return _exec_status_image;
                }
                set
                {
                   
                    this["exec_status_image"] = value;
                }
            }
            
            [ColumnInfo("Schedule", "", "", "", "", false)]
            public string schedule_name
            {
                get
                {
                    string _schedule_name;

                            _schedule_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("schedule_name"))
                        {
                        _schedule_name = Convert.ToString(this["schedule_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting schedule_name: {e.Message}");
                        _schedule_name = default(string);
                    }
                    return _schedule_name;
                }
                set
                {
                   
                    this["schedule_name"] = value;
                }
            }
            
            [ColumnInfo("On Failure Action", "", "", "", "", false)]
            public string on_failure_action_action
            {
                get
                {
                    string _on_failure_action_action;

                            _on_failure_action_action = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("on_failure_action_action"))
                        {
                        _on_failure_action_action = Convert.ToString(this["on_failure_action_action"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting on_failure_action_action: {e.Message}");
                        _on_failure_action_action = default(string);
                    }
                    return _on_failure_action_action;
                }
                set
                {
                   
                    this["on_failure_action_action"] = value;
                }
            }
                }
        }
