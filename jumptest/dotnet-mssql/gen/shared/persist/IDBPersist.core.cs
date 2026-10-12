
using System.Collections.Generic;

namespace jumptest
{
    public interface IDBPersist
    {
        long insert(BaseObject baseObject, string connectionName = "default");
        void update(BaseObject baseObject, string connectionName = "default");
        void update(BaseObject baseObject, BaseObject filter, string connectionName = "default");
        void put(BaseObject baseObject, string rwkCol, string connectionName = "default");
        void put(BaseObject baseObject, string connectionName = "default");
        void get(BaseObject t, string connectionName = "default");
        T get<T>(long id, string connectionName = "default") where T : BaseObject, new();
    }
}
