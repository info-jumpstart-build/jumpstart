
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{

    public interface ICronDomLogic 
    {
        // Generated methods
        List<CronDom> select();
        List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new();
        List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new();
        CronDom get(long id);
       

        List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new();
        List<MapOption> maplist(long id, string mapSuffix);
<<<<<<< HEAD
=======
        void mapsync(long id, string mapSuffix, List<long> checkedOtherIds);
>>>>>>> main
        List<CronDomHistory> history(long id);
        void insert(CronDom crondom);
        void update(long id, CronDom crondom);
        void put(CronDom crondom);
        void delete( long id );
        CronDomView view(long id);
        
    
    }

    public partial class CronDomLogic : BaseLogic, ICronDomLogic
    {

        /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="ICronDomLogic"/>.</returns>
        public static ICronDomLogic Create()
        {
            var crondom = new CronDomLogic();

            var proxy = DispatchProxy.Create<ICronDomLogic, Proxy<ICronDomLogic>>();
            ((Proxy<ICronDomLogic>)proxy).Initialize();
            ((Proxy<ICronDomLogic>)proxy).Target = crondom;
            ((Proxy<ICronDomLogic>)proxy).DomainObj = "CronDom";

            return proxy;
        }

        /// <summary>
        /// Creates a direct (non-proxied) instance of the logic class, bypassing authorization enforcement.
        /// Use only in contexts where no security principal is available, such as import/export tests
        /// running against an empty database.
        /// </summary>
        /// <returns>A direct <see cref="CronDomLogic"/> instance implementing <see cref="ICronDomLogic"/>.</returns>
        public static ICronDomLogic Unsafe()
        {
            return new CronDomLogic();
        }

        /// <summary>
        /// Selects all domain objects using the default return type of the domain object.
        /// </summary>
        /// <returns>List of <see cref="CronDom"/> instances.</returns>
        public virtual List<CronDom> select()
        {
            return select<CronDom>();
        }

       
    
        /// <summary>
        /// Selects all domain objects and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new()
        {
            Logger.Debug("Processing CronDomLogic select<TBaseObject> List");

            List<TBaseObject> crondoms = select<TBaseObject>("core.cron_dom-select");

            
            return crondoms;
        }

        /// <summary>
        /// Selects domain objects using a named query and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <param name="queryName">The named query to execute.</param>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing CronDomLogic select<TBaseObject> with query: {queryName}");

            List<TBaseObject> crondoms = DBPersist.ExecuteQueryByName<TBaseObject>(queryName);

            return crondoms;
        }

       
        
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>List of <see cref="CronDomHistory"/> entries associated with the given id.</returns>
        public virtual List<CronDomHistory> history(long id)
        {
            // History rows live in the same table. Return all versions newest-first.
            // is_active=1 is the current version; is_active=0 rows are prior versions.
            List<CronDomHistory> crondomHistory = DBPersist.select<CronDomHistory>(
                $"SELECT * FROM core.cron_dom WHERE id = {id} ORDER BY txn_id DESC");

            return crondomHistory;
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
            Logger.Debug($"Processing CronDomLogic children<TBaseObject> for ID={id}, childSuffix={childSuffix}");

            string queryName = "core.cron_dom-children-" + childSuffix;
            List<TBaseObject> children = DBPersist.ExecuteQueryByName<TBaseObject>(queryName, new BaseObject() { { "id", id } });

            return children;
        }

        /// <summary>
        /// Retrieves the checklist rows for a many-to-many ("map") relationship tab:
        /// one row per possible "other side" record, flagged with whether it's
        /// already mapped to this object via the junction table. See
        /// MetaObject.MapRelationships / database/{pgsql,mssql}/data/template.map.generated.sql.cshtml
        /// for how the "core.cron_dom-map-{mapSuffix}" query is generated.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to list (e.g. "org-org").</param>
        /// <returns>List of <see cref="MapOption"/> rows.</returns>
        public virtual List<MapOption> maplist(long id, string mapSuffix)
        {
            Logger.Debug($"Processing CronDomLogic maplist for ID={id}, mapSuffix={mapSuffix}");

            string queryName = "core.cron_dom-map-" + mapSuffix;
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
            Logger.Debug($"Processing CronDomLogic mapsync for ID={id}, mapSuffix={mapSuffix}");

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
                    throw new Exception($"Unknown map relationship suffix '{mapSuffix}' on CronDom");
            }
        }

        /// <summary>
>>>>>>> main
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>An instance of <see cref="CronDom"/> if found; otherwise <c>null</c>.</returns>
        public virtual CronDom get(long id)
        {
            Logger.Debug($"Processing CronDomLogic get ID={id}");

            CronDom crondom = DBPersist.select<CronDom>($"SELECT * FROM core.cron_dom WHERE id = {id} AND is_active = 1").FirstOrDefault();
            

            return crondom;
        }

        /// <summary>
        /// Retrieves a view representation of the domain object by id.  A view instance contains dereferenced fields from related domain objects.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A view instance for the domain object, or <c>null</c> if not found.</returns>
        public virtual CronDomView view(long id) 
        {
            Logger.Debug($"Processing CronDomLogic view<TBaseObject> ID={id}");

            string queryName = "core.cron_dom-get";
            List<CronDomView> CronDomView = DBPersist.ExecuteQueryByName<CronDomView>(queryName, new BaseObject() { { "id", id } });
            

            return CronDomView.FirstOrDefault();
        }
        /// <summary>
        /// Inserts a new domain object into persistence.
        /// </summary>
        /// <param name="crondom">The domain object instance to insert.</param>
        public virtual void insert(CronDom crondom)
        {
            Logger.Debug($"Processing CronDomLogic insert: {crondom}");

            crondom.is_active = 1;

            DBPersist.insert(crondom);
        }

        /// <summary>
        /// Inserts or updates (put) the domain object in persistence.
        /// </summary>
        /// <param name="crondom">The domain object instance to put.</param>
        public virtual void put(CronDom crondom)
        {
            Logger.Debug($"Processing CronDomLogic put: {crondom}");

            crondom.is_active = 1;

            DBPersist.put(crondom);
        }

        /// <summary>
        /// Updates an existing domain object by id using the provided instance.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="crondom">The updated domain object instance.</param>
        public virtual void update(long id, CronDom crondom)
        {
            Logger.Debug($"Processing CronDomLogic update: ID = {id}\n{crondom}");

            crondom.id = id;
            // A normal save always (re)activates the record -- explicit here
            // (matching insert/put above) now that DBPersistAudit.update() no
            // longer forces is_active=1 itself, since delete() below relies on
            // that method honoring the is_active it sets.
            crondom.is_active = 1;
            DBPersist.update(crondom);
        }

        /// <summary>
        /// Soft-deletes a domain object by setting its <c>is_active</c> flag to 0.
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        public virtual void delete(long id)
        {
            CronDom crondom = get(id);
            crondom.is_active = 0;
            DBPersist.update(crondom);
        }
        
    }
}
