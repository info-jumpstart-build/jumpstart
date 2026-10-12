
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{

    public interface ITestCaseLogic 
    {
        // Generated methods
        List<TestCase> select();
        List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new();
        List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new();
        TestCase get(long id);
       

        List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new();
        List<MapOption> maplist(long id, string mapSuffix);
<<<<<<< HEAD
=======
        void mapsync(long id, string mapSuffix, List<long> checkedOtherIds);
>>>>>>> main
        List<TestCaseHistory> history(long id);
        void insert(TestCase testcase);
        void update(long id, TestCase testcase);
        void put(TestCase testcase);
        void delete( long id );
        TestCaseView view(long id);
        
    
    }

    public partial class TestCaseLogic : BaseLogic, ITestCaseLogic
    {

        /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="ITestCaseLogic"/>.</returns>
        public static ITestCaseLogic Create()
        {
            var testcase = new TestCaseLogic();

            var proxy = DispatchProxy.Create<ITestCaseLogic, Proxy<ITestCaseLogic>>();
            ((Proxy<ITestCaseLogic>)proxy).Initialize();
            ((Proxy<ITestCaseLogic>)proxy).Target = testcase;
            ((Proxy<ITestCaseLogic>)proxy).DomainObj = "TestCase";

            return proxy;
        }

        /// <summary>
        /// Creates a direct (non-proxied) instance of the logic class, bypassing authorization enforcement.
        /// Use only in contexts where no security principal is available, such as import/export tests
        /// running against an empty database.
        /// </summary>
        /// <returns>A direct <see cref="TestCaseLogic"/> instance implementing <see cref="ITestCaseLogic"/>.</returns>
        public static ITestCaseLogic Unsafe()
        {
            return new TestCaseLogic();
        }

        /// <summary>
        /// Selects all domain objects using the default return type of the domain object.
        /// </summary>
        /// <returns>List of <see cref="TestCase"/> instances.</returns>
        public virtual List<TestCase> select()
        {
            return select<TestCase>();
        }

       
    
        /// <summary>
        /// Selects all domain objects and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new()
        {
            Logger.Debug("Processing TestCaseLogic select<TBaseObject> List");

            List<TBaseObject> testcases = select<TBaseObject>("app.test_case-select");

            
            return testcases;
        }

        /// <summary>
        /// Selects domain objects using a named query and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <param name="queryName">The named query to execute.</param>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing TestCaseLogic select<TBaseObject> with query: {queryName}");

            List<TBaseObject> testcases = DBPersist.ExecuteQueryByName<TBaseObject>(queryName);

            return testcases;
        }

       
        
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>List of <see cref="TestCaseHistory"/> entries associated with the given id.</returns>
        public virtual List<TestCaseHistory> history(long id)
        {
            // History rows live in the same table. Return all versions newest-first.
            // is_active=1 is the current version; is_active=0 rows are prior versions.
            List<TestCaseHistory> testcaseHistory = DBPersist.select<TestCaseHistory>(
                $"SELECT * FROM app.test_case WHERE id = {id} ORDER BY txn_id DESC");

            return testcaseHistory;
        }
        

        /// <summary>
        /// Retrieves child records for a given domain object id and child suffix.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to map child records to.</typeparam>
        /// <param name="id">The parent object's id.</param>
        /// <param name="childSuffix">The suffix identifying which child query to run.</param>
        /// <returns>List of child records mapped to <typeparamref name="TBaseObject"/>.</returns>
        public virtual List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing TestCaseLogic children<TBaseObject> for ID={id}, childSuffix={childSuffix}");

            string queryName = "app.test_case-children-" + childSuffix;
            List<TBaseObject> children = DBPersist.ExecuteQueryByName<TBaseObject>(queryName, new BaseObject() { { "id", id } });

            return children;
        }

        /// <summary>
        /// Retrieves the checklist rows for a many-to-many ("map") relationship tab:
        /// one row per possible "other side" record, flagged with whether it's
        /// already mapped to this object via the junction table. See
        /// MetaObject.MapRelationships / database/{pgsql,mssql}/data/template.map.generated.sql.cshtml
        /// for how the "app.test_case-map-{mapSuffix}" query is generated.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to list (e.g. "org-org").</param>
        /// <returns>List of <see cref="MapOption"/> rows.</returns>
        public virtual List<MapOption> maplist(long id, string mapSuffix)
        {
            Logger.Debug($"Processing TestCaseLogic maplist for ID={id}, mapSuffix={mapSuffix}");

            string queryName = "app.test_case-map-" + mapSuffix;
            List<MapOption> options = DBPersist.ExecuteQueryByName<MapOption>(queryName, new BaseObject() { { "id", id } });

            return options;
        }

        /// <summary>
