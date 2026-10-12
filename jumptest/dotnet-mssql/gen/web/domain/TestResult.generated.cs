

using System;
using System.Reflection;
using System.Collections.Generic;

namespace jumptest
{
    [ClassInfo("Test Result")]
    public partial class TestResult : BaseObject
    {
        public TestResult(BaseObject baseObject) : base(baseObject)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            // Default initializer
            domainName = "TestResult";
            tableName = "app.test_result";
            schemaName = "app";
            tableBaseName = "test_result";
            auditTableName = "app.test_result"; // history rows live in the same table (is_active=0)
            isAudited = true;


            rwk.Add("test_run_id");
            
            rwk.Add("test_case_id");
            

            _defaults["id"] = default(long);
            
            _defaults["test_run_id"] = default(long);
            
            _defaults["test_case_id"] = default(long);
            
            _defaults["test_result_status_id"] = default(long);
            
            _defaults["executed_at"] = default(DateTime);
            
            _defaults["executed_by"] = default(string);
            
            _defaults["actual_result"] = default(string);
            
            _defaults["notes"] = default(string);
            
            _defaults["is_active"] = default(int);
            
            _defaults["created_by"] = default(string);
            
            _defaults["last_updated"] = default(DateTime);
            
            _defaults["last_updated_by"] = default(string);
            
            _defaults["txn_id"] = default(long);
                    }


            [ColumnInfo("Test Result ID", "", "", "", "", false)]
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
            
            [ColumnInfo("Test Run", "TestRun", "parent", "test_run", "testrun", false)]
            public long test_run_id
            {
                get
                {
                    long _test_run_id;

                             _test_run_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("test_run_id"))
                        {
                        _test_run_id = Convert.ToInt64(this["test_run_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_run_id: {e.Message}");
                        _test_run_id = default(long);
                    }
                    return _test_run_id;
                }
                set
                {
                   
                    this["test_run_id"] = value;
                }
            }
            
            [ColumnInfo("Test Case", "TestCase", "parent", "test_case", "testcase", false)]
            public long test_case_id
            {
                get
                {
                    long _test_case_id;

                             _test_case_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("test_case_id"))
                        {
                        _test_case_id = Convert.ToInt64(this["test_case_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_case_id: {e.Message}");
                        _test_case_id = default(long);
                    }
                    return _test_case_id;
                }
                set
                {
                   
                    this["test_case_id"] = value;
                }
            }
            
            [ColumnInfo("Status", "TestResultStatus", "enum", "test_result_status", "testresultstatus", false)]
            public long test_result_status_id
            {
                get
                {
                    long _test_result_status_id;

                             _test_result_status_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("test_result_status_id"))
                        {
                        _test_result_status_id = Convert.ToInt64(this["test_result_status_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_result_status_id: {e.Message}");
                        _test_result_status_id = default(long);
                    }
                    return _test_result_status_id;
                }
                set
                {
                   
                    this["test_result_status_id"] = value;
                }
            }
            
