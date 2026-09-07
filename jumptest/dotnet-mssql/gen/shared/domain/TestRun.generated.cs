

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Test Run")]
    public partial class TestRun : BaseObject
    {
        public TestRun(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "TestRun";
            tableName = "app.test_run";
            schemaName = "app";
            tableBaseName = "test_run";
            auditTableName = "app.test_run"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("name");
            
            rwk.Add("test_plan_id");
            

            _defaults["id"] = default(long);
            
            _defaults["name"] = default(string);
            
            _defaults["test_plan_id"] = default(long);
            
            _defaults["run_at"] = default(DateTime);
            
            _defaults["run_by"] = default(string);
            
            _defaults["notes"] = default(string);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Test Run ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Test Plan", "TestPlan", "parent", "test_plan", "testplan", false)]
            public long test_plan_id
            {
                get
                {
                    long _test_plan_id;

                             _test_plan_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("test_plan_id"))
                        {
                        _test_plan_id = Convert.ToInt64(this["test_plan_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_plan_id: {e.Message}");
                        _test_plan_id = default(long);
                    }
                    return _test_plan_id;
                }
                set
                {
                   
                    this["test_plan_id"] = value;
                }
            }
            
            [ColumnInfo("Run At", "", "", "", "", false)]
            public DateTime run_at
            {
                get
                {
                    DateTime _run_at;

                             _run_at = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("run_at"))
                        {
                        _run_at = Convert.ToDateTime(this["run_at"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting run_at: {e.Message}");
                        _run_at = default(DateTime);
                    }
                    return _run_at;
                }
                set
                {
                   
                    this["run_at"] = value;
                }
            }
            
            [ColumnInfo("Run By", "", "", "", "", false)]
            public string run_by
            {
                get
                {
                    string _run_by;

                            _run_by = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("run_by"))
                        {
                        _run_by = Convert.ToString(this["run_by"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting run_by: {e.Message}");
                        _run_by = default(string);
                    }
                    return _run_by;
                }
                set
                {
                   
                    this["run_by"] = value;
                }
            }
            
            [ColumnInfo("Notes", "", "", "", "", false)]
            public string notes
            {
                get
                {
                    string _notes;

                            _notes = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("notes"))
                        {
                        _notes = Convert.ToString(this["notes"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting notes: {e.Message}");
                        _notes = default(string);
                    }
                    return _notes;
                }
                set
                {
                   
                    this["notes"] = value;
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

    public partial class TestRunHistory : TestRun
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "TestRunHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class TestRunView : TestRun
    {

            [ColumnInfo("Test Plan", "", "", "", "", false)]
            public string test_plan_name
            {
                get
                {
                    string _test_plan_name;

                            _test_plan_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("test_plan_name"))
                        {
                        _test_plan_name = Convert.ToString(this["test_plan_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_plan_name: {e.Message}");
                        _test_plan_name = default(string);
                    }
                    return _test_plan_name;
                }
                set
                {
                   
                    this["test_plan_name"] = value;
                }
            }
                }
        }
