
using System;
using System.Collections.Generic;

namespace jumptest
{
    // Simple persistence without soft-delete versioning or audit fields.
    // insert assigns an id and writes the row as-is; update modifies the existing row in place.
    public class DBPersistBasic : DBPersistAudit
    {
        public override long insert(BaseObject baseObject, string connectionName = "default")
        {
            long id = DBPersist.identity(baseObject, connectionName);
            baseObject["id"] = id;
            baseObject["txn_id"] = id; // Set txn_id to the same value as id for simplicity in this basic implementation
            baseObject["is_active"] = 1;
            baseObject["created_by"] = Environment.UserName;
            baseObject["last_updated"] = DateTime.UtcNow;
            baseObject["last_updated_by"] = Environment.UserName;
            var (sql, parameters) = DBPersist.insertSql(baseObject, baseObject.tableName, connectionName);
            DBPersist.execCmd(sql, parameters, connectionName);

            return id;
        }

        public override void update(BaseObject baseObject, string connectionName = "default")
        {
            baseObject["is_active"] = 1;
            baseObject["last_updated"] = DateTime.UtcNow;
            baseObject["last_updated_by"] = Environment.UserName;
            string sql = DBPersist.updateSql(baseObject, connectionName);
            DBPersist.execCmd(sql, connectionName);
        }
    }
}
