

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Main Menu")]
    public partial class NavMenu : BaseObject
    {
        public NavMenu(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "NavMenu";
            tableName = "core.nav_menu";
            schemaName = "core";
            tableBaseName = "nav_menu";
            auditTableName = "core.nav_menu"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("name");
            

            _defaults["id"] = default(long);
            
            _defaults["parent_id"] = default(long);
            
            _defaults["ordinal"] = default(int);
            
            _defaults["name"] = default(string);
            
            _defaults["link"] = default(string);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Nav Menu ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Parent Menu ID", "NavMenu", "parent", "nav_menu", "navmenu", false)]
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
            
            [ColumnInfo("Ordinal", "", "", "", "", false)]
            public int ordinal
            {
                get
                {
                    int _ordinal;

                             _ordinal = default(int);
                                                 
                    try
                    {
                        if(this.ContainsKey("ordinal"))
                        {
                        _ordinal = Convert.ToInt32(this["ordinal"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting ordinal: {e.Message}");
                        _ordinal = default(int);
                    }
                    return _ordinal;
                }
                set
                {
                   
                    this["ordinal"] = value;
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
            
            [ColumnInfo("Link", "", "", "", "", false)]
            public string link
            {
                get
                {
                    string _link;

                            _link = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("link"))
                        {
                        _link = Convert.ToString(this["link"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting link: {e.Message}");
                        _link = default(string);
                    }
                    return _link;
                }
                set
                {
                   
                    this["link"] = value;
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

    public partial class NavMenuHistory : NavMenu
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "NavMenuHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class NavMenuView : NavMenu
    {

            [ColumnInfo("Parent Menu ID", "", "", "", "", false)]
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
                }
        }
