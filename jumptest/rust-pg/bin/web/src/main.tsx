// The Node/React analogue of Blazor's Program.cs.cshtml: wires up Auth0 OIDC
// authentication and the "RemoteAPI" client, then mounts the root component.
import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import { loadConfig } from "./api/config";
import { ApiClientProvider } from "./api/apiClient";


async function bootstrap() {
  const config = await loadConfig();

  const rootElement = document.getElementById("root");
  if (!rootElement) {
    throw new Error("Missing #root element in index.html");
  }

  // --noauth: authentication code generation suppressed -- no Auth0Provider.
  ReactDOM.createRoot(rootElement).render(
    <React.StrictMode>
      <BrowserRouter>
        <ApiClientProvider apiBaseUrl={config.apiBaseUrl}>
          <App />
        </ApiClientProvider>
      </BrowserRouter>
    </React.StrictMode>,
  );
}

bootstrap();