<<<<<<< HEAD
=======
        /// Applies a many-to-many ("map") relationship checklist submitted from
        /// the Edit page: the full set of "other side" ids that should be
        /// mapped to this object after the call (see DBPersist.mapSync). The
        /// junction table/column names are resolved here from generation-time
        /// metadata (MetaObject.MapRelationships), never trusted from the
        /// client -- mapSuffix only selects which relationship to sync.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to sync (e.g. "org-org").</param>
        /// <param name="checkedOtherIds">The full set of other-side ids that should be checked/mapped.</param>
        public virtual void mapsync(long id, string mapSuffix, List<long> checkedOtherIds)
        {
            Logger.Debug($"Processing TestCaseLogic mapsync for ID={id}, mapSuffix={mapSuffix}");

            // Defaulted outside the generated switch below (rather than
            // inline as "checkedOtherIds ?? new List<long>()" in each case)
            // -- a bare "List<long>" inside the <text> excursion that
            // generates the switch cases gets parsed as an HTML tag by
            // RazorLight's markup tokenizer, same issue documented in
            // web/blazor/Pages/Edittemplate.razor.cshtml.
            List<long> ids = checkedOtherIds ?? new List<long>();

            switch (mapSuffix)
            {
                default:
                    throw new Exception($"Unknown map relationship suffix '{mapSuffix}' on TestCase");
            }
        }

        /// <summary>
>>>>>>> main
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>An instance of <see cref="TestCase"/> if found; otherwise <c>null</c>.</returns>
        public virtual TestCase get(long id)
        {
            Logger.Debug($"Processing TestCaseLogic get ID={id}");

            TestCase testcase = DBPersist.select<TestCase>($"SELECT * FROM app.test_case WHERE id = {id} AND is_active = 1").FirstOrDefault();
            

            return testcase;
        }

        /// <summary>
        /// Retrieves a view representation of the domain object by id.  A view instance contains dereferenced fields from related domain objects.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A view instance for the domain object, or <c>null</c> if not found.</returns>
        public virtual TestCaseView view(long id) 
        {
            Logger.Debug($"Processing TestCaseLogic view<TBaseObject> ID={id}");

            string queryName = "app.test_case-get";
            List<TestCaseView> TestCaseView = DBPersist.ExecuteQueryByName<TestCaseView>(queryName, new BaseObject() { { "id", id } });
            

            return TestCaseView.FirstOrDefault();
        }
        /// <summary>
        /// Inserts a new domain object into persistence.
        /// </summary>
        /// <param name="testcase">The domain object instance to insert.</param>
        public virtual void insert(TestCase testcase)
        {
            Logger.Debug($"Processing TestCaseLogic insert: {testcase}");

            testcase.is_active = 1;

            DBPersist.insert(testcase);
        }

        /// <summary>
        /// Inserts or updates (put) the domain object in persistence.
        /// </summary>
        /// <param name="testcase">The domain object instance to put.</param>
        public virtual void put(TestCase testcase)
        {
            Logger.Debug($"Processing TestCaseLogic put: {testcase}");

            testcase.is_active = 1;

            DBPersist.put(testcase);
        }

        /// <summary>
        /// Updates an existing domain object by id using the provided instance.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testcase">The updated domain object instance.</param>
        public virtual void update(long id, TestCase testcase)
        {
            Logger.Debug($"Processing TestCaseLogic update: ID = {id}\n{testcase}");

            testcase.id = id;
            // A normal save always (re)activates the record -- explicit here
            // (matching insert/put above) now that DBPersistAudit.update() no
            // longer forces is_active=1 itself, since delete() below relies on
            // that method honoring the is_active it sets.
            testcase.is_active = 1;
            DBPersist.update(testcase);
        }

        /// <summary>
        /// Soft-deletes a domain object by setting its <c>is_active</c> flag to 0.
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        public virtual void delete(long id)
        {
            TestCase testcase = get(id);
            testcase.is_active = 0;
            DBPersist.update(testcase);
        }
        
    }
}