            [ColumnInfo("Executed At", "", "", "", "", false)]
            public DateTime executed_at
            {
                get
                {
                    DateTime _executed_at;

                             _executed_at = default(DateTime);
                                                 
                    try
                    {
                        if(this.ContainsKey("executed_at"))
                        {
                        _executed_at = Convert.ToDateTime(this["executed_at"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting executed_at: {e.Message}");
                        _executed_at = default(DateTime);
                    }
                    return _executed_at;
                }
                set
                {
                   
                    this["executed_at"] = value;
                }
            }
            
            [ColumnInfo("Executed By", "", "", "", "", false)]
            public string executed_by
            {
                get
                {
                    string _executed_by;

                            _executed_by = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("executed_by"))
                        {
                        _executed_by = Convert.ToString(this["executed_by"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting executed_by: {e.Message}");
                        _executed_by = default(string);
                    }
                    return _executed_by;
                }
                set
                {
                   
                    this["executed_by"] = value;
                }
            }
            
            [ColumnInfo("Actual Result", "", "", "", "", false)]
            public string actual_result
            {
                get
                {
                    string _actual_result;

                            _actual_result = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("actual_result"))
                        {
                        _actual_result = Convert.ToString(this["actual_result"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting actual_result: {e.Message}");
                        _actual_result = default(string);
                    }
                    return _actual_result;
                }
                set
                {
                   
                    this["actual_result"] = value;
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

    public partial class TestResultHistory : TestResult
    {
        protected override void Initialize()
        {
            // History rows live in the same table as the active row.
            // is_active=0 rows are prior versions; is_active=1 is the current row.
            base.Initialize();
            domainName = "TestResultHistory";
            // tableName and schemaName remain the same as the parent class
        }
    }


    // View class that extends the base domain object with RWK columns from foreign key joins
    public class TestResultView : TestResult
    {

            [ColumnInfo("Test Run", "", "", "", "", false)]
            public string test_run_name
            {
                get
                {
                    string _test_run_name;

                            _test_run_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("test_run_name"))
                        {
                        _test_run_name = Convert.ToString(this["test_run_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_run_name: {e.Message}");
                        _test_run_name = default(string);
                    }
                    return _test_run_name;
                }
                set
                {
                   
                    this["test_run_name"] = value;
                }
            }
            
            [ColumnInfo("Test Run Test Plan", "TestPlan", "parent", "test_plan", "testplan", false)]
            public long test_run_test_plan_id
            {
                get
                {
                    long _test_run_test_plan_id;

                             _test_run_test_plan_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("test_run_test_plan_id"))
                        {
                        _test_run_test_plan_id = Convert.ToInt64(this["test_run_test_plan_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_run_test_plan_id: {e.Message}");
                        _test_run_test_plan_id = default(long);
                    }
                    return _test_run_test_plan_id;
                }
                set
                {
                   
                    this["test_run_test_plan_id"] = value;
                }
            }
            
            [ColumnInfo("Test Case Test Plan", "TestPlan", "parent", "test_plan", "testplan", false)]
            public long test_case_test_plan_id
            {
                get
                {
                    long _test_case_test_plan_id;

                             _test_case_test_plan_id = default(long);
                                                 
                    try
                    {
                        if(this.ContainsKey("test_case_test_plan_id"))
                        {
                        _test_case_test_plan_id = Convert.ToInt64(this["test_case_test_plan_id"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_case_test_plan_id: {e.Message}");
                        _test_case_test_plan_id = default(long);
                    }
                    return _test_case_test_plan_id;
                }
                set
                {
                   
                    this["test_case_test_plan_id"] = value;
                }
            }
            
            [ColumnInfo("Test Case Code", "", "", "", "", false)]
            public string test_case_code
            {
                get
                {
                    string _test_case_code;

                            _test_case_code = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("test_case_code"))
                        {
                        _test_case_code = Convert.ToString(this["test_case_code"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_case_code: {e.Message}");
                        _test_case_code = default(string);
                    }
                    return _test_case_code;
                }
                set
                {
                   
                    this["test_case_code"] = value;
                }
            }
            
            [ColumnInfo("Test Case Title", "", "", "", "", false)]
            public string test_case_title
            {
                get
                {
                    string _test_case_title;

                            _test_case_title = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("test_case_title"))
                        {
                        _test_case_title = Convert.ToString(this["test_case_title"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_case_title: {e.Message}");
                        _test_case_title = default(string);
                    }
                    return _test_case_title;
                }
                set
                {
                   
                    this["test_case_title"] = value;
                }
            }
            
            [ColumnInfo("Status", "", "", "", "", false)]
            public string test_result_status_name
            {
                get
                {
                    string _test_result_status_name;

                            _test_result_status_name = string.Empty;
                                                
                    try
                    {
                        if(this.ContainsKey("test_result_status_name"))
                        {
                        _test_result_status_name = Convert.ToString(this["test_result_status_name"].ToString());
                        }
                    }
                    catch(Exception )
                    {
                        //Logger.Error($"Error getting test_result_status_name: {e.Message}");
                        _test_result_status_name = default(string);
                    }
                    return _test_result_status_name;
                }
                set
                {
                   
                    this["test_result_status_name"] = value;
                }
            }
                }
        }
