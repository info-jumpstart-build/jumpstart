using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace jumptest
{
    /// <summary>
    /// REST client for communicating with Agent services
    /// </summary>
    public class ScriptAgentClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ServerNode _serverNode;

        public ScriptAgentClient(ServerNode serverNode)
        {
            _serverNode = serverNode ?? throw new ArgumentNullException(nameof(serverNode));
            if (string.IsNullOrEmpty(_serverNode.url))
            {
                throw new Exception("ScriptAgent URL is not set for server node " + _serverNode.id);
            }

            // Attach M2M Bearer token if provider is configured
            // (covers both interactive API calls and autonomous Scheduler thread invocations)
            var handler = M2MTokenProvider.Instance != null
                ? (HttpMessageHandler) new M2MAuthHandler(M2MTokenProvider.Instance, "ScriptAgent")
                : new HttpClientHandler();
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(_serverNode.url),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        // =====================================
        // Process Management Operations
        // =====================================


        /// <summary>
        /// Stop a running process
        /// </summary>
        public async global::System.Threading.Tasks.Task StopAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/stop/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Kill a process forcefully
        /// </summary>
        public async global::System.Threading.Tasks.Task KillAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/kill/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Restart a process
        /// </summary>
        public async global::System.Threading.Tasks.Task RestartAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/restart/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Pause a process
        /// </summary>
        public async global::System.Threading.Tasks.Task PauseAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/pause/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Resume a paused process
        /// </summary>
        public async global::System.Threading.Tasks.Task ResumeAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/resume/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        // =====================================
        // Status & Reporting Operations
        // =====================================

        /// <summary>
        /// Get status of a process
        /// </summary>
        public async Task<object?> GetStatusAsync(long executionId)
        {
            var response = await _httpClient.GetAsync($"/api/scriptagent/status/{executionId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Send heartbeat
        /// </summary>
        public async global::System.Threading.Tasks.Task HeartbeatAsync(object heartbeatData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/heartbeat", heartbeatData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Send a report
        /// </summary>
        public async global::System.Threading.Tasks.Task ReportAsync(object reportData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/report", reportData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Send log entry
        /// </summary>
        public async global::System.Threading.Tasks.Task LogAsync(object logData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/log", logData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Send metrics
        /// </summary>
        public async global::System.Threading.Tasks.Task MetricsAsync(object metricsData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/metrics", metricsData);
            response.EnsureSuccessStatusCode();
        }

        // =====================================
        // Resource Management Operations
        // =====================================

        /// <summary>
        /// Register agent with scheduler
        /// </summary>
        public async Task<long> RegisterAsync(object registrationData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/register", registrationData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Unregister agent
        /// </summary>
        public async global::System.Threading.Tasks.Task UnregisterAsync(long agentId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/unregister/{agentId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Get agent capabilities
        /// </summary>
        public async Task<object?> GetCapabilitiesAsync()
        {
            var response = await _httpClient.GetAsync("/api/scriptagent/capabilities");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Allocate resources
        /// </summary>
        public async Task<long> AllocateAsync(long executionId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/allocate", executionId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public async global::System.Threading.Tasks.Task ReleaseAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/release/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        // =====================================
        // Job Execution Operations
        // =====================================

        /// <summary>
        /// Execute a job
        /// </summary>
        public async Task<long> ExecuteAsync(long executionId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/execute", executionId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Validate job requirements
        /// </summary>
        public async Task<object?> ValidateAsync(long executionId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/validate", executionId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Prepare for job execution
        /// </summary>
        public async Task<long> PrepareAsync(long executionId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/prepare", executionId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Clean up after job
        /// </summary>
        public async global::System.Threading.Tasks.Task CleanupAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/cleanup/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Retry a failed job
        /// </summary>
        public async global::System.Threading.Tasks.Task RetryAsync(long executionId)
        {
            var response = await _httpClient.PostAsync($"/api/scriptagent/retry/{executionId}", null);
            response.EnsureSuccessStatusCode();
        }

        // =====================================
        // Communication Operations
        // =====================================

        /// <summary>
        /// Ping the agent
        /// </summary>
        public async Task<object?> PingAsync()
        {
            var response = await _httpClient.GetAsync("/api/scriptagent/ping");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Acknowledge a command
        /// </summary>
        public async global::System.Threading.Tasks.Task AcknowledgeAsync(long executionId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/acknowledge", executionId);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Send notification
        /// </summary>
        public async global::System.Threading.Tasks.Task NotifyAsync(object notificationData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/notify", notificationData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Request resources or information
        /// </summary>
        public async Task<object?> RequestAsync(object requestData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/request", requestData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        // =====================================
        // System Operations
        // =====================================

        /// <summary>
        /// Shutdown agent
        /// </summary>
        public async global::System.Threading.Tasks.Task ShutdownAsync()
        {
            var response = await _httpClient.PostAsync("/api/scriptagent/shutdown", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Reload agent configuration
        /// </summary>
        public async global::System.Threading.Tasks.Task ReloadAsync()
        {
            var response = await _httpClient.PostAsync("/api/scriptagent/reload", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Update agent software
        /// </summary>
        public async global::System.Threading.Tasks.Task UpdateAsync(object updateData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/update", updateData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Run diagnostics
        /// </summary>
        public async Task<object?> DiagnoseAsync()
        {
            var response = await _httpClient.GetAsync("/api/scriptagent/diagnose");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Get health status
        /// </summary>
        public async Task<object?> GetHealthAsync()
        {
            var response = await _httpClient.GetAsync("/api/scriptagent/health");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        // =====================================
        // Security Operations
        // =====================================

        /// <summary>
        /// Authenticate agent
        /// </summary>
        public async Task<object?> AuthenticateAsync(object credentials)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/authenticate", credentials);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Authorize operation
        /// </summary>
        public async Task<object?> AuthorizeAsync(object authorizationData)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/authorize", authorizationData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        // =====================================
        // Standard Operations
        // =====================================

        /// <summary>
        /// Get all executions
        /// </summary>
        public async Task<List<long>> GetExecutionsAsync()
        {
            var response = await _httpClient.GetAsync("/api/scriptagent/executions");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Get an execution by ID
        /// </summary>
        public async Task<object?> GetExecutionAsync(long executionId)
        {
            var response = await _httpClient.GetAsync($"/api/scriptagent/execution/{executionId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Get agent information
        /// </summary>
        public async Task<object?> GetAgentInfoAsync()
        {
            var response = await _httpClient.GetAsync("/api/scriptagent/info");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Create a new execution
        /// </summary>
        public async Task<long> CreateExecutionAsync(object executionDefinition)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/scriptagent/execution", executionDefinition);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Update an execution
        /// </summary>
        public async global::System.Threading.Tasks.Task UpdateExecutionAsync(long executionId, object executionData)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/scriptagent/execution/{executionId}", executionData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Delete an execution
        /// </summary>
        public async global::System.Threading.Tasks.Task DeleteExecutionAsync(long executionId)
        {
            var response = await _httpClient.DeleteAsync($"/api/scriptagent/execution/{executionId}");
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

