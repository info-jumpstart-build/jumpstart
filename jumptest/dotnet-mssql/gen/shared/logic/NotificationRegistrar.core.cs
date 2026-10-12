using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    /// <summary>
    /// Registers an AfterAction callback with Proxy to publish property update notifications
    /// to the API server's notification endpoint.
    /// </summary>
    public static class NotificationRegistrar
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static List<string> _cachedApiServerUrls = null;
        private static readonly object _cacheLock = new object();
        private static DateTime _cacheExpiry = DateTime.MinValue;
        private static readonly TimeSpan CacheTimeout = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Registers the notification callback with the Proxy class.
        /// This should be called once during application startup.
        /// </summary>
        public static void Register()
        {
            ProxyAction.AddAfterAction((domainObj, method, result, args) =>
            {
                
                // Fire-and-forget async operation to avoid blocking
                _ = global::System.Threading.Tasks.Task.Run(async () =>
                {
                    try
                    {
                        // Only process methods that return BaseObject or update BaseObject instances
                        if (result is BaseObject baseObject)
                        {
                            await PublishPropertyUpdates(domainObj, baseObject, method, args);
                        }
                        // Also check if any argument is a BaseObject that was updated
                        else if (args != null)
                        {
                            foreach (var arg in args)
                            {
                                if (arg is BaseObject argObject)
                                {
                                    await PublishPropertyUpdates(domainObj, argObject, method, args);
                                    break; // Only process first BaseObject found
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log but don't throw - don't break the original method execution
                        Logger.Error($"Error in NotificationRegistrar AfterAction: {ex.Message}", ex);
                    }
                });
            });

            Logger.Info("NotificationRegistrar: Registered AfterAction callback with Proxy");
        }

        /// <summary>
        /// Publishes property update notifications for a BaseObject
        /// </summary>
        private static async global::System.Threading.Tasks.Task PublishPropertyUpdates(string domainObj, BaseObject baseObject, MethodInfo method, object[] args)
        {
            // Only handle insert/update operations. If this callback is for another method,
            // skip publishing notifications.
            if (method == null)
            {
                return;
            }

            string methodName = method.Name ?? string.Empty;
            if (!methodName.StartsWith("Insert", StringComparison.OrdinalIgnoreCase)
                && !methodName.StartsWith("Update", StringComparison.OrdinalIgnoreCase))
            {
                // Not an insert or update method - nothing to publish
                return;
            }

            // Use the domainObj parameter passed from the callback, or fallback to extraction
            string domainObjectName = domainObj;
            if (string.IsNullOrEmpty(domainObjectName))
            {
                // Fallback: Extract domain object name from the method's declaring type or BaseObject
                domainObjectName = GetDomainObjectName(baseObject, method);
                if (string.IsNullOrEmpty(domainObjectName))
                {
                    return; // Can't determine domain object name
                }
            }

            // Get instance ID
            long instanceId = GetInstanceId(baseObject);
            if (instanceId == 0)
            {
                return; // No instance ID available
            }

            // Get JSON representation of the entire BaseObject using ToString()
            string jsonData = baseObject.ToString();
            
            // Publish notification to all API servers with the entire object as JSON
            await PublishNotificationToAll(domainObjectName, instanceId, jsonData);
        }

        /// <summary>
        /// Gets all API server URLs, with caching for performance
        /// </summary>
        private static async Task<List<string>> GetApiServerUrls()
        {
            lock (_cacheLock)
            {
                // Check cache first
                if (_cachedApiServerUrls != null && DateTime.UtcNow < _cacheExpiry)
                {
                    return new List<string>(_cachedApiServerUrls); // Return a copy
                }
            }

            try
            {
                // Query for all online API servers
                List<ServerNode> apiServers = DBPersist.select<ServerNode>(
                    $"SELECT * FROM core.server_node WHERE server_node_type_id = {(long)ServerNode.ServerNodeType.ApiServer} " +
                    $"AND server_node_status_id = {(long)ServerNode.ServerNodeStatus.Online} " +
                    $"AND is_active = 1 " +
                    $"ORDER BY registered_at DESC");

                if (apiServers != null && apiServers.Count > 0)
                {
                    var urls = new List<string>();
                    foreach (var apiServer in apiServers)
                    {
                        if (!string.IsNullOrEmpty(apiServer.url))
                        {
                            urls.Add(apiServer.url);
                        }
                    }
                    
                    lock (_cacheLock)
                    {
                        _cachedApiServerUrls = urls;
                        _cacheExpiry = DateTime.UtcNow.Add(CacheTimeout);
                    }

                    return new List<string>(urls); // Return a copy
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"NotificationRegistrar: Error looking up API servers: {ex.Message}", ex);
            }

            return new List<string>(); // Return empty list if no servers found
        }

        /// <summary>
        /// Extracts the domain object name from the BaseObject or method
        /// </summary>
        private static string GetDomainObjectName(BaseObject baseObject, MethodInfo method)
        {
            // Try to get from BaseObject's table name or type
            if (baseObject.ContainsKey("tableName"))
            {
                string tableName = baseObject["tableName"]?.ToString();
                if (!string.IsNullOrEmpty(tableName))
                {
                    // Convert table name to domain object name (e.g., "core.scriptagent" -> "ScriptAgent")
                    string[] parts = tableName.Split('.');
                    if (parts.Length > 0)
                    {
                        string lastPart = parts[parts.Length - 1];
                        // Capitalize first letter
                        return char.ToUpper(lastPart[0]) + lastPart.Substring(1);
                    }
                }
            }

            // Fallback: try to get from method's declaring type
            if (method?.DeclaringType != null)
            {
                string typeName = method.DeclaringType.Name;
                // Remove "Logic" suffix if present (e.g., "ScriptAgentLogic" -> "ScriptAgent")
                if (typeName.EndsWith("Logic"))
                {
                    return typeName.Substring(0, typeName.Length - 5);
                }
                return typeName;
            }

            return null;
        }

        /// <summary>
        /// Gets the instance ID from a BaseObject
        /// </summary>
        private static long GetInstanceId(BaseObject baseObject)
        {
            if (baseObject.ContainsKey("id"))
            {
                var idValue = baseObject["id"];
                if (idValue != null)
                {
                    if (long.TryParse(idValue.ToString(), out long id))
                    {
                        return id;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Publishes a notification to all API servers
        /// </summary>
        private static async global::System.Threading.Tasks.Task PublishNotificationToAll(string domainObjectName, long instanceId, string jsonData)
        {
            // Get all API server URLs
            List<string> apiServerUrls = await GetApiServerUrls();
            
            if (apiServerUrls == null || apiServerUrls.Count == 0)
            {
                Logger.Warn("NotificationRegistrar: No API servers found, skipping notification");
                return;
            }

            // Build notification request once
            var notificationRequest = new
            {
                DomainObjectName = domainObjectName,
                InstanceId = instanceId,
                JsonData = jsonData
            };

            // Serialize to JSON once (reuse for all servers)
            string json = JsonSerializer.Serialize(notificationRequest);

            // Broadcast to all API servers in parallel
            List<global::System.Threading.Tasks.Task> publishTasks = new List<global::System.Threading.Tasks.Task>();
            foreach (string apiServerUrl in apiServerUrls)
            {
                publishTasks.Add(PublishNotification(apiServerUrl, json, domainObjectName, instanceId));
            }

            // Wait for all publish operations to complete (don't throw on individual failures)
            await global::System.Threading.Tasks.Task.WhenAll(publishTasks);
        }

        /// <summary>
        /// Publishes a notification to a single API server
        /// </summary>
        private static async global::System.Threading.Tasks.Task PublishNotification(string apiServerUrl, string json,
            string domainObjectName, long instanceId)
        {
            try
            {
                // Create content for this specific request (StringContent can only be used once)
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make HTTP POST request
                string url = $"{apiServerUrl.TrimEnd('/')}/api/Notification/publish";
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Logger.Warn($"NotificationRegistrar: Failed to publish notification to {apiServerUrl}. Status: {response.StatusCode}, Response: {responseBody}");
                }
                else
                {
                    Logger.Debug($"NotificationRegistrar: Published notification for {domainObjectName}[{instanceId}] to {apiServerUrl}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"NotificationRegistrar: Error publishing notification to {apiServerUrl}: {ex.Message}", ex);
            }
        }
    }
}

