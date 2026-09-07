namespace jumptest
{
    /// <summary>
    /// Strongly-typed representation of the Auth0 section in appsettings.json.
    /// Bound via builder.Services.Configure<Auth0Config>(builder.Configuration.GetSection("Auth0")).
    /// </summary>
    public class Auth0Config
    {
        /// <summary>Auth0 tenant domain, e.g. "your-tenant.auth0.com"</summary>
        public string Domain { get; set; } = string.Empty;

        /// <summary>Auth0 API audience/identifier, e.g. "https://your-api-identifier"</summary>
        public string Audience { get; set; } = string.Empty;
    }
}
