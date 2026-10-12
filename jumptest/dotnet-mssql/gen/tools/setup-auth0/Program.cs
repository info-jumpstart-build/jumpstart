/// <summary>
/// setup-auth0 — Auth0 registration tool for jumptest
///
/// Registers all Auth0 applications, APIs, and grants needed to run the
/// generated server stack. Run this once per environment before starting
/// the servers for the first time.
///
/// Usage:
///   dotnet run -- --token <AUTH0_MGMT_TOKEN> --env <dev|staging|prod>
///
/// The Management API token is obtained from the Auth0 dashboard:
///   Applications → APIs → Auth0 Management API → Test → copy the token.
///
/// Outputs: credentials-<env>.json with all client IDs and secrets.
///          Paste those values into each server's appsettings.json.
/// </summary>

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

// ── Argument parsing ─────────────────────────────────────────────────────────

string? mgmtToken = null;
string  env       = "dev";
string  domain    = "";
string  appName   = "jumptest";

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--token" && i + 1 < args.Length)  mgmtToken = args[++i];
    if (args[i] == "--env"   && i + 1 < args.Length)  env       = args[++i];
    if (args[i] == "--domain"&& i + 1 < args.Length)  domain    = args[++i];
}

if (string.IsNullOrEmpty(mgmtToken))
{
    Console.Error.WriteLine("Error: --token <AUTH0_MANAGEMENT_API_TOKEN> is required.");
    Console.Error.WriteLine("Get it from: Auth0 Dashboard → Applications → APIs → Auth0 Management API → Test");
    Environment.Exit(1);
}

if (string.IsNullOrEmpty(domain))
{
    Console.Error.WriteLine("Error: Auth0 domain is not set. Pass --domain YOUR-TENANT.auth0.com");
    Environment.Exit(1);
}

Console.WriteLine($"Setting up Auth0 for app '{appName}', environment '{env}', domain '{domain}'");
Console.WriteLine();

// ── HTTP client ──────────────────────────────────────────────────────────────

using var http = new HttpClient();
http.BaseAddress = new Uri($"https://{domain}/api/v2/");
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mgmtToken);
http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

var credentials = new Credentials();

// ── 1. Register API resource servers (audiences) ────────────────────────────

Console.WriteLine("Step 1/4 — Registering API resource servers …");

credentials.ApiAudience        = await EnsureResourceServer($"{appName}-api",        $"https://{appName}-api",        $"{appName} REST API");
credentials.SchedulerAudience  = await EnsureResourceServer($"{appName}-scheduler",  $"https://{appName}-scheduler",  $"{appName} Scheduler");
credentials.ScriptAgentAudience = await EnsureResourceServer($"{appName}-scriptagent",$"https://{appName}-scriptagent",$"{appName} ScriptAgent");

Console.WriteLine("  ✓ API resource servers ready.");
Console.WriteLine();

// ── 2. Register the Blazor SPA application ───────────────────────────────────

Console.WriteLine("Step 2/4 — Registering Blazor SPA application …");

var spa = await EnsureApplication(
    name:        $"{appName}-web-{env}",
    appType:     "spa",
    callbacks:   new[] { "http://localhost:5000/authentication/login-callback" },
    logouts:     new[] { "http://localhost:5000/authentication/logout-callback" },
    origins:     new[] { "http://localhost:5000" }
);
credentials.SpaClientId = spa.ClientId;
Console.WriteLine($"  ✓ SPA client ID: {credentials.SpaClientId}");
Console.WriteLine();

// ── 3. Register M2M applications ─────────────────────────────────────────────

Console.WriteLine("Step 3/4 — Registering machine-to-machine applications …");

var apiM2M = await EnsureApplication(
    name:    $"{appName}-api-server-m2m-{env}",
    appType: "non_interactive"
);
credentials.ApiServerM2MClientId     = apiM2M.ClientId;
credentials.ApiServerM2MClientSecret = apiM2M.ClientSecret;

var schedulerM2M = await EnsureApplication(
    name:    $"{appName}-scheduler-m2m-{env}",
    appType: "non_interactive"
);
credentials.SchedulerM2MClientId     = schedulerM2M.ClientId;
credentials.SchedulerM2MClientSecret = schedulerM2M.ClientSecret;

Console.WriteLine($"  ✓ API-server M2M client ID:   {credentials.ApiServerM2MClientId}");
Console.WriteLine($"  ✓ Scheduler M2M client ID:    {credentials.SchedulerM2MClientId}");
Console.WriteLine();

// ── 4. Grant M2M apps access to their target APIs ────────────────────────────

Console.WriteLine("Step 4/4 — Granting M2M access …");

await EnsureClientGrant(credentials.ApiServerM2MClientId,  credentials.SchedulerAudience);
await EnsureClientGrant(credentials.SchedulerM2MClientId,  credentials.ScriptAgentAudience);

Console.WriteLine("  ✓ API-server → Scheduler grant created.");
Console.WriteLine("  ✓ Scheduler → ScriptAgent grant created.");
Console.WriteLine();

// ── Write credentials output ─────────────────────────────────────────────────

var outputFile = $"credentials-{env}.json";
var json = JsonSerializer.Serialize(credentials, new JsonSerializerOptions { WriteIndented = true });
await File.WriteAllTextAsync(outputFile, json);

