using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace jumptest
{
    /// <summary>
    /// REST client for communicating with the Scheduler service
    /// </summary>
    public class SchedulerClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ServerNode _serverNode;

        public SchedulerClient(ServerNode serverNode)
        {
            _serverNode = serverNode ?? throw new ArgumentNullException(nameof(serverNode));
            if (string.IsNullOrEmpty(_serverNode.url))
            {
                throw new Exception("Scheduler URL is not set for server node " + _serverNode.id);
            }

            // Attach M2M Bearer token if provider is configured (covers autonomous scheduler thread)
            var handler = M2MTokenProvider.Instance != null
                ? (HttpMessageHandler) new M2MAuthHandler(M2MTokenProvider.Instance, "Scheduler")
                : new HttpClientHandler();
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(_serverNode.url),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        /// <summary>
        /// Schedule a new job
        /// </summary>
        public async Task<long> ScheduleAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.ScheduleAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/schedule", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Execute a job immediately
        /// </summary>
        public async Task<long> ExecuteAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.ExecuteAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/execute", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Cancel a scheduled job
        /// </summary>
        public async global::System.Threading.Tasks.Task CancelAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.CancelAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/cancel/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Pause a running job
        /// </summary>
        public async global::System.Threading.Tasks.Task PauseAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.PauseAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/pause/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Resume a paused job
        /// </summary>
        public async global::System.Threading.Tasks.Task ResumeAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.ResumeAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/resume/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Retry a failed job
        /// </summary>
        public async global::System.Threading.Tasks.Task RetryAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.RetryAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/retry/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Abort a running job
        /// </summary>
        public async global::System.Threading.Tasks.Task AbortAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.AbortAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/abort/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Get status of a workflow
        /// </summary>
        public async Task<object?> GetStatusAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.GetStatusAsync: workflowId={workflowId}");
            var response = await _httpClient.GetAsync($"/api/scheduler/status/{workflowId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// List workflows with optional filters
        /// </summary>
        public async Task<List<long>?> ListAsync(string? status = null, string? priority = null)
        {
            Logger.Debug($"SchedulerClient.ListAsync: status={status}, priority={priority}");
            var query = new List<string>();
            if (!string.IsNullOrEmpty(status)) query.Add($"status={status}");
            if (!string.IsNullOrEmpty(priority)) query.Add($"priority={priority}");
            
            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : "";
            var response = await _httpClient.GetAsync($"/api/scheduler/list{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Monitor a workflow
        /// </summary>
        public async Task<object?> MonitorAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.MonitorAsync: workflowId={workflowId}");
            var response = await _httpClient.GetAsync($"/api/scheduler/monitor/{workflowId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Query workflows by date range
        /// </summary>
        public async Task<List<long>?> QueryAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            Logger.Debug($"SchedulerClient.QueryAsync: startDate={startDate}, endDate={endDate}");
            var query = new List<string>();
            if (startDate.HasValue) query.Add($"startDate={startDate.Value:yyyy-MM-dd}");
            if (endDate.HasValue) query.Add($"endDate={endDate.Value:yyyy-MM-dd}");
            
            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : "";
            var response = await _httpClient.GetAsync($"/api/scheduler/query{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Register a worker node
        /// </summary>
        public async global::System.Threading.Tasks.Task RegisterAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.RegisterAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/register", workflowId);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Unregister a worker node
        /// </summary>
        public async global::System.Threading.Tasks.Task UnregisterAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.UnregisterAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/unregister/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Get health status
        /// </summary>
        public async Task<object?> GetHealthAsync()
        {
            Logger.Debug($"SchedulerClient.GetHealthAsync");
            var response = await _httpClient.GetAsync("/api/scheduler/health");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Queue a workflow
        /// </summary>
        public async Task<long> QueueAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.QueueAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/queue", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Get a workflow by ID
        /// </summary>
        public async Task<object?> GetWorkflowAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.GetWorkflowAsync: workflowId={workflowId}");
            var response = await _httpClient.GetAsync($"/api/scheduler/workflow/{workflowId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        /// <summary>
        /// Get all workflows
        /// </summary>
        public async Task<List<long>?> GetAllWorkflowsAsync()
        {
            Logger.Debug($"SchedulerClient.GetAllWorkflowsAsync");
            var response = await _httpClient.GetAsync("/api/scheduler/workflows");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Create a new workflow
        /// </summary>
        public async Task<long> CreateWorkflowAsync(object workflowDefinition)
        {
            Logger.Debug($"SchedulerClient.CreateWorkflowAsync");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/workflow", workflowDefinition);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Update a workflow
        /// </summary>
        public async global::System.Threading.Tasks.Task UpdateWorkflowAsync(long workflowId, object workflowDefinition)
        {
            Logger.Debug($"SchedulerClient.UpdateWorkflowAsync: workflowId={workflowId}");
            var response = await _httpClient.PutAsJsonAsync($"/api/scheduler/workflow/{workflowId}", workflowDefinition);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Delete a workflow
        /// </summary>
        public async global::System.Threading.Tasks.Task DeleteWorkflowAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.DeleteWorkflowAsync: workflowId={workflowId}");
            var response = await _httpClient.DeleteAsync($"/api/scheduler/workflow/{workflowId}");
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Allocate resources for a workflow
        /// </summary>
        public async Task<long> AllocateAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.AllocateAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/allocate", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Deallocate resources for a workflow
        /// </summary>
        public async global::System.Threading.Tasks.Task DeallocateAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.DeallocateAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/deallocate/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Balance load across agents
        /// </summary>
        public async global::System.Threading.Tasks.Task BalanceAsync()
        {
            Logger.Debug($"SchedulerClient.BalanceAsync");
            var response = await _httpClient.PostAsync("/api/scheduler/balance", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Scale resources
        /// </summary>
        public async global::System.Threading.Tasks.Task ScaleAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.ScaleAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/scale", workflowId);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Wait for dependencies
        /// </summary>
        public async Task<long> WaitAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.WaitAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/wait", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Trigger dependent workflows
        /// </summary>
        public async global::System.Threading.Tasks.Task TriggerAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.TriggerAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/trigger/{workflowId}", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Chain multiple workflows
        /// </summary>
        public async Task<List<long>?> ChainAsync(List<long> workflowIds)
        {
            Logger.Debug($"SchedulerClient.ChainAsync: count={workflowIds?.Count}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/chain", workflowIds);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Fork workflow into sub-workflows
        /// </summary>
        public async Task<List<long>?> ForkAsync(long workflowId, List<long> subWorkflowIds)
        {
            Logger.Debug($"SchedulerClient.ForkAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync($"/api/scheduler/fork/{workflowId}", subWorkflowIds);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Join parallel workflows
        /// </summary>
        public async Task<long> JoinAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.JoinAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsync($"/api/scheduler/join/{workflowId}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Schedule recurring workflow
        /// </summary>
        public async Task<long> RepeatAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.RepeatAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/repeat", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Batch multiple workflows
        /// </summary>
        public async Task<List<long>?> BatchAsync(List<long> workflowIds)
        {
            Logger.Debug($"SchedulerClient.BatchAsync: count={workflowIds?.Count}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/batch", workflowIds);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<long>>();
        }

        /// <summary>
        /// Prioritize a workflow
        /// </summary>
        public async global::System.Threading.Tasks.Task PrioritizeAsync(long workflowId, int priority)
        {
            Logger.Debug($"SchedulerClient.PrioritizeAsync: workflowId={workflowId}, priority={priority}");
            var response = await _httpClient.PostAsJsonAsync($"/api/scheduler/prioritize/{workflowId}", priority);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Recover from failures
        /// </summary>
        public async global::System.Threading.Tasks.Task RecoverAsync()
        {
            Logger.Debug($"SchedulerClient.RecoverAsync");
            var response = await _httpClient.PostAsync("/api/scheduler/recover", null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Migrate workflow to another agent
        /// </summary>
        public async Task<long> MigrateAsync(long workflowId, string targetAgent)
        {
            Logger.Debug($"SchedulerClient.MigrateAsync: workflowId={workflowId}, targetAgent={targetAgent}");
            var response = await _httpClient.PostAsJsonAsync($"/api/scheduler/migrate/{workflowId}", targetAgent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<long>();
        }

        /// <summary>
        /// Configure workflow
        /// </summary>
        public async global::System.Threading.Tasks.Task ConfigureAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.ConfigureAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/configure", workflowId);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Update workflow configuration
        /// </summary>
        public async global::System.Threading.Tasks.Task UpdateAsync(long workflowId, object updateData)
        {
            Logger.Debug($"SchedulerClient.UpdateAsync: workflowId={workflowId}");
            var response = await _httpClient.PutAsJsonAsync($"/api/scheduler/update/{workflowId}", updateData);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Validate workflow
        /// </summary>
        public async Task<object?> ValidateAsync(long workflowId)
        {
            Logger.Debug($"SchedulerClient.ValidateAsync: workflowId={workflowId}");
            var response = await _httpClient.PostAsJsonAsync("/api/scheduler/validate", workflowId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<object>();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

