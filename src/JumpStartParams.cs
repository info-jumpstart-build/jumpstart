using System.Collections.Generic;

namespace jumpstart
{
    /// <summary>
    /// Auth0 tenant settings written into ~/.jumpstart.json.
    /// Maps to the "auth0" section in ~/.jumpstart.json.
    /// </summary>
    public class Auth0Config
    {
        /// <summary>Auth0 tenant domain, e.g. "your-tenant.auth0.com"</summary>
        public string domain { get; set; } = string.Empty;

        /// <summary>Auth0 SPA client ID (from the Blazor application registration)</summary>
        public string clientId { get; set; } = string.Empty;

        /// <summary>Auth0 API audience, e.g. "https://your-api-identifier"</summary>
        public string audience { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents the JSON configuration file structure for jumpstart code generator
    /// </summary>
    public class JumpStartParams
    {
        public string modelpath { get; set; } = string.Empty;
        public List<string> templatedefs { get; set; } = new List<string>();
        public Auth0Config auth0 { get; set; } = new Auth0Config();

        /// <summary>When true, suppresses authentication code generation (equivalent to the --noauth CLI flag).</summary>
        public bool noauth { get; set; } = false;
    }
}

