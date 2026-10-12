

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Operation Role Members")]
    public partial class OpRoleMember : BaseObject
    {
        public OpRoleMember(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "OpRoleMember";
            tableName = "core.op_role_member";
            schemaName = "core";
            tableBaseName = "op_role_member";
            auditTableName = "core.op_role_member"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("principal_id");
            
            rwk.Add("op_role_id");
            

            _defaults["id"] = default(long);
            
            _defaults["principal_id"] = default(long);
            
            _defaults["op_role_id"] = default(long);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Member ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Username", "Principal", "map", "principal", "principal", false)]
            public long principal_id
            {
                get
                {
                    long _principal_id;

                             _principal_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("principal_id"))
                        {
                        _principal_id = Convert.ToInt64(this["principal_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting principal_id: {e.Message}");
                        _principal_id = default(long);
                    }
                    return _principal_id;
                }
                set
                {
                   
                    this["principal_id"] = value;
                }
            }
            
            [ColumnInfo("Role", "OpRole", "map", "op_role", "oprole", false)]
            public long op_role_id
            {
                get
                {
                    long _op_role_id;

                             _op_role_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("op_role_id"))
                        {
                        _op_role_id = Convert.ToInt64(this["op_role_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting op_role_id: {e.Message}");
                        _op_role_id = default(long);
                    }
                    return _op_role_id;
                }
                set
                {
                   
                    this["op_role_id"] = value;
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

    public partial class OpRoleMemberHistory : OpRoleMember
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "OpRoleMemberHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class OpRoleMemberView : OpRoleMember
    {

            [ColumnInfo("Username Email", "", "", "", "", false)]
            public string principal_email
            {
                get
                {
                    string _principal_email;

                            _principal_email = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("principal_email"))
                        {
                        _principal_email = Convert.ToString(this["principal_email"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting principal_email: {e.Message}");
                        _principal_email = default(string);
                    }
                    return _principal_email;
                }
                set
                {
                   
                    this["principal_email"] = value;
                }
            }
            
            [ColumnInfo("Username Enabled", "", "", "", "", false)]
            public int principal_enabled
            {
                get
                {
                    int _principal_enabled;

                             _principal_enabled = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("principal_enabled"))
                        {
                        _principal_enabled = Convert.ToInt32(this["principal_enabled"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting principal_enabled: {e.Message}");
                        _principal_enabled = default(int);
                    }
                    return _principal_enabled;
                }
                set
                {
                   
                    this["principal_enabled"] = value;
                }
            }
            
            [ColumnInfo("Role", "", "", "", "", false)]
            public string op_role_name
            {
                get
                {
                    string _op_role_name;

                            _op_role_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("op_role_name"))
                        {
                        _op_role_name = Convert.ToString(this["op_role_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting op_role_name: {e.Message}");
                        _op_role_name = default(string);
                    }
                    return _op_role_name;
                }
                set
                {
                   
                    this["op_role_name"] = value;
                }
            }
                }
        }
