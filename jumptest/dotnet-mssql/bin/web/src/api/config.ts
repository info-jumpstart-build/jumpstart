// Runtime configuration, loaded once from /config.json at startup -- the
// Node/React analogue of Blazor's wwwroot/appsettings.json read through
// WebAssemblyHostBuilder.Configuration. Keeping this in public/config.json
// (rather than baked-in Vite env vars) lets the same build be redeployed
// against different API/Auth0 endpoints without a rebuild.
export interface Auth0Settings {
  domain: string;
  clientId: string;
  audience: string;
}

export interface AppConfig {
  apiBaseUrl: string;
  auth0: Auth0Settings;
}

let configPromise: Promise<AppConfig> | null = null;

export function loadConfig(): Promise<AppConfig> {
  if (!configPromise) {
    configPromise = fetch("/config.json").then((response) => {
      if (!response.ok) {
        throw new Error(`Failed to load /config.json: ${response.status} ${response.statusText}`);
      }
      return response.json() as Promise<AppConfig>;
    });
  }
  return configPromise;
}
