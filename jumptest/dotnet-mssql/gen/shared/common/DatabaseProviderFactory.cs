using System;
using System.Collections.Generic;

namespace jumptest
{
    public static class DatabaseProviderFactory
    {
        // Dictionary to cache providers by connection name
        private static readonly Dictionary<string, IDatabaseProvider> _providers = new Dictionary<string, IDatabaseProvider>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Creates or retrieves a cached database provider for the specified connection name
        /// </summary>
        /// <param name="connectionName">The name of the connection to create a provider for</param>
        /// <returns>An IDatabaseProvider instance for the specified connection</returns>
        public static IDatabaseProvider CreateProvider(string connectionName = "default")
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                connectionName = "default";
            }

            lock (_lock)
            {
                // Check if provider already exists in cache
                if (_providers.ContainsKey(connectionName))
                {
                    Logger.Debug($"Returning cached provider for connection '{connectionName}'");
                    return _providers[connectionName];
                }

                // Determine database type for this connection
                string databaseType = GetDatabaseTypeForConnection(connectionName);
                
                // Create the appropriate provider
                IDatabaseProvider provider = databaseType switch
                {
                    "postgresql" or "pgsql" => new PostgreSQLProvider(),
                    "sqlserver" or "mssql" => new SqlServerProvider(),
                    _ => throw new ArgumentException($"Unsupported database type: {databaseType} for connection '{connectionName}'")
                };

                // Cache the provider
                _providers[connectionName] = provider;
                Logger.Debug($"Created and cached new provider for connection '{connectionName}' with database type '{databaseType}'");

                return provider;
            }
        }

        /// <summary>
        /// Gets the database type for a specific connection name
        /// </summary>
        /// <param name="connectionName">The name of the connection</param>
        /// <returns>The database type string (e.g., "postgresql", "sqlserver")</returns>
        private static string GetDatabaseTypeForConnection(string connectionName)
        {
            // Load the namespace configuration to get data source info
            var config = Config.LoadNamespaceConfig();
            
            if (config?.DataSourceConfigs == null || !config.DataSourceConfigs.ContainsKey(connectionName))
            {
                throw new KeyNotFoundException($"Data source '{connectionName}' not found in configuration. Available data sources: {(config?.DataSourceConfigs != null ? string.Join(", ", config.DataSourceConfigs.Keys) : "none")}");
            }

            var dataSource = config.DataSourceConfigs[connectionName];
            string dbType = dataSource.DbType?.ToLower().Trim() ?? "postgresql";
            
            return dbType;
        }

        /// <summary>
        /// Clears the provider cache, forcing providers to be recreated on next access
        /// </summary>
        public static void ClearProviderCache()
        {
            lock (_lock)
            {
                _providers.Clear();
                Logger.Debug("Provider cache cleared");
            }
        }

        /// <summary>
        /// Gets the number of cached providers
        /// </summary>
        /// <returns>The number of providers currently in the cache</returns>
        public static int GetCacheSize()
        {
            lock (_lock)
            {
                return _providers.Count;
            }
        }

        /// <summary>
        /// Removes a specific provider from the cache
        /// </summary>
        /// <param name="connectionName">The name of the connection whose provider should be removed</param>
        /// <returns>True if the provider was removed, false if it wasn't in the cache</returns>
        public static bool RemoveProvider(string connectionName)
        {
            lock (_lock)
            {
                bool removed = _providers.Remove(connectionName);
                if (removed)
                {
                    Logger.Debug($"Provider for connection '{connectionName}' removed from cache");
                }
                return removed;
            }
        }
    }
} 