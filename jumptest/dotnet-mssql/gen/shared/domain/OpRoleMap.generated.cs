

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Operation Group Map")]
    public partial class OpRoleMap : BaseObject
    {
        public OpRoleMap(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "OpRoleMap";
            tableName = "core.op_role_map";
            schemaName = "core";
            tableBaseName = "op_role_map";
            auditTableName = "core.op_role_map"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("op_id");
            
            rwk.Add("op_role_id");
            

            _defaults["id"] = default(long);
            
            _defaults["op_id"] = default(long);
            
            _defaults["op_role_id"] = default(long);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Operation Role Map ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Operation ID", "Operation", "map", "operation", "operation", false)]
            public long op_id
            {
                get
                {
                    long _op_id;

                             _op_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("op_id"))
                        {
                        _op_id = Convert.ToInt64(this["op_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting op_id: {e.Message}");
                        _op_id = default(long);
                    }
                    return _op_id;
                }
                set
                {
                   
                    this["op_id"] = value;
                }
            }
            
            [ColumnInfo("Role ID", "OpRole", "map", "op_role", "oprole", false)]
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

    public partial class OpRoleMapHistory : OpRoleMap
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "OpRoleMapHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class OpRoleMapView : OpRoleMap
    {

            [ColumnInfo("Operation ID Object", "", "", "", "", false)]
            public string op_objectname
            {
                get
                {
                    string _op_objectname;

                            _op_objectname = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("op_objectname"))
                        {
                        _op_objectname = Convert.ToString(this["op_objectname"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting op_objectname: {e.Message}");
                        _op_objectname = default(string);
                    }
                    return _op_objectname;
                }
                set
                {
                   
                    this["op_objectname"] = value;
                }
            }
            
            [ColumnInfo("Operation ID Method", "", "", "", "", false)]
            public string op_methodname
            {
                get
                {
                    string _op_methodname;

                            _op_methodname = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("op_methodname"))
                        {
                        _op_methodname = Convert.ToString(this["op_methodname"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting op_methodname: {e.Message}");
                        _op_methodname = default(string);
                    }
                    return _op_methodname;
                }
                set
                {
                   
                    this["op_methodname"] = value;
                }
            }
            
            [ColumnInfo("Role ID", "", "", "", "", false)]
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
