using System;
using System.Data.Common;
using Npgsql;

namespace jumptest
{
    public class PostgreSQLProvider : IDatabaseProvider
    {
        public PostgreSQLProvider()
        {
            
        }

        public DbConnection CreateConnection(string connectionString)
        {
            NpgsqlConnection connection =  new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public DbCommand CreateCommand(string sql, DbConnection connection)
        {
            return new NpgsqlCommand(sql, (NpgsqlConnection)connection);
        }

        public DbParameter CreateParameter()
        {
            return new NpgsqlParameter();
        }

        public void AddParameterWithValue(DbCommand command, string parameterName, object value)
        {
            command.Parameters.Add(new NpgsqlParameter(parameterName, value));
        }

        public string GetNextIdentitySql(string tableName)
        {
            return $"SELECT nextval('{tableName}_identity');";
        }

        public string FormatParameter(string parameterName)
        {
            return $"@{parameterName}";
        }

        public string FormatTableName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return tableName;

            var parts = tableName.Split('.');
            var formattedParts = new string[parts.Length];
            
            for (int i = 0; i < parts.Length; i++)
            {
                formattedParts[i] = $"\"{parts[i]}\"";
            }
            
            return string.Join(".", formattedParts);
        }

        public string FormatColumnName(string columnName)
        {
            return $"\"{columnName}\"";
        }

        public string FormatValue(object value)
        {
            if (value == null) return "NULL";

            if (value.GetType().Equals(typeof(DateTime)))
            {
                DateTime d = (DateTime)value;
                return $"'{d.ToString("yyyy-MM-dd HH:mm:ss")}'";
            }
            else if (value.GetType().Equals(typeof(string)))
            {
                return $"'{value.ToString().Replace("'", "''")}'";
            }
            else if (value.GetType().FullName.Contains("Text"))
            {
                return $"'{value.ToString().Replace("'", "''")}'";
            }
            else
            {
                return value.ToString();
            }
        }

        public string GetParameterPrefix()
        {
            return "@";
        }
    }
} 