
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{

    public interface ICronDowLogic 
    {
        // Generated methods
        List<CronDow> select();
        List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new();
        List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new();
        CronDow get(long id);
       

        List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new();
        List<MapOption> maplist(long id, string mapSuffix);
<<<<<<< HEAD
=======
        void mapsync(long id, string mapSuffix, List<long> checkedOtherIds);
>>>>>>> main
        List<CronDowHistory> history(long id);
        void insert(CronDow crondow);
        void update(long id, CronDow crondow);
        void put(CronDow crondow);
        void delete( long id );
        CronDowView view(long id);
        
    
    }

    public partial class CronDowLogic : BaseLogic, ICronDowLogic
    {

        /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="ICronDowLogic"/>.</returns>
        public static ICronDowLogic Create()
        {
            var crondow = new CronDowLogic();

            var proxy = DispatchProxy.Create<ICronDowLogic, Proxy<ICronDowLogic>>();
            ((Proxy<ICronDowLogic>)proxy).Initialize();
            ((Proxy<ICronDowLogic>)proxy).Target = crondow;
            ((Proxy<ICronDowLogic>)proxy).DomainObj = "CronDow";

            return proxy;
        }

        /// <summary>
        /// Creates a direct (non-proxied) instance of the logic class, bypassing authorization enforcement.
        /// Use only in contexts where no security principal is available, such as import/export tests
        /// running against an empty database.
        /// </summary>
        /// <returns>A direct <see cref="CronDowLogic"/> instance implementing <see cref="ICronDowLogic"/>.</returns>
        public static ICronDowLogic Unsafe()
        {
            return new CronDowLogic();
        }

        /// <summary>
        /// Selects all domain objects using the default return type of the domain object.
        /// </summary>
        /// <returns>List of <see cref="CronDow"/> instances.</returns>
        public virtual List<CronDow> select()
        {
            return select<CronDow>();
        }

       
    
        /// <summary>
        /// Selects all domain objects and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new()
        {
            Logger.Debug("Processing CronDowLogic select<TBaseObject> List");

            List<TBaseObject> crondows = select<TBaseObject>("core.cron_dow-select");

            
            return crondows;
        }

        /// <summary>
        /// Selects domain objects using a named query and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <param name="queryName">The named query to execute.</param>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing CronDowLogic select<TBaseObject> with query: {queryName}");

            List<TBaseObject> crondows = DBPersist.ExecuteQueryByName<TBaseObject>(queryName);

            return crondows;
        }

       
        
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>List of <see cref="CronDowHistory"/> entries associated with the given id.</returns>
        public virtual List<CronDowHistory> history(long id)
        {
            // History rows live in the same table. Return all versions newest-first.
            // is_active=1 is the current version; is_active=0 rows are prior versions.
            List<CronDowHistory> crondowHistory = DBPersist.select<CronDowHistory>(
                $"SELECT * FROM core.cron_dow WHERE id = {id} ORDER BY txn_id DESC");

            return crondowHistory;
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
            Logger.Debug($"Processing CronDowLogic children<TBaseObject> for ID={id}, childSuffix={childSuffix}");

            string queryName = "core.cron_dow-children-" + childSuffix;
            List<TBaseObject> children = DBPersist.ExecuteQueryByName<TBaseObject>(queryName, new BaseObject() { { "id", id } });

            return children;
        }

        /// <summary>
        /// Retrieves the checklist rows for a many-to-many ("map") relationship tab:
        /// one row per possible "other side" record, flagged with whether it's
        /// already mapped to this object via the junction table. See
        /// MetaObject.MapRelationships / database/{pgsql,mssql}/data/template.map.generated.sql.cshtml
        /// for how the "core.cron_dow-map-{mapSuffix}" query is generated.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to list (e.g. "org-org").</param>
        /// <returns>List of <see cref="MapOption"/> rows.</returns>
        public virtual List<MapOption> maplist(long id, string mapSuffix)
        {
            Logger.Debug($"Processing CronDowLogic maplist for ID={id}, mapSuffix={mapSuffix}");

            string queryName = "core.cron_dow-map-" + mapSuffix;
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
            Logger.Debug($"Processing CronDowLogic mapsync for ID={id}, mapSuffix={mapSuffix}");

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
                    throw new Exception($"Unknown map relationship suffix '{mapSuffix}' on CronDow");
            }
        }

        /// <summary>
>>>>>>> main
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>An instance of <see cref="CronDow"/> if found; otherwise <c>null</c>.</returns>
        public virtual CronDow get(long id)
        {
            Logger.Debug($"Processing CronDowLogic get ID={id}");

            CronDow crondow = DBPersist.select<CronDow>($"SELECT * FROM core.cron_dow WHERE id = {id} AND is_active = 1").FirstOrDefault();
            

            return crondow;
        }

        /// <summary>
        /// Retrieves a view representation of the domain object by id.  A view instance contains dereferenced fields from related domain objects.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A view instance for the domain object, or <c>null</c> if not found.</returns>
        public virtual CronDowView view(long id) 
        {
            Logger.Debug($"Processing CronDowLogic view<TBaseObject> ID={id}");

            string queryName = "core.cron_dow-get";
            List<CronDowView> CronDowView = DBPersist.ExecuteQueryByName<CronDowView>(queryName, new BaseObject() { { "id", id } });
            

            return CronDowView.FirstOrDefault();
        }
        /// <summary>
        /// Inserts a new domain object into persistence.
        /// </summary>
        /// <param name="crondow">The domain object instance to insert.</param>
        public virtual void insert(CronDow crondow)
        {
            Logger.Debug($"Processing CronDowLogic insert: {crondow}");

            crondow.is_active = 1;

            DBPersist.insert(crondow);
        }

        /// <summary>
        /// Inserts or updates (put) the domain object in persistence.
        /// </summary>
        /// <param name="crondow">The domain object instance to put.</param>
        public virtual void put(CronDow crondow)
        {
            Logger.Debug($"Processing CronDowLogic put: {crondow}");

            crondow.is_active = 1;

            DBPersist.put(crondow);
        }

        /// <summary>
        /// Updates an existing domain object by id using the provided instance.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="crondow">The updated domain object instance.</param>
        public virtual void update(long id, CronDow crondow)
        {
            Logger.Debug($"Processing CronDowLogic update: ID = {id}\n{crondow}");

            crondow.id = id;
            // A normal save always (re)activates the record -- explicit here
            // (matching insert/put above) now that DBPersistAudit.update() no
            // longer forces is_active=1 itself, since delete() below relies on
            // that method honoring the is_active it sets.
            crondow.is_active = 1;
            DBPersist.update(crondow);
        }

        /// <summary>
        /// Soft-deletes a domain object by setting its <c>is_active</c> flag to 0.
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        public virtual void delete(long id)
        {
            CronDow crondow = get(id);
            crondow.is_active = 0;
            DBPersist.update(crondow);
        }
        
    }
}
