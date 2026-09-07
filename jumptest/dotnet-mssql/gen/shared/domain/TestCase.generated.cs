

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Test Case")]
    public partial class TestCase : BaseObject
    {
        public TestCase(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "TestCase";
            tableName = "app.test_case";
            schemaName = "app";
            tableBaseName = "test_case";
            auditTableName = "app.test_case"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("test_plan_id");
            
            rwk.Add("code");
            
            rwk.Add("title");
            

            _defaults["id"] = default(long);
            
            _defaults["test_plan_id"] = default(long);
            
            _defaults["code"] = default(string);
            
            _defaults["area"] = default(string);
            
            _defaults["title"] = default(string);
            
            _defaults["preconditions"] = default(string);
            
            _defaults["steps"] = default(string);
            
            _defaults["expected_result"] = default(string);
            
            _defaults["priority"] = default(string);
            
            _defaults["component"] = default(string);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Test Case ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Code", "", "", "", "", false)]
            public string code
            {
                get
                {
                    string _code;

                            _code = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("code"))
                        {
                        _code = Convert.ToString(this["code"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting code: {e.Message}");
                        _code = default(string);
                    }
                    return _code;
                }
                set
                {
                   
                    this["code"] = value;
                }
            }
            
            [ColumnInfo("Area", "", "", "", "", false)]
            public string area
            {
                get
                {
                    string _area;

                            _area = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("area"))
                        {
                        _area = Convert.ToString(this["area"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting area: {e.Message}");
                        _area = default(string);
                    }
                    return _area;
                }
                set
                {
                   
                    this["area"] = value;
                }
            }
            
            [ColumnInfo("Title", "", "", "", "", false)]
            public string title
            {
                get
                {
                    string _title;

                            _title = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("title"))
                        {
                        _title = Convert.ToString(this["title"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting title: {e.Message}");
                        _title = default(string);
                    }
                    return _title;
                }
                set
                {
                   
                    this["title"] = value;
                }
            }
            
            [ColumnInfo("Preconditions", "", "", "", "", false)]
            public string preconditions
            {
                get
                {
                    string _preconditions;

                            _preconditions = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("preconditions"))
                        {
                        _preconditions = Convert.ToString(this["preconditions"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting preconditions: {e.Message}");
                        _preconditions = default(string);
                    }
                    return _preconditions;
                }
                set
                {
                   
                    this["preconditions"] = value;
                }
            }
            
            [ColumnInfo("Steps", "", "", "", "", false)]
            public string steps
            {
                get
                {
                    string _steps;

                            _steps = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("steps"))
                        {
                        _steps = Convert.ToString(this["steps"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting steps: {e.Message}");
                        _steps = default(string);
                    }
                    return _steps;
                }
                set
                {
                   
                    this["steps"] = value;
                }
            }
            
            [ColumnInfo("Expected Result", "", "", "", "", false)]
            public string expected_result
            {
                get
                {
                    string _expected_result;

                            _expected_result = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("expected_result"))
                        {
                        _expected_result = Convert.ToString(this["expected_result"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting expected_result: {e.Message}");
                        _expected_result = default(string);
                    }
                    return _expected_result;
                }
                set
                {
                   
                    this["expected_result"] = value;
                }
            }
            
            [ColumnInfo("Priority", "", "", "", "", false)]
            public string priority
            {
                get
                {
                    string _priority;

                            _priority = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("priority"))
                        {
                        _priority = Convert.ToString(this["priority"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting priority: {e.Message}");
                        _priority = default(string);
                    }
                    return _priority;
                }
                set
                {
                   
                    this["priority"] = value;
                }
            }
            
            [ColumnInfo("Component", "", "", "", "", false)]
            public string component
            {
                get
                {
                    string _component;

                            _component = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("component"))
                        {
                        _component = Convert.ToString(this["component"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting component: {e.Message}");
                        _component = default(string);
                    }
                    return _component;
                }
                set
                {
                   
                    this["component"] = value;
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

    public partial class TestCaseHistory : TestCase
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "TestCaseHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class TestCaseView : TestCase
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
