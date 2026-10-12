

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Principal")]
    public partial class Principal : BaseObject
    {
        public Principal(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "Principal";
            tableName = "core.principal";
            schemaName = "core";
            tableBaseName = "principal";
            auditTableName = "core.principal"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("email");
            
            rwk.Add("enabled");
            

            _defaults["id"] = default(long);
            
            _defaults["first_name"] = default(string);
            
            _defaults["last_name"] = default(string);
            
            _defaults["username"] = default(string);
            
            _defaults["email"] = default(string);
            
            _defaults["enabled"] = default(int);
            
            _defaults["created_date"] = default(DateTime);
            
            _defaults["last_login_date"] = default(DateTime);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Principal ID", "", "", "", "", false)]
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
            
            [ColumnInfo("First", "", "", "", "", false)]
            public string first_name
            {
                get
                {
                    string _first_name;

                            _first_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("first_name"))
                        {
                        _first_name = Convert.ToString(this["first_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting first_name: {e.Message}");
                        _first_name = default(string);
                    }
                    return _first_name;
                }
                set
                {
                   
                    this["first_name"] = value;
                }
            }
            
            [ColumnInfo("Last", "", "", "", "", false)]
            public string last_name
            {
                get
                {
                    string _last_name;

                            _last_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("last_name"))
                        {
                        _last_name = Convert.ToString(this["last_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_name: {e.Message}");
                        _last_name = default(string);
                    }
                    return _last_name;
                }
                set
                {
                   
                    this["last_name"] = value;
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
            
            [ColumnInfo("Email", "", "", "", "", false)]
            public string email
            {
                get
                {
                    string _email;

                            _email = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("email"))
                        {
                        _email = Convert.ToString(this["email"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting email: {e.Message}");
                        _email = default(string);
                    }
                    return _email;
                }
                set
                {
                   
                    this["email"] = value;
                }
            }
            
            [ColumnInfo("Enabled", "", "", "", "", false)]
            public int enabled
            {
                get
                {
                    int _enabled;

                             _enabled = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("enabled"))
                        {
                        _enabled = Convert.ToInt32(this["enabled"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting enabled: {e.Message}");
                        _enabled = default(int);
                    }
                    return _enabled;
                }
                set
                {
                   
                    this["enabled"] = value;
                }
            }
            
            [ColumnInfo("Created", "", "", "", "", false)]
            public DateTime created_date
            {
                get
                {
                    DateTime _created_date;

                             _created_date = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("created_date"))
                        {
                        _created_date = Convert.ToDateTime(this["created_date"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting created_date: {e.Message}");
                        _created_date = default(DateTime);
                    }
                    return _created_date;
                }
                set
                {
                   
                    this["created_date"] = value;
                }
            }
            
            [ColumnInfo("Last Login", "", "", "", "", false)]
            public DateTime last_login_date
            {
                get
                {
                    DateTime _last_login_date;

                             _last_login_date = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("last_login_date"))
                        {
                        _last_login_date = Convert.ToDateTime(this["last_login_date"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting last_login_date: {e.Message}");
                        _last_login_date = default(DateTime);
                    }
                    return _last_login_date;
                }
                set
                {
                   
                    this["last_login_date"] = value;
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

    public partial class PrincipalHistory : Principal
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "PrincipalHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class PrincipalView : Principal
    {
    }
        }