Console.WriteLine("═══════════════════════════════════════════════════════");
Console.WriteLine($"  Auth0 setup complete!  Credentials → {outputFile}");
Console.WriteLine("═══════════════════════════════════════════════════════");
Console.WriteLine();
Console.WriteLine("Next steps:");
Console.WriteLine($"  1. Open {outputFile}");
Console.WriteLine("  2. Paste the values into each server's appsettings.json:");
Console.WriteLine($"       API server:   Auth0.Audience  = {credentials.ApiAudience}");
Console.WriteLine($"                     M2MClients.Scheduler.ClientId/ClientSecret");
Console.WriteLine($"       Scheduler:    Auth0.Audience  = {credentials.SchedulerAudience}");
Console.WriteLine($"                     M2MClients.ScriptAgent.ClientId/ClientSecret");
Console.WriteLine($"       ScriptAgent:  Auth0.Audience  = {credentials.ScriptAgentAudience}");
Console.WriteLine($"       Blazor web:   Auth0.ClientId  = {credentials.SpaClientId}");
Console.WriteLine("  3. For staging/prod, update the SPA callback/logout URLs in the Auth0 dashboard.");
Console.WriteLine();

// ── Helpers ──────────────────────────────────────────────────────────────────

async Task<string> EnsureResourceServer(string name, string identifier, string description)
{
    // Check if already exists
    var existing = await http.GetFromJsonAsync<Auth0ResourceServersPage>(
        $"resource-servers?per_page=100");

    var found = existing?.ResourceServers?.Find(r => r.Identifier == identifier);
    if (found != null)
    {
        Console.WriteLine($"  (reusing existing API '{name}')");
        return found.Identifier;
    }

    var created = await http.PostAsJsonAsync("resource-servers", new
    {
        name,
        identifier,
        description,
        token_lifetime         = 86400,
        token_lifetime_for_web = 7200,
        signing_alg            = "RS256"
    });
    created.EnsureSuccessStatusCode();
    var rs = await created.Content.ReadFromJsonAsync<ResourceServer>();
    return rs!.Identifier;
}

async Task<AppCredentials> EnsureApplication(
    string   name,
    string   appType,
    string[]? callbacks = null,
    string[]? logouts   = null,
    string[]? origins   = null)
{
    // Check if already exists
    var existing = await http.GetFromJsonAsync<Auth0ClientsPage>(
        $"clients?per_page=100&fields=client_id,client_secret,name,app_type");

    var found = existing?.Clients?.Find(a => a.Name == name);
    if (found != null)
    {
        Console.WriteLine($"  (reusing existing application '{name}')");
        return new AppCredentials(found.ClientId, found.ClientSecret ?? string.Empty);
    }

    var body = new Dictionary<string, object>
    {
        ["name"]     = name,
        ["app_type"] = appType
    };
    if (callbacks != null) body["callbacks"]               = callbacks;
    if (logouts   != null) body["allowed_logout_urls"]     = logouts;
    if (origins   != null) body["web_origins"]             = origins;

    var created = await http.PostAsJsonAsync("clients", body);
    created.EnsureSuccessStatusCode();
    var app = await created.Content.ReadFromJsonAsync<Auth0App>();
    return new AppCredentials(app!.ClientId, app.ClientSecret ?? string.Empty);
}

async Task EnsureClientGrant(string clientId, string audience)
{
    var existing = await http.GetFromJsonAsync<List<ClientGrant>>(
        $"client-grants?per_page=100&client_id={clientId}");

    if (existing?.Exists(g => g.Audience == audience) == true)
    {
        Console.WriteLine($"  (grant {clientId} → {audience} already exists)");
        return;
    }

    var resp = await http.PostAsJsonAsync("client-grants", new
    {
        client_id = clientId,
        audience,
        scope     = Array.Empty<string>()
    });
    resp.EnsureSuccessStatusCode();
}

// ── Models ───────────────────────────────────────────────────────────────────

record AppCredentials(string ClientId, string ClientSecret);

class Credentials
{
    [JsonPropertyName("apiAudience")]              public string ApiAudience              { get; set; } = string.Empty;
    [JsonPropertyName("schedulerAudience")]        public string SchedulerAudience        { get; set; } = string.Empty;
    [JsonPropertyName("scriptAgentAudience")]      public string ScriptAgentAudience      { get; set; } = string.Empty;
    [JsonPropertyName("spaClientId")]              public string SpaClientId              { get; set; } = string.Empty;
    [JsonPropertyName("apiServerM2MClientId")]     public string ApiServerM2MClientId     { get; set; } = string.Empty;
    [JsonPropertyName("apiServerM2MClientSecret")] public string ApiServerM2MClientSecret { get; set; } = string.Empty;
    [JsonPropertyName("schedulerM2MClientId")]     public string SchedulerM2MClientId     { get; set; } = string.Empty;
    [JsonPropertyName("schedulerM2MClientSecret")] public string SchedulerM2MClientSecret { get; set; } = string.Empty;
}

class Auth0ClientsPage
{
    [JsonPropertyName("clients")]          public List<Auth0App>?      Clients         { get; set; }
}

class Auth0ResourceServersPage
{
    [JsonPropertyName("resource_servers")] public List<ResourceServer>? ResourceServers { get; set; }
}

class Auth0App
{
    [JsonPropertyName("client_id")]     public string  ClientId     { get; set; } = string.Empty;
    [JsonPropertyName("client_secret")] public string? ClientSecret { get; set; }
    [JsonPropertyName("name")]          public string  Name         { get; set; } = string.Empty;
    [JsonPropertyName("app_type")]      public string  AppType      { get; set; } = string.Empty;
}

class ResourceServer
{
    [JsonPropertyName("id")]         public string Id         { get; set; } = string.Empty;
    [JsonPropertyName("identifier")] public string Identifier { get; set; } = string.Empty;
    [JsonPropertyName("name")]       public string Name       { get; set; } = string.Empty;
}

class ClientGrant
{
    [JsonPropertyName("id")]        public string Id       { get; set; } = string.Empty;
    [JsonPropertyName("client_id")] public string ClientId { get; set; } = string.Empty;
    [JsonPropertyName("audience")]  public string Audience { get; set; } = string.Empty;
}
