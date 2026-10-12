
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace jumptest
{
    public class DataSourceConfig
    {
        [JsonPropertyName("dbtype")]
        public string DbType { get; set; }
        
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }
        
        [JsonPropertyName("port")]
        public string Port { get; set; }
        
        [JsonPropertyName("database")]
        public string Database { get; set; }
        
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
    
    public class NamespaceConfig
    {
        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }

        [JsonPropertyName("datasources")]
        public Dictionary<string, DataSourceConfig> DataSourceConfigs { get; set; }

        [JsonPropertyName("network_interface")]
        public int? NetworkInterface { get; set; }
    }

    public class Config
    {
        static IConfiguration configBuilder = null;
        static NamespaceConfig _namespaceConfig = null;
        static string _currentDataSource = "default";

        static IConfiguration getConfigBuilder()
        {
            if (configBuilder == null)
            {
                configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            }

            return configBuilder;
        }
        
        static public string getString(string param)
        {
            string result = string.Empty;

            if (param == "db.connection")
            {
                result = getDbConnection();
            }
            else if (param == "dbType" || param == "server" || param == "port" || 
                     param == "database" || param == "username" || param == "password")
            {
                // Get database parameter from current data source
                var config = LoadNamespaceConfig();
                if (config?.DataSourceConfigs != null && config.DataSourceConfigs.ContainsKey(_currentDataSource))
                {
                    var dataSource = config.DataSourceConfigs[_currentDataSource];
                    result = param switch
                    {
                        "dbType" => dataSource.DbType?.ToLower().Trim() ?? "postgresql",
                        "server" => dataSource.Hostname ?? "localhost",
                        "port" => dataSource.Port ?? "5432",
                        "database" => dataSource.Database ?? "",
                        "username" => dataSource.Username ?? "",
                        "password" => dataSource.Password ?? "",
                        _ => string.Empty
                    };
                }
            }
            else
            {
                IConfigurationSection section = getConfigBuilder().GetSection("appsettings");
                result = section[param];
            }

            return result;
        }

        static public string getString(string param, string defaultValue)
        {
            string result = getString(param);
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        static public void setDataSource(string dataSourceName)
        {
            _currentDataSource = dataSourceName;
        }

        static public string getCurrentDataSource()
        {
            return _currentDataSource;
        }

        static public NamespaceConfig LoadNamespaceConfig()
        {
            if (_namespaceConfig == null)
            {
                // Path to the .namespace.json file in the user's home directory
                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string jsonFilePath = Path.Combine(homeDirectory, ".jumptest.json");
                string legacyFilePath = Path.Combine(homeDirectory, ".jumptest");

                // Check for JSON file first, fall back to legacy format
                if (File.Exists(jsonFilePath))
                {
                    // Read and parse JSON file
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    _namespaceConfig = JsonSerializer.Deserialize<NamespaceConfig>(jsonContent);
                    
                    if (_namespaceConfig == null || _namespaceConfig.DataSourceConfigs == null || _namespaceConfig.DataSourceConfigs.Count == 0)
                    {
                        throw new FormatException("The .jumptest.json file is invalid or contains no data sources.");
                    }
                }
                else if (File.Exists(legacyFilePath))
                {
                    // Parse legacy format for backward compatibility
                    _namespaceConfig = parseLegacyFormat(legacyFilePath);
                }
                else
                {
                    throw new FileNotFoundException("Neither .jumptest.json nor .jumptest file was found in the user's home directory.", jsonFilePath);
                }
            }
            
            return _namespaceConfig;
        }

        static private NamespaceConfig parseLegacyFormat(string filePath)
        {
            // Read the legacy file content
            string fileContent = File.ReadAllText(filePath).Trim();

            // Split the content by colon (:) to extract parameters
            string[] parameters = fileContent.Split(':');
            if (parameters.Length != 4 && parameters.Length != 6)
            {
                throw new FormatException("The .jumptest file must contain either 4 or 6 parameters separated by colons (:).\nFormat: dbtype:server:port:database[:username:password]");
            }

            // Create a NamespaceConfig with a single "default" data source
            var config = new NamespaceConfig
            {
                Namespace = "jumptest",
                DataSourceConfigs = new Dictionary<string, DataSourceConfig>
                {
                    ["default"] = new DataSourceConfig
                    {
                        DbType = parameters[0].ToLower().Trim(),
                        Hostname = parameters[1].Trim(),
                        Port = parameters[2].Trim(),
                        Database = parameters[3].Trim(),
                        Username = parameters.Length >= 5 ? parameters[4].Trim() : string.Empty,
                        Password = parameters.Length >= 6 ? parameters[5].Trim() : string.Empty
                    }
                }
            };

            return config;
        }

        static public string getDbConnection()
        {
            return getDbConnection(_currentDataSource);
        }

        static public string getDbConnection(string dataSourceName)
        {
            // Load the namespace configuration
            var config = LoadNamespaceConfig();
            
            // Get the requested data source
            if (!config.DataSourceConfigs.ContainsKey(dataSourceName))
            {
                throw new KeyNotFoundException($"Data source '{dataSourceName}' not found in configuration. Available data sources: {string.Join(", ", config.DataSourceConfigs.Keys)}");
            }
            
            var dataSource = config.DataSourceConfigs[dataSourceName];

            // Get values directly from dataSource
            string dbType = dataSource.DbType?.ToLower().Trim() ?? "postgresql";
            string server = dataSource.Hostname ?? "localhost";
            string port = dataSource.Port ?? "5432";
            string database = dataSource.Database ?? "";
            string username = dataSource.Username ?? "";
            string password = dataSource.Password ?? "";

            // Create the connection string based on database type and whether username/password are provided
            string dbcon;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Full connection string with username and password
                dbcon = getConnectionStringTemplate(dbType, true);
            }
            else
            {
                // Connection string without username and password (for trusted connections)
                dbcon = getConnectionStringTemplate(dbType, false);
            }

            // Replace placeholders with actual values
            dbcon = dbcon.Replace("^server", server)
                        .Replace("^port", port)
                        .Replace("^database", database);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                dbcon = dbcon.Replace("^username", username)
                            .Replace("^password", password);
            }

            return dbcon;
        }

        static private string getConnectionStringTemplate(string dbType, bool includeCredentials)
        {
            switch (dbType)
            {
                case "sqlserver":
                case "mssql":
                    if (includeCredentials)
                    {
                        return "Server=^server,^port;Database=^database;User Id=^username;Password=^password;TrustServerCertificate=true;";
                    }
                    else
                    {
                        return "Server=^server,^port;Database=^database;Trusted_Connection=true;TrustServerCertificate=true;";
                    }

                case "postgresql":
                case "pgsql":
                default:
                    if (includeCredentials)
                    {
                        return "Host=^server;Port=^port;Database=^database;Username=^username;Password=^password;";
                    }
                    else
                    {
                        return "Host=^server;Port=^port;Database=^database;";
                    }
            }
        }
        
        static public int getInt(string param)
        {
            IConfigurationSection section = getConfigBuilder().GetSection("appsettings");

            return Convert.ToInt32(section[param]);
        }

        /// <summary>
        /// Returns the IPv4 address of the configured network interface.
        /// Reads the network_interface index from .namespace.json (default: 0).
        /// </summary>
        static public IPAddress getNetworkInterfaceIP()
        {
            var config = LoadNamespaceConfig();
            int interfaceIndex = config.NetworkInterface ?? 0;

            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up
                          && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .ToArray();

            if (interfaces.Length == 0)
            {
                Console.WriteLine("WARNING: No active network interfaces found, falling back to loopback");
                return IPAddress.Loopback;
            }

            if (interfaceIndex >= interfaces.Length)
            {
                Console.WriteLine($"WARNING: Network interface index {interfaceIndex} out of range (found {interfaces.Length} interfaces), using interface 0");
                interfaceIndex = 0;
            }

            var selectedInterface = interfaces[interfaceIndex];
            var ipProps = selectedInterface.GetIPProperties();
            var ipv4Address = ipProps.UnicastAddresses
                .FirstOrDefault(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork);

            if (ipv4Address == null)
            {
                Console.WriteLine($"WARNING: No IPv4 address found on interface '{selectedInterface.Name}', falling back to loopback");
                return IPAddress.Loopback;
            }

            Console.WriteLine($"Using network interface {interfaceIndex}: {selectedInterface.Name} ({ipv4Address.Address})");
            return ipv4Address.Address;
        }
    }
}
