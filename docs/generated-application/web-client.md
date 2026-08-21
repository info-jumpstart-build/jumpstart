---
layout: default
title: Web Client
parent: Generated Application
nav_order: 5
---

# Web Client

Jumpstart generates a complete web frontend in either of two stacks — pick one per application via the template definition:

| | `web-blazor` | `web-nodejs` |
|---|-------------|--------------|
| Stack | Blazor WebAssembly (.NET 9) | React 18 + TypeScript, Vite |
| Routing | Blazor router | react-router |
| Auth | OIDC + PKCE (Blazor WASM auth library) | `@auth0/auth0-react` |
| Real-time | `EventSource` (SSE) | `EventSource` (SSE) |
| Build | `dotnet build` | `tsc && vite build` |

Both are generated from the same metadata, implement the same pages and components, and speak the same REST/SSE contract — so either frontend works against either backend (.NET or Rust).

## Generated Structure

For each domain object the generator emits a **list page** and an **edit page** (`object`-type templates), plus shared infrastructure (`model`-type templates):

| Piece | Blazor | React |
|-------|--------|-------|
| List page (per object) | `List{Obj}.razor` | `List{Obj}.tsx` |
| Edit page (per object) | `Edit{Obj}.razor` | `Edit{Obj}.tsx` |
| Workflow list (live status) | `ListWorkflowCore.razor` | `ListWorkflowCore.tsx` |
| Data table + context menu | `DataTable`, `DataTableContextMenu` | same, as `.tsx` + `.css` |
| Tab control (child records) | `TabControl` | same |
| Layout & navigation | `MainLayout`, `NavMenuLayout` | `MainLayout`, `NavMenu` |
| Auth plumbing | `Authentication.razor`, `LoginDisplay`, `RedirectToLogin` | `ProtectedRoute`, `LoginDisplay`, `AuthCallback` |
| API access | `HttpClient` | `apiClient.tsx` + `config.ts` |

### Page behavior

- **List pages** display the object's RWK columns in a `DataTable`, with a context menu for open/delete (and custom actions), and navigate to the edit page on selection.
- **Edit pages** render one input per column based on its `InputType` (Text, TextArea, Number, Date, Radio), dropdowns for `enum` FKs (fed by the `/api/{entity}/enum` endpoints), tabs for `parent` child collections, and a History tab for audited objects.
- **Navigation** is data-driven: the menu tree comes from `GET /api/NavMenu/byparent`, generated from the metadata's `NAV_MENU` column. A table's `URI` override can point its menu entry at a custom page.

## Real-Time Updates

Both clients subscribe to the API's Server-Sent Events stream:

```
GET /api/Notification/stream?ticket=<ticket>  →  PropertyUpdated { DomainObjectName, InstanceId, JsonData }
```

Pages filter events by domain object name, re-fetch the changed record, and update the list in place — the workflow list shows live status transitions (Dispatched → Executing → Completed) without polling.

**Stream auth is a ticket, not the access token.** The browser's native `EventSource` API can't set an `Authorization` header, so it can't carry the normal Auth0 bearer token the way `fetch`/`HttpClient` calls do. If Auth0 is configured, connecting straight to `/api/Notification/stream` with no token gets a `401` and the client never registers — SSE broadcasts then log `to 0 client(s)` even though the page looks "connected" in your UI.

Instead, immediately before opening the stream, both clients call `POST /api/Notification/stream-ticket` (a normal endpoint, gated by the usual `Authorization` header) which mints a random, single-use ticket good for ~30 seconds, tied to the caller's identity. That ticket — not the JWT — goes in the stream URL's `?ticket=` param; the server redeems (and immediately invalidates) it in place of the Authorization header. Redeeming removes it from the server's ticket store, so a ticket that ends up in an access log or browser history is already worthless. See:
- Rust: `notification_stream_ticket` / `redeem_stream_ticket` in `main.generated.rs`
- .NET: `NotificationController.MintStreamTicket` and `SseNotificationService.RedeemStreamTicket`
- React: `openNotificationStream` in `src/api/apiClient.tsx`
- Blazor: `ConnectNotifications` in `Listtemplate.razor` / `ListWorkflowCore.razor`

## Configuration

| | Blazor | React |
|---|--------|-------|
| Runtime config file | `wwwroot/appsettings.json` | `public/config.json` (generated into `usr/web/public`, `FORCE=FALSE`) |
| Contents | API base address, Auth0 domain/clientId/audience | same |

The client expects the API at `http://localhost:5200` by default — the bottom of the API's port range. See [Operations Notes](../operations.md) if a stray process pushes the API off that port.

Auth0 SPA settings can also be supplied at generation time via the `auth0` section of `~/.jumpstart.json`; see [Auth0 Setup](../auth0-setup.md) for the full tenant walkthrough.

## Customization

Web output follows the same `gen/` vs `usr/` convention as everything else: generated pages land in `gen/web` and are overwritten on regeneration; custom pages belong in `usr/web`.

The `jumptest` application shows the full pattern for a custom action: a right-click **Generate** menu item on the test-plan list page (copied from the `ListWorkflowCore` context-menu convention), wired to a custom API route and a custom logic operation, with the table's nav entry pointed at the custom page via the metadata `URI` override. See [Testing & Tools](../testing.md).

## Build and Run

```bash
make -C gen/web     # Blazor: dotnet build · React: npm install + tsc + vite build
./bin/web.sh        # launch (serves the built app)
```

For React development, `npm run dev` in `gen/web` starts the Vite dev server with hot reload.
