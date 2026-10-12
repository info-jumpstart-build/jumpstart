
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace jumptest
{
    public class DBPersistAudit : IDBPersist
    {
        public virtual long insert(BaseObject baseObject, string connectionName = "default")
        {
            long id = DBPersist.identity(baseObject, connectionName);

            baseObject["id"] = id;
            baseObject["txn_id"] = id;
            baseObject["is_active"] = 1;
            baseObject["created_by"] = Environment.UserName;
            baseObject["last_updated"] = DateTime.UtcNow;
            baseObject["last_updated_by"] = Environment.UserName;

            var (sql, parameters) = DBPersist.insertSql(baseObject, baseObject.tableName, connectionName);
            DBPersist.execCmd(sql, parameters, connectionName);

            return id;
        }

        public virtual void update(BaseObject baseObject, string connectionName = "default")
        {
            IDatabaseProvider provider = DatabaseProviderFactory.CreateProvider(connectionName);
            long existingId = Convert.ToInt64(baseObject["id"]);

            string deactivateSql = $"UPDATE {provider.FormatTableName(baseObject.tableName)} " +
                                   $"SET {provider.FormatColumnName("is_active")} = 0 " +
                                   $"WHERE {provider.FormatColumnName("id")} = {existingId} " +
                                   $"AND {provider.FormatColumnName("is_active")} = 1";
            DBPersist.execCmd(deactivateSql, connectionName);

            // Deliberately does NOT force is_active=1 here (unlike insert,
            // above) -- the new version row must carry whatever is_active
            // the caller already set on baseObject: Logic.update() sets it
            // to 1 (a normal save keeps the record active), Logic.delete()
            // sets it to 0 (soft delete). Forcing it to 1 unconditionally
            // used to silently undo delete()'s is_active=0, making the Edit
            // page's Delete button appear to do nothing.
            long newTxnId = DBPersist.identity(baseObject, connectionName);
            baseObject["txn_id"] = newTxnId;
            baseObject["last_updated"] = DateTime.UtcNow;
            baseObject["last_updated_by"] = Environment.UserName;

            var (sql, parameters) = DBPersist.insertSql(baseObject, baseObject.tableName, connectionName);
            DBPersist.execCmd(sql, parameters, connectionName);
        }

        public virtual void update(BaseObject baseObject, BaseObject filter, string connectionName = "default")
        {
            // Filter-based bulk update — used for operational state changes (e.g. deactivating exec logs).
            // These are not entity-version updates, so we update in place without creating a new version row.
            baseObject["last_updated"] = DateTime.UtcNow;
            baseObject["last_updated_by"] = Environment.UserName;

            string sql = DBPersist.updateSql(baseObject, filter, connectionName);
            DBPersist.execCmd(sql, connectionName);
        }

        public virtual void put(BaseObject baseObject, string rwkCol, string connectionName = "default")
        {
            IDatabaseProvider provider = DatabaseProviderFactory.CreateProvider(connectionName);

            string sql = "select * from " + provider.FormatTableName("^(tableName)") + " where " + provider.FormatColumnName("^(rwkCol)") + " = ^(rwkValue) and is_active=1";
            bool found = false;

            BaseObject filter = new BaseObject();
            filter["tableName"] = baseObject.tableName;
            filter["rwkCol"] = rwkCol;
            filter["rwkValue"] = provider.FormatValue(baseObject[rwkCol].ToString());

            DBPersist.SelectCallback scb = (rdr) =>
            {
                found = true;
                baseObject["id"] = rdr["id"];
            };

            DBPersist.select(scb, sql, filter, connectionName);

            if (found)
                update(baseObject, connectionName);
            else
                insert(baseObject, connectionName);
        }

        public virtual void put(BaseObject baseObject, string connectionName = "default")
        {
            IDatabaseProvider provider = DatabaseProviderFactory.CreateProvider(connectionName);

            string sql = "select * from " + provider.FormatTableName("^(schemaName).^(tableName)") + " where ^(expression) and is_active=1";
            int count = 0;
            StringBuilder expression = new StringBuilder();
            bool hasId = false;

            if (baseObject.ContainsKey("id"))
            {
                if (Convert.ToInt64(baseObject["id"]) != default(long)) hasId = true;
            }

            if (!hasId)
            {
                if (baseObject.getRwk().Count <= 0) throw new Exception(baseObject.GetType().Name + ".rwk list is empty. DBPersist.put(BaseObject) requires at least one attribute in the rwk list.");

                DBPersist.exprSql(baseObject, baseObject.getRwk(), " and ", expression, connectionName);

                BaseObject filter = new BaseObject();
                filter["schemaName"] = baseObject.schemaName;
                filter["tableName"] = baseObject.tableBaseName;
                filter["expression"] = expression.ToString();

                DBPersist.SelectCallback scb = (rdr) =>
                {
                    count++;
                    baseObject["id"] = rdr["id"];
                };

                DBPersist.select(scb, sql, filter, connectionName);
            }

            if (count == 1 || hasId)
                update(baseObject, connectionName);
            else if (count == 0)
                insert(baseObject, connectionName);
            else
                throw new Exception(baseObject.GetType().Name + ".rwk returned more than one row. sql=" + sql);
        }

        public virtual void get(BaseObject t, string connectionName = "default")
        {
            string sql = DBPersist.getSql(t, connectionName);

            DBPersist.SelectCallback sc = (rdr) =>
            {
                DBPersist.autoAssign(rdr, t);
            };

            DBPersist.select(sc, sql, connectionName);
        }

        public virtual T get<T>(long id, string connectionName = "default") where T : BaseObject, new()
        {
            T t = new T();
            t["id"] = id;
            get(t, connectionName);
            return t;
        }
    }
}
