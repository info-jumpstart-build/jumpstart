
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using jumptest;

namespace jumptest
{

    public interface IDataSourceLogic 
    {
        // Generated methods
        List<DataSource> select();
        List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new();
        List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new();
        DataSource get(long id);
       

        List<TBaseObject> children<TBaseObject>(long id, string childSuffix) where TBaseObject : BaseObject, new();
        List<MapOption> maplist(long id, string mapSuffix);
        void mapsync(long id, string mapSuffix, List<long> checkedOtherIds);
        List<DataSourceHistory> history(long id);
        void insert(DataSource datasource);
        void update(long id, DataSource datasource);
        void put(DataSource datasource);
        void delete( long id );
        DataSourceView view(long id);
        
    
    }

    public partial class DataSourceLogic : BaseLogic, IDataSourceLogic
    {

        /// <summary>
        /// Creates and initializes a proxy instance of the logic implementation for the domain object.
        /// </summary>
        /// <returns>An initialized proxy implementing <see cref="IDataSourceLogic"/>.</returns>
        public static IDataSourceLogic Create()
        {
            var datasource = new DataSourceLogic();

            var proxy = DispatchProxy.Create<IDataSourceLogic, Proxy<IDataSourceLogic>>();
            ((Proxy<IDataSourceLogic>)proxy).Initialize();
            ((Proxy<IDataSourceLogic>)proxy).Target = datasource;
            ((Proxy<IDataSourceLogic>)proxy).DomainObj = "DataSource";

            return proxy;
        }

        /// <summary>
        /// Creates a direct (non-proxied) instance of the logic class, bypassing authorization enforcement.
        /// Use only in contexts where no security principal is available, such as import/export tests
        /// running against an empty database.
        /// </summary>
        /// <returns>A direct <see cref="DataSourceLogic"/> instance implementing <see cref="IDataSourceLogic"/>.</returns>
        public static IDataSourceLogic Unsafe()
        {
            return new DataSourceLogic();
        }

        /// <summary>
        /// Selects all domain objects using the default return type of the domain object.
        /// </summary>
        /// <returns>List of <see cref="DataSource"/> instances.</returns>
        public virtual List<DataSource> select()
        {
            return select<DataSource>();
        }

       
    
        /// <summary>
        /// Selects all domain objects and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>() where TBaseObject : BaseObject, new()
        {
            Logger.Debug("Processing DataSourceLogic select<TBaseObject> List");

            List<TBaseObject> datasources = select<TBaseObject>("core.data_source-select");

            
            return datasources;
        }

        /// <summary>
        /// Selects domain objects using a named query and maps them to the specified base object type.
        /// </summary>
        /// <typeparam name="TBaseObject">The type to which the results will be mapped. Must inherit from <see cref="BaseObject"/> and have a parameterless constructor.</typeparam>
        /// <param name="queryName">The named query to execute.</param>
        /// <returns>List of <typeparamref name="TBaseObject"/> instances.</returns>
        public virtual List<TBaseObject> select<TBaseObject>(string queryName) where TBaseObject : BaseObject, new()
        {
            Logger.Debug($"Processing DataSourceLogic select<TBaseObject> with query: {queryName}");

            List<TBaseObject> datasources = DBPersist.ExecuteQueryByName<TBaseObject>(queryName);

            return datasources;
        }

       
        
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>List of <see cref="DataSourceHistory"/> entries associated with the given id.</returns>
        public virtual List<DataSourceHistory> history(long id)
        {
            // History rows live in the same table. Return all versions newest-first.
            // is_active=1 is the current version; is_active=0 rows are prior versions.
            List<DataSourceHistory> datasourceHistory = DBPersist.select<DataSourceHistory>(
                $"SELECT * FROM core.data_source WHERE id = {id} ORDER BY txn_id DESC");

            return datasourceHistory;
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
            Logger.Debug($"Processing DataSourceLogic children<TBaseObject> for ID={id}, childSuffix={childSuffix}");

            string queryName = "core.data_source-children-" + childSuffix;
            List<TBaseObject> children = DBPersist.ExecuteQueryByName<TBaseObject>(queryName, new BaseObject() { { "id", id } });

            return children;
        }

