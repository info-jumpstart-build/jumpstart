# jumptest - Node.js Web Application

This is a generated React + TypeScript single-page application, the Node.js
equivalent of the generated Blazor WebAssembly app. It follows the same
architecture: a client-side SPA that authenticates against Auth0 and talks
to the same REST API (the dotnet or rust server) over HTTP.

## Prerequisites

- **Node.js 20+** and npm
- A running API server (dotnet or rust) -- see `ApiBaseUrl` below

## Quick Start

```bash
# Install dependencies
npm install
# or: ./build.sh / build.cmd

# Run the dev server
npm run dev
# or: ./web.sh / web.cmd
```

The application will be available at `http://localhost:5063`.

## Project Structure

- **src/pages/** -- Route-level page components (List/Edit pages per domain object are added here in later generator passes)
- **src/layout/** -- `MainLayout` (app shell + top-level nav) and `NavMenu` (child sidebar), mirroring `web/blazor/Layout`
- **src/auth/** -- Auth0 login/logout/callback components, mirroring `web/blazor/Auth`
- **src/api/** -- Runtime config loader and the `useApiClient()` HTTP client (mirrors the named "RemoteAPI" `HttpClient` registered in Blazor's `Program.cs`)
- **src/types/** -- TypeScript interfaces matching the JSON shape returned by the API (snake_case, same as the C#/Rust domain objects)
- **public/** -- Static assets (favicon, Bootstrap, `config.json`)

## Configuration

Runtime configuration lives in `public/config.json` (equivalent to Blazor's
`wwwroot/appsettings.json`) so the same build can point at different API/Auth0
endpoints without a rebuild:

```json
{
  "apiBaseUrl": "http://localhost:5200",
  "auth0": {
    "domain": "...",
    "clientId": "...",
    "audience": "..."
  }
}
```

## API Contract

This app calls the exact same REST endpoints as the Blazor client, e.g.:

- `GET /api/navmenu/byparent?parent_id=0&orderby=ordinal` -- top-level nav menu
- `GET /api/navmenu/byparent?parent_id={id}&orderby=ordinal` -- child menu items
- `GET /api/navmenu/{id}` -- single nav menu record

See `docs/generated-application/application-server.md` for the full endpoint
reference (CRUD, views, enums, children, history) shared by the dotnet and
rust servers.

## Building for Production

```bash
npm run build
```

Output is written to `dist/`. `make build` additionally copies hand-written
assets from `usr/web` (e.g. a customized `config.json`) over the build output,
mirroring the Blazor `makefile`'s `usr/web` overlay.
