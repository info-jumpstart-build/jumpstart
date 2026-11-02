
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{

    public interface ITestRunLogic 
    {
        // Generated methods
        List<TestRun> select();
        List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new();
        List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new();
        TestRun get(long id);
       

        List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new();
        List<MapOption> maplist(long id, string mapSuffix);
        void mapsync(long id, string mapSuffix, List<long> checkedOtherIds);
        List<TestRunHistory> history(long id);
        void insert(TestRun testrun);
        void update(long id, TestRun testrun);
        void put(TestRun testrun);
        void delete( long id );
        TestRunView view(long id);
        
    
    }

    public partial class TestRunLogic : BaseLogic, ITestRunLogic
    {

        /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="ITestRunLogic"/>.</returns>
        public static ITestRunLogic Create()
        {
            var testrun = new TestRunLogic();

            var proxy = DispatchProxy.Create<ITestRunLogic, Proxy<ITestRunLogic>>();
            ((Proxy<ITestRunLogic>)proxy).Initialize();
            ((Proxy<ITestRunLogic>)proxy).Target = testrun;
            ((Proxy<ITestRunLogic>)proxy).DomainObj = "TestRun";

            return proxy;
        }

        /// <summary>
        /// Creates a direct (non-proxied) instance of the logic class, bypassing authorization enforcement.
        /// Use only in contexts where no security principal is available, such as import/export tests
        /// running against an empty database.
        /// </summary>
        /// <returns>A direct <see cref="TestRunLogic"/> instance implementing <see cref="ITestRunLogic"/>.</returns>
        public static ITestRunLogic Unsafe()
        {
            return new TestRunLogic();
        }

        /// <summary>
        /// Selects all domain objects using the default return type of the domain object.
        /// </summary>
        /// <returns>List of <see cref="TestRun"/> instances.</returns>
        public virtual List<TestRun> select()
        {
            return select<TestRun>();
        }

       
    
        /// <summary>
        /// Selects all domain objects and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new()
        {
            Logger.Debug("Processing TestRunLogic select<TBaseObject> List");

            List<TBaseObject> testruns = select<TBaseObject>("app.test_run-select");

            
            return testruns;
        }

        /// <summary>
        /// Selects domain objects using a named query and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <param name="queryName">The named query to execute.</param>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing TestRunLogic select<TBaseObject> with query: {queryName}");

            List<TBaseObject> testruns = DBPersist.ExecuteQueryByName<TBaseObject>(queryName);

            return testruns;
        }

       
        
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>List of <see cref="TestRunHistory"/> entries associated with the given id.</returns>
        public virtual List<TestRunHistory> history(long id)
        {
            // History rows live in the same table. Return all versions newest-first.
            // is_active=1 is the current version; is_active=0 rows are prior versions.
            List<TestRunHistory> testrunHistory = DBPersist.select<TestRunHistory>(
                $"SELECT * FROM app.test_run WHERE id = {id} ORDER BY txn_id DESC");

            return testrunHistory;
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
            Logger.Debug($"Processing TestRunLogic children<TBaseObject> for ID={id}, childSuffix={childSuffix}");

            string queryName = "app.test_run-children-" + childSuffix;
            List<TBaseObject> children = DBPersist.ExecuteQueryByName<TBaseObject>(queryName, new BaseObject() { { "id", id } });

            return children;
        }

        /// <summary>
        /// Retrieves the checklist rows for a many-to-many ("map") relationship tab:
        /// one row per possible "other side" record, flagged with whether it's
        /// already mapped to this object via the junction table. See
        /// MetaObject.MapRelationships / database/{pgsql,mssql}/data/template.map.generated.sql.cshtml
        /// for how the "app.test_run-map-{mapSuffix}" query is generated.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to list (e.g. "org-org").</param>
        /// <returns>List of <see cref="MapOption"/> rows.</returns>
        public virtual List<MapOption> maplist(long id, string mapSuffix)
        {
            Logger.Debug($"Processing TestRunLogic maplist for ID={id}, mapSuffix={mapSuffix}");

            string queryName = "app.test_run-map-" + mapSuffix;
            List<MapOption> options = DBPersist.ExecuteQueryByName<MapOption>(queryName, new BaseObject() { { "id", id } });

            return options;
        }

        /// <summary>
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
            Logger.Debug($"Processing TestRunLogic mapsync for ID={id}, mapSuffix={mapSuffix}");

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
                    throw new Exception($"Unknown map relationship suffix '{mapSuffix}' on TestRun");
            }
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>An instance of <see cref="TestRun"/> if found; otherwise <c>null</c>.</returns>
        public virtual TestRun get(long id)
        {
            Logger.Debug($"Processing TestRunLogic get ID={id}");

            TestRun testrun = DBPersist.select<TestRun>($"SELECT * FROM app.test_run WHERE id = {id} AND is_active = 1").FirstOrDefault();
            

            return testrun;
        }

        /// <summary>
        /// Retrieves a view representation of the domain object by id.  A view instance contains dereferenced fields from related domain objects.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A view instance for the domain object, or <c>null</c> if not found.</returns>
        public virtual TestRunView view(long id) 
        {
            Logger.Debug($"Processing TestRunLogic view<TBaseObject> ID={id}");

            string queryName = "app.test_run-get";
            List<TestRunView> TestRunView = DBPersist.ExecuteQueryByName<TestRunView>(queryName, new BaseObject() { { "id", id } });
            

            return TestRunView.FirstOrDefault();
        }
        /// <summary>
        /// Inserts a new domain object into persistence.
        /// </summary>
        /// <param name="testrun">The domain object instance to insert.</param>
        public virtual void insert(TestRun testrun)
        {
            Logger.Debug($"Processing TestRunLogic insert: {testrun}");

            testrun.is_active = 1;

            DBPersist.insert(testrun);
        }

        /// <summary>
        /// Inserts or updates (put) the domain object in persistence.
        /// </summary>
        /// <param name="testrun">The domain object instance to put.</param>
        public virtual void put(TestRun testrun)
        {
            Logger.Debug($"Processing TestRunLogic put: {testrun}");

            testrun.is_active = 1;

            DBPersist.put(testrun);
        }

        /// <summary>
        /// Updates an existing domain object by id using the provided instance.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testrun">The updated domain object instance.</param>
        public virtual void update(long id, TestRun testrun)
        {
            Logger.Debug($"Processing TestRunLogic update: ID = {id}\n{testrun}");

            testrun.id = id;
            // A normal save always (re)activates the record -- explicit here
            // (matching insert/put above) now that DBPersistAudit.update() no
            // longer forces is_active=1 itself, since delete() below relies on
            // that method honoring the is_active it sets.
            testrun.is_active = 1;
            DBPersist.update(testrun);
        }

        /// <summary>
        /// Soft-deletes a domain object by setting its <c>is_active</c> flag to 0.
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        public virtual void delete(long id)
        {
            TestRun testrun = get(id);
            testrun.is_active = 0;
            DBPersist.update(testrun);
        }
        
    }
}