        /// <summary>
        /// Retrieves the checklist rows for a many-to-many ("map") relationship tab:
        /// one row per possible "other side" record, flagged with whether it's
        /// already mapped to this object via the junction table. See
        /// MetaObject.MapRelationships / database/{pgsql,mssql}/data/template.map.generated.sql.cshtml
        /// for how the "core.data_source-map-{mapSuffix}" query is generated.
        /// </summary>
        /// <param name="id">This object's id.</param>
        /// <param name="mapSuffix">The suffix identifying which map relationship to list (e.g. "org-org").</param>
        /// <returns>List of <see cref="MapOption"/> rows.</returns>
        public virtual List<MapOption> maplist(long id, string mapSuffix)
        {
            Logger.Debug($"Processing DataSourceLogic maplist for ID={id}, mapSuffix={mapSuffix}");

            string queryName = "core.data_source-map-" + mapSuffix;
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
            Logger.Debug($"Processing DataSourceLogic mapsync for ID={id}, mapSuffix={mapSuffix}");

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
                    throw new Exception($"Unknown map relationship suffix '{mapSuffix}' on DataSource");
            }
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>An instance of <see cref="DataSource"/> if found; otherwise <c>null</c>.</returns>
        public virtual DataSource get(long id)
        {
            Logger.Debug($"Processing DataSourceLogic get ID={id}");

            DataSource datasource = DBPersist.select<DataSource>($"SELECT * FROM core.data_source WHERE id = {id} AND is_active = 1").FirstOrDefault();
            

            return datasource;
        }

        /// <summary>
        /// Retrieves a view representation of the domain object by id.  A view instance contains dereferenced fields from related domain objects.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A view instance for the domain object, or <c>null</c> if not found.</returns>
        public virtual DataSourceView view(long id) 
        {
            Logger.Debug($"Processing DataSourceLogic view<TBaseObject> ID={id}");

            string queryName = "core.data_source-get";
            List<DataSourceView> DataSourceView = DBPersist.ExecuteQueryByName<DataSourceView>(queryName, new BaseObject() { { "id", id } });
            

            return DataSourceView.FirstOrDefault();
        }
        /// <summary>
        /// Inserts a new domain object into persistence.
        /// </summary>
        /// <param name="datasource">The domain object instance to insert.</param>
        public virtual void insert(DataSource datasource)
        {
            Logger.Debug($"Processing DataSourceLogic insert: {datasource}");

            datasource.is_active = 1;

            DBPersist.insert(datasource);
        }

        /// <summary>
        /// Inserts or updates (put) the domain object in persistence.
        /// </summary>
        /// <param name="datasource">The domain object instance to put.</param>
        public virtual void put(DataSource datasource)
        {
            Logger.Debug($"Processing DataSourceLogic put: {datasource}");

            datasource.is_active = 1;

            DBPersist.put(datasource);
        }

        /// <summary>
        /// Updates an existing domain object by id using the provided instance.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="datasource">The updated domain object instance.</param>
        public virtual void update(long id, DataSource datasource)
        {
            Logger.Debug($"Processing DataSourceLogic update: ID = {id}\n{datasource}");

            datasource.id = id;
            // A normal save always (re)activates the record -- explicit here
            // (matching insert/put above) now that DBPersistAudit.update() no
            // longer forces is_active=1 itself, since delete() below relies on
            // that method honoring the is_active it sets.
            datasource.is_active = 1;
            DBPersist.update(datasource);
        }

        /// <summary>
        /// Soft-deletes a domain object by setting its <c>is_active</c> flag to 0.
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        public virtual void delete(long id)
        {
            DataSource datasource = get(id);
            datasource.is_active = 0;
            DBPersist.update(datasource);
        }
        
    }
}
