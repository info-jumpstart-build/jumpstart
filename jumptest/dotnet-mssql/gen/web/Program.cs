using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

using jumptest;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ── Auth0 OIDC authentication ─────────────────────────────────────────────────
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    // Request the Auth0 audience so the returned access token is a JWT
    // that the API server can validate.
    options.ProviderOptions.AdditionalProviderParameters.Add(
        "audience", builder.Configuration["Auth0:Audience"] ?? string.Empty);
});

// Read API base URL from wwwroot/appsettings.json (editable per deployment)
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5200";

// Named client for the "Remote API" — automatically attaches the OIDC Bearer token
builder.Services.AddHttpClient("RemoteAPI", client2 =>
{
    client2.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(authorizedUrls: new[] { apiBaseUrl });
    return handler;
});

await builder.Build().RunAsync();

/*
// 1. Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp", policy =>
    {
        policy.WithOrigins("http://localhost:5063")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();

// 2. Use the configured CORS policy
app.UseCors("AllowFrontendApp");

app.MapControllers();

app.Run();
*/
