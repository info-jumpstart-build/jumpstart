using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace jumptest
{
    public class SqlServerProvider : IDatabaseProvider
    {
        public DbConnection CreateConnection(string connectionString)
        {
            DbConnection connection;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            } 
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error opening connection: " + ex.Message);
                throw;
            }
            return connection;
        }

        public DbCommand CreateCommand(string sql, DbConnection connection)
        {
            return new SqlCommand(sql, (SqlConnection)connection);
        }

        public DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        public void AddParameterWithValue(DbCommand command, string parameterName, object value)
        {
            var parameter = new SqlParameter(parameterName, value);
            command.Parameters.Add(parameter);
        }

        public string GetNextIdentitySql(string tableName)
        {
            return $"SELECT NEXT VALUE FOR {tableName}_identity;";
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
                formattedParts[i] = $"[{parts[i]}]";
            }
            
            return string.Join(".", formattedParts);
        }

        public string FormatColumnName(string columnName)
        {
            return $"[{columnName}]";
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