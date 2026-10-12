using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace jumptest
{
    // ──────────────────────────────────────────────────────────────────────────
    // M2MTokenProvider
    //
    // Singleton that fetches and caches Auth0 Client-Credentials tokens for
    // machine-to-machine (server → server) HTTP calls.
    //
    // Configuration (appsettings.json):
    //
    //   "M2MClients": {
    //     "Scheduler": {
    //       "TokenEndpoint": "https://YOUR-TENANT.auth0.com/oauth/token",
    //       "ClientId":      "...",
    //       "ClientSecret":  "...",
    //       "Audience":      "https://scheduler-api"
    //     },
    //     "ScriptAgent": {
    //       "TokenEndpoint": "https://YOUR-TENANT.auth0.com/oauth/token",
    //       "ClientId":      "...",
    //       "ClientSecret":  "...",
    //       "Audience":      "https://scriptagent-api"
    //     }
    //   }
    //
    // Usage in Program.cs:
    //   M2MTokenProvider.Initialize(builder.Configuration);
    //
    // Tokens are cached until 60 s before expiry. Thread-safe: concurrent
    // callers for the same service key share a per-key lock so only one
    // refresh call reaches Auth0 at a time.
    // ──────────────────────────────────────────────────────────────────────────

    public class M2MTokenProvider
    {
        // ── Internal types ───────────────────────────────────────────────────

        private class TokenCache
        {
            public string  Token     { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; } = DateTime.MinValue;

            // Consider the token valid until 60 seconds before actual expiry
            public bool IsValid =>
                !string.IsNullOrEmpty(Token) &&
                DateTime.UtcNow < ExpiresAt.AddSeconds(-60);
        }

        private class TokenResponse
        {
            [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
            [JsonPropertyName("expires_in")]   public int    ExpiresIn   { get; set; }
            [JsonPropertyName("token_type")]   public string TokenType   { get; set; } = string.Empty;
        }

        private class ServiceConfig
        {
            public string TokenEndpoint { get; set; } = string.Empty;
            public string ClientId      { get; set; } = string.Empty;
            public string ClientSecret  { get; set; } = string.Empty;
            public string Audience      { get; set; } = string.Empty;
        }

        // ── Singleton ────────────────────────────────────────────────────────

        private static M2MTokenProvider? _instance;

        /// <summary>
        /// The active provider instance, or null if M2M has not been configured.
        /// </summary>
        public static M2MTokenProvider? Instance => _instance;

        /// <summary>
        /// Call once at startup (e.g. in Program.cs) to configure the provider.
        /// If the "M2MClients" configuration section is absent or empty, the
        /// provider is not initialized and all client calls proceed without tokens.
        /// </summary>
        public static void Initialize(IConfiguration configuration)
        {
            var section = configuration.GetSection("M2MClients");
            if (section.Exists())
            {
                _instance = new M2MTokenProvider(configuration);
                Console.WriteLine("M2MTokenProvider: initialized (machine-to-machine auth enabled).");
            }
            else
            {
                Console.WriteLine("M2MTokenProvider: M2MClients config section not found — inter-service calls will be unauthenticated.");
            }
        }

        // ── Instance state ───────────────────────────────────────────────────

        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, TokenCache>     _cache = new();
        private readonly Dictionary<string, SemaphoreSlim>  _locks = new();
        private readonly object _mapLock = new();

        private M2MTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ── Public API ───────────────────────────────────────────────────────

        /// <summary>
        /// Returns a valid Bearer token for the named service (e.g. "Scheduler",
        /// "ScriptAgent"). Returns null if the service is not configured.
        /// Tokens are cached; Auth0 is only contacted when the cached token is
        /// within 60 s of expiry.
        /// </summary>
        public async Task<string?> GetTokenAsync(string serviceKey)
        {
            SemaphoreSlim semaphore;
            TokenCache    cache;

            // Get or create per-service lock and cache entry (under a fast object lock)
            lock (_mapLock)
            {
                if (!_locks.ContainsKey(serviceKey))
                {
                    _locks[serviceKey] = new SemaphoreSlim(1, 1);
                    _cache[serviceKey] = new TokenCache();
                }
                semaphore = _locks[serviceKey];
                cache     = _cache[serviceKey];
            }

            // Fast path — cached token is still valid
            if (cache.IsValid)
                return cache.Token;

            // Slow path — acquire per-service lock so only one thread refreshes
            await semaphore.WaitAsync();
            try
            {
                // Double-check after acquiring the lock
                if (cache.IsValid)
                    return cache.Token;

                var fresh = await FetchTokenAsync(serviceKey);
                if (fresh != null)
                {
                    lock (_mapLock) { _cache[serviceKey] = fresh; }
                    return fresh.Token;
                }
                return null;
            }
            finally
            {
                semaphore.Release();
            }
        }

        // ── Private helpers ──────────────────────────────────────────────────

        private async Task<TokenCache?> FetchTokenAsync(string serviceKey)
        {
            var cfg = ReadConfig(serviceKey);
            if (cfg == null)
            {
                Console.WriteLine($"M2MTokenProvider: no config found for service key '{serviceKey}'.");
                return null;
            }

            try
            {
                using var http     = new HttpClient();
                var       payload  = new Dictionary<string, string>
                {
                    ["grant_type"]    = "client_credentials",
                    ["client_id"]     = cfg.ClientId,
                    ["client_secret"] = cfg.ClientSecret,
                    ["audience"]      = cfg.Audience
                };

                var response = await http.PostAsJsonAsync(cfg.TokenEndpoint, payload);
                response.EnsureSuccessStatusCode();

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    Console.WriteLine($"M2MTokenProvider: empty response from Auth0 for '{serviceKey}'.");
                    return null;
                }

                Console.WriteLine($"M2MTokenProvider: obtained fresh token for '{serviceKey}' (expires in {tokenResponse.ExpiresIn}s).");
                return new TokenCache
                {
                    Token     = tokenResponse.AccessToken,
                    ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"M2MTokenProvider: token fetch failed for '{serviceKey}': {ex.Message}");
                return null;
            }
        }

        private ServiceConfig? ReadConfig(string serviceKey)
        {
            var section = _configuration.GetSection($"M2MClients:{serviceKey}");
            if (!section.Exists()) return null;

            var cfg = new ServiceConfig
            {
                TokenEndpoint = section["TokenEndpoint"] ?? string.Empty,
                ClientId      = section["ClientId"]      ?? string.Empty,
                ClientSecret  = section["ClientSecret"]  ?? string.Empty,
                Audience      = section["Audience"]      ?? string.Empty
            };

            if (string.IsNullOrEmpty(cfg.TokenEndpoint) ||
                string.IsNullOrEmpty(cfg.ClientId)      ||
                string.IsNullOrEmpty(cfg.ClientSecret))
                return null;

            return cfg;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // M2MAuthHandler
    //
    // DelegatingHandler that attaches a Bearer token from M2MTokenProvider to
    // every outgoing HTTP request. Used internally by SchedulerClient and
    // ScriptAgentClient when M2MTokenProvider is initialized.
    // ──────────────────────────────────────────────────────────────────────────

    public class M2MAuthHandler : DelegatingHandler
    {
        private readonly M2MTokenProvider _provider;
        private readonly string           _serviceKey;

        public M2MAuthHandler(M2MTokenProvider provider, string serviceKey)
            : base(new HttpClientHandler())
        {
            _provider   = provider;
            _serviceKey = serviceKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken  cancellationToken)
        {
            var token = await _provider.GetTokenAsync(_serviceKey);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
