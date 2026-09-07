using System;
using System.Data.Common;

namespace jumptest
{
    public interface IDatabaseProvider
    {
        DbConnection CreateConnection(string connectionString);
        DbCommand CreateCommand(string sql, DbConnection connection);
        DbParameter CreateParameter();
        void AddParameterWithValue(DbCommand command, string parameterName, object value);
        string GetNextIdentitySql(string tableName);
        string FormatParameter(string parameterName);
        string FormatTableName(string tableName);
        string FormatColumnName(string columnName);
        string FormatValue(object value);
        string GetParameterPrefix();
    }
} 