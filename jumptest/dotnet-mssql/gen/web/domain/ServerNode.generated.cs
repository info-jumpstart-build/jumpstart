

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Server Node")]
    public partial class ServerNode : BaseObject
    {
        public ServerNode(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "ServerNode";
            tableName = "core.server_node";
            schemaName = "core";
            tableBaseName = "server_node";
            auditTableName = "core.server_node"; // history rows live in the same table (is_active=0)
            isAudited = false;


            rwk.Add("hostname");
            
            rwk.Add("port");
            

            _defaults["id"] = default(long);
            
            _defaults["server_node_type_id"] = default(long);
            
            _defaults["hostname"] = default(string);
            
            _defaults["ip_address"] = default(string);
            
            _defaults["port"] = default(int);
            
            _defaults["username"] = default(string);
            
            _defaults["url"] = default(string);
            
            _defaults["user_domain"] = default(string);
            
            _defaults["os_name"] = default(string);
            
            _defaults["os_version"] = default(string);
            
            _defaults["architecture"] = default(string);
            
            _defaults["registered_at"] = default(DateTime);
            
            _defaults["server_node_status_id"] = default(long);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Server Node ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Server Node Type", "ServerNodeType", "enum", "server_node_type", "servernodetype", false)]
            public long server_node_type_id
            {
                get
                {
                    long _server_node_type_id;

                             _server_node_type_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("server_node_type_id"))
                        {
                        _server_node_type_id = Convert.ToInt64(this["server_node_type_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_type_id: {e.Message}");
                        _server_node_type_id = default(long);
                    }
                    return _server_node_type_id;
                }
                set
                {
                   
                    this["server_node_type_id"] = value;
                }
            }
            
            [ColumnInfo("Hostname", "", "", "", "", false)]
            public string hostname
            {
                get
                {
                    string _hostname;

                            _hostname = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("hostname"))
                        {
                        _hostname = Convert.ToString(this["hostname"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting hostname: {e.Message}");
                        _hostname = default(string);
                    }
                    return _hostname;
                }
                set
                {
                   
                    this["hostname"] = value;
                }
            }
            
            [ColumnInfo("Address", "", "", "", "", false)]
            public string ip_address
            {
                get
                {
                    string _ip_address;

                            _ip_address = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("ip_address"))
                        {
                        _ip_address = Convert.ToString(this["ip_address"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting ip_address: {e.Message}");
                        _ip_address = default(string);
                    }
                    return _ip_address;
                }
                set
                {
                   
                    this["ip_address"] = value;
                }
            }
            
            [ColumnInfo("Port", "", "", "", "", false)]
            public int port
            {
                get
                {
                    int _port;

                             _port = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("port"))
                        {
                        _port = Convert.ToInt32(this["port"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting port: {e.Message}");
                        _port = default(int);
                    }
                    return _port;
                }
                set
                {
                   
                    this["port"] = value;
                }
            }
            
            [ColumnInfo("Username", "", "", "", "", false)]
            public string username
            {
                get
                {
                    string _username;

                            _username = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("username"))
                        {
                        _username = Convert.ToString(this["username"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting username: {e.Message}");
                        _username = default(string);
                    }
                    return _username;
                }
                set
                {
                   
                    this["username"] = value;
                }
            }
            
            [ColumnInfo("URL", "", "", "", "", false)]
            public string url
            {
                get
                {
                    string _url;

                            _url = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("url"))
                        {
                        _url = Convert.ToString(this["url"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting url: {e.Message}");
                        _url = default(string);
                    }
                    return _url;
                }
                set
                {
                   
                    this["url"] = value;
                }
            }
            
            [ColumnInfo("User Domain", "", "", "", "", false)]
            public string user_domain
            {
                get
                {
                    string _user_domain;

                            _user_domain = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("user_domain"))
                        {
                        _user_domain = Convert.ToString(this["user_domain"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting user_domain: {e.Message}");
                        _user_domain = default(string);
                    }
                    return _user_domain;
                }
                set
                {
                   
                    this["user_domain"] = value;
                }
            }
            
            [ColumnInfo("OS Name", "", "", "", "", false)]
            public string os_name
            {
                get
                {
                    string _os_name;

                            _os_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("os_name"))
                        {
                        _os_name = Convert.ToString(this["os_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting os_name: {e.Message}");
                        _os_name = default(string);
                    }
                    return _os_name;
                }
                set
                {
                   
                    this["os_name"] = value;
                }
            }
            
            [ColumnInfo("OS Version", "", "", "", "", false)]
            public string os_version
            {
                get
                {
                    string _os_version;

                            _os_version = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("os_version"))
                        {
                        _os_version = Convert.ToString(this["os_version"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting os_version: {e.Message}");
                        _os_version = default(string);
                    }
                    return _os_version;
                }
                set
                {
                   
                    this["os_version"] = value;
                }
            }
            
            [ColumnInfo("Architecture", "", "", "", "", false)]
            public string architecture
            {
                get
                {
                    string _architecture;

                            _architecture = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("architecture"))
                        {
                        _architecture = Convert.ToString(this["architecture"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting architecture: {e.Message}");
                        _architecture = default(string);
                    }
                    return _architecture;
                }
                set
                {
                   
                    this["architecture"] = value;
                }
            }
            
            [ColumnInfo("Registered At", "", "", "", "", false)]
            public DateTime registered_at
            {
                get
                {
                    DateTime _registered_at;

                             _registered_at = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("registered_at"))
                        {
                        _registered_at = Convert.ToDateTime(this["registered_at"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting registered_at: {e.Message}");
                        _registered_at = default(DateTime);
                    }
                    return _registered_at;
                }
                set
                {
                   
                    this["registered_at"] = value;
                }
            }
            
            [ColumnInfo("Status", "ServerNodeStatus", "enum", "server_node_status", "servernodestatus", false)]
            public long server_node_status_id
            {
                get
                {
                    long _server_node_status_id;

                             _server_node_status_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("server_node_status_id"))
                        {
                        _server_node_status_id = Convert.ToInt64(this["server_node_status_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_status_id: {e.Message}");
                        _server_node_status_id = default(long);
                    }
                    return _server_node_status_id;
                }
                set
                {
                   
                    this["server_node_status_id"] = value;
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

    public partial class ServerNodeHistory : ServerNode
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "ServerNodeHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class ServerNodeView : ServerNode
    {

            [ColumnInfo("Server Node Type", "", "", "", "", false)]
            public string server_node_type_name
            {
                get
                {
                    string _server_node_type_name;

                            _server_node_type_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("server_node_type_name"))
                        {
                        _server_node_type_name = Convert.ToString(this["server_node_type_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_type_name: {e.Message}");
                        _server_node_type_name = default(string);
                    }
                    return _server_node_type_name;
                }
                set
                {
                   
                    this["server_node_type_name"] = value;
                }
            }
            
            [ColumnInfo("Status", "", "", "", "", false)]
            public string server_node_status_name
            {
                get
                {
                    string _server_node_status_name;

                            _server_node_status_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("server_node_status_name"))
                        {
                        _server_node_status_name = Convert.ToString(this["server_node_status_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting server_node_status_name: {e.Message}");
                        _server_node_status_name = default(string);
                    }
                    return _server_node_status_name;
                }
                set
                {
                   
                    this["server_node_status_name"] = value;
                }
            }
                }
        }
