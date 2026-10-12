// The Node/React analogue of Blazor's Program.cs.cshtml: wires up Auth0 OIDC
// authentication and the "RemoteAPI" client, then mounts the root component.
import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, useNavigate } from "react-router-dom";
import { Auth0Provider, type AppState } from "@auth0/auth0-react";
import App from "./App";
import { loadConfig } from "./api/config";
import { ApiClientProvider } from "./api/apiClient";

// Handles the post-login redirect the same way Auth0Provider's
// onRedirectCallback prop is meant to: route back to wherever the user was
// before ProtectedRoute sent them to log in (default: home).
function Auth0ProviderWithNavigate({ children, ...props }: React.ComponentProps<typeof Auth0Provider>) {
  const navigate = useNavigate();

  function onRedirectCallback(appState?: AppState) {
    navigate(appState?.returnTo ?? "/");
  }

  return (
    <Auth0Provider {...props} onRedirectCallback={onRedirectCallback}>
      {children}
    </Auth0Provider>
  );
}

async function bootstrap() {
  const config = await loadConfig();

  const rootElement = document.getElementById("root");
  if (!rootElement) {
    throw new Error("Missing #root element in index.html");
  }

  ReactDOM.createRoot(rootElement).render(
    <React.StrictMode>
      <BrowserRouter>
        <Auth0ProviderWithNavigate
          domain={config.auth0.domain}
          clientId={config.auth0.clientId}
          authorizationParams={{
            redirect_uri: `${window.location.origin}/authentication/callback`,
            audience: config.auth0.audience,
          }}
        >
          <ApiClientProvider apiBaseUrl={config.apiBaseUrl}>
            <App />
          </ApiClientProvider>
        </Auth0ProviderWithNavigate>
      </BrowserRouter>
    </React.StrictMode>,
  );
}

bootstrap();
