
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{

    public interface ITestPlanLogic 
    {
        // Generated methods
        List<TestPlan> select();
        List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new();
        List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new();
        TestPlan get(long id);
       

        List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new();
        List<MapOption> maplist(long id, string mapSuffix);
        void mapsync(long id, string mapSuffix, List<long> checkedOtherIds);
        List<TestPlanHistory> history(long id);
        void insert(TestPlan testplan);
        void update(long id, TestPlan testplan);
        void put(TestPlan testplan);
        void delete( long id );
        TestPlanView view(long id);
        
    
    }

    public partial class TestPlanLogic : BaseLogic, ITestPlanLogic
    {

        /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="ITestPlanLogic"/>.</returns>
        public static ITestPlanLogic Create()
        {
            var testplan = new TestPlanLogic();

            var proxy = DispatchProxy.Create<ITestPlanLogic, Proxy<ITestPlanLogic>>();
            ((Proxy<ITestPlanLogic>)proxy).Initialize();
            ((Proxy<ITestPlanLogic>)proxy).Target = testplan;
            ((Proxy<ITestPlanLogic>)proxy).DomainObj = "TestPlan";

            return proxy;
        }

        /// <summary>
        /// Creates a direct (non-proxied) instance of the logic class, bypassing authorization enforcement.
        /// Use only in contexts where no security principal is available, such as import/export tests
        /// running against an empty database.
        /// </summary>
        /// <returns>A direct <see cref="TestPlanLogic"/> instance implementing <see cref="ITestPlanLogic"/>.</returns>
        public static ITestPlanLogic Unsafe()
        {
            return new TestPlanLogic();
        }

        /// <summary>
        /// Selects all domain objects using the default return type of the domain object.
        /// </summary>
        /// <returns>List of <see cref="TestPlan"/> instances.</returns>
        public virtual List<TestPlan> select()
        {
            return select<TestPlan>();
        }

       
    
        /// <summary>
        /// Selects all domain objects and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new()
        {
            Logger.Debug("Processing TestPlanLogic select<TBaseObject> List");

            List<TBaseObject> testplans = select<TBaseObject>("app.test_plan-select");

            
            return testplans;
        }

        /// <summary>
        /// Selects domain objects using a named query and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <param name="queryName">The named query to execute.</param>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing TestPlanLogic select<TBaseObject> with query: {queryName}");

            List<TBaseObject> testplans = DBPersist.ExecuteQueryByName<TBaseObject>(queryName);

            return testplans;
        }

       
        
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>List of <see cref="TestPlanHistory"/> entries associated with the given id.</returns>
        public virtual List<TestPlanHistory> history(long id)
        {
            // History rows live in the same table. Return all versions newest-first.
            // is_active=1 is the current version; is_active=0 rows are prior versions.
            List<TestPlanHistory> testplanHistory = DBPersist.select<TestPlanHistory>(
                $"SELECT * FROM app.test_plan WHERE id = {id} ORDER BY txn_id DESC");

            return testplanHistory;
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
            Logger.Debug($"Processing TestPlanLogic children<TBaseObject> for ID={id}, childSuffix={childSuffix}");

            string queryName = "app.test_plan-children-" + childSuffix;
            List<TBaseObject> children = DBPersist.ExecuteQueryByName<TBaseObject>(queryName, new BaseObject() { { "id", id } });

            return children;
        }

        /// <summary>
        /// Retrieves the checklist rows for a many-to-many ("map") relationship tab:
        /// one row per possible "other side" record, flagged with whether it's
        /// already mapped to this object via the junction table. See
        /// MetaObject.MapRelationships / database/{pgsql,mssql}/data/template.map.generated.sql.cshtml
        /// for how the "app.test_plan-map-{mapSuffix}" query is generated.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to list (e.g. "org-org").</param>
        /// <returns>List of <see cref="MapOption"/> rows.</returns>
        public virtual List<MapOption> maplist(long id, string mapSuffix)
        {
            Logger.Debug($"Processing TestPlanLogic maplist for ID={id}, mapSuffix={mapSuffix}");

            string queryName = "app.test_plan-map-" + mapSuffix;
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
            Logger.Debug($"Processing TestPlanLogic mapsync for ID={id}, mapSuffix={mapSuffix}");

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
                    throw new Exception($"Unknown map relationship suffix '{mapSuffix}' on TestPlan");
            }
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>An instance of <see cref="TestPlan"/> if found; otherwise <c>null</c>.</returns>
        public virtual TestPlan get(long id)
        {
            Logger.Debug($"Processing TestPlanLogic get ID={id}");

            TestPlan testplan = DBPersist.select<TestPlan>($"SELECT * FROM app.test_plan WHERE id = {id} AND is_active = 1").FirstOrDefault();
            

            return testplan;
        }

        /// <summary>
        /// Retrieves a view representation of the domain object by id.  A view instance contains dereferenced fields from related domain objects.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A view instance for the domain object, or <c>null</c> if not found.</returns>
        public virtual TestPlanView view(long id) 
        {
            Logger.Debug($"Processing TestPlanLogic view<TBaseObject> ID={id}");

            string queryName = "app.test_plan-get";
            List<TestPlanView> TestPlanView = DBPersist.ExecuteQueryByName<TestPlanView>(queryName, new BaseObject() { { "id", id } });
            

            return TestPlanView.FirstOrDefault();
        }
        /// <summary>
        /// Inserts a new domain object into persistence.
        /// </summary>
        /// <param name="testplan">The domain object instance to insert.</param>
        public virtual void insert(TestPlan testplan)
        {
            Logger.Debug($"Processing TestPlanLogic insert: {testplan}");

            testplan.is_active = 1;

            DBPersist.insert(testplan);
        }

        /// <summary>
        /// Inserts or updates (put) the domain object in persistence.
        /// </summary>
        /// <param name="testplan">The domain object instance to put.</param>
        public virtual void put(TestPlan testplan)
        {
            Logger.Debug($"Processing TestPlanLogic put: {testplan}");

            testplan.is_active = 1;

            DBPersist.put(testplan);
        }

        /// <summary>
        /// Updates an existing domain object by id using the provided instance.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testplan">The updated domain object instance.</param>
        public virtual void update(long id, TestPlan testplan)
        {
            Logger.Debug($"Processing TestPlanLogic update: ID = {id}\n{testplan}");

            testplan.id = id;
            // A normal save always (re)activates the record -- explicit here
            // (matching insert/put above) now that DBPersistAudit.update() no
            // longer forces is_active=1 itself, since delete() below relies on
            // that method honoring the is_active it sets.
            testplan.is_active = 1;
            DBPersist.update(testplan);
        }

        /// <summary>
        /// Soft-deletes a domain object by setting its <c>is_active</c> flag to 0.
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        public virtual void delete(long id)
        {
            TestPlan testplan = get(id);
            testplan.is_active = 0;
            DBPersist.update(testplan);
        }
        
    }
}
