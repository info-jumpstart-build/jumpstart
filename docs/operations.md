---
layout: default
title: Operations Notes
nav_order: 9
---

# Operations Notes

Runtime configuration, logging, ports, and hard-won troubleshooting knowledge for generated applications. Most notes apply to both backends; Rust-specific ones are marked.

## Runtime Configuration: `~/.<namespace>.json`

Every generated binary — servers, tests, tools, database scripts — reads its runtime configuration from `~/.<namespace>.json` (e.g. `~/.jumptest.json`). See [namespace.json.example](namespace.json.example).

```json
{
  "namespace": "myapp",
  "datasources": {
    "default": { "dbtype": "postgresql", "hostname": "localhost", "port": "5432",
                 "database": "myapp", "username": "myapp_user", "password": "..." }
  },
  "loglevel": "debug",
  "logwriters": "console,file",
  "auth0": { "...": "see auth0-setup.md / auth0-m2m.md" }
}
```

**Shape matters:** `loglevel` / `logwriters` / `auth0` are **siblings** of `datasources`, not nested inside it. A malformed or empty config makes config loading panic on first database use — if a server dies immediately at startup, check this file first.

## Logging

Three writers, selected by the comma-separated `logwriters` setting:

| Writer | Destination |
|--------|-------------|
| `console` | stderr |
| `file` | day-rolling `logs/<program>.log` (created relative to the working directory if missing) |
| `database` | `core.log` table |

`loglevel` is a threshold (`debug` enables everything; `error` only errors). The `JUMPSTART_LOG` environment variable overrides it at runtime. With no `logwriters` set, the default is `console,file`.

The database writer is asynchronous by design: callers enqueue a record and a dedicated background thread owns the DB work. Two reasons this is load-bearing:

1. **Startup safety.** A synchronous DB write inside `log()` can panic the main thread on a malformed config before the server even binds its port.
2. **Load.** Connections are opened per operation (no pool), so a synchronous per-line DB writer at `debug` level turns every SQL statement into connect→insert→disconnect.

## Ports and Server Discovery

Each server binds the **first free port** in its range and registers itself in `core.server_node`:

| Server | Range | `server_node_type_id` |
|--------|-------|----------------------|
| Scheduler | 5000-5100 | 2 |
| Script agent | 5100-5200 | 1 |
| API | 5200-5300 | 3 |

**The web client is pinned to `http://localhost:5200`.** If a stray process (often a previous API instance) is holding 5200, the new API lands on 5201 and the UI can't reach it. Kill the stale instance rather than reconfiguring.

A server binding a different port than expected doesn't just break the web client's REST calls -- it also splits any purely in-process state across the two servers. The clearest case is SSE: connected browser clients are registered in the *hosting process's* in-memory client list (see `sse_clients` in the Rust API's `main.generated.rs`), so if clients are connected to one process while an update is handled by another, the broadcasting process logs `SSE broadcast '...' to 0 client(s)` even though clients are, in fact, connected -- just to the other process.

`SSE broadcast '...' to 0 client(s)` has a second, unrelated cause worth ruling out first: a `401` on `GET /api/Notification/stream` in the browser console. If Auth0 is configured, the stream requires a ticket (`?ticket=`) minted via `POST /api/Notification/stream-ticket`, because the browser's `EventSource` can't send an `Authorization` header -- see [Real-Time Updates](generated-application/web-client.md#real-time-updates). A 401 there means the client never registered at all, which looks identical in the log to the port-drift case above but has nothing to do with ports.

To pin a service to a fixed port instead of scanning its range, add a `servers` section to `~/.<namespace>.json`, keyed by service name (`api`, `scheduler`, `scriptagent`):

```json
"servers": {
  "api": { "port": "5200" },
  "scheduler": { "port": "5000" },
  "scriptagent": { "port": "5100" }
}
```

If the configured port is already taken (e.g. by a stale process), the server does **not** silently fall back to scanning without saying so -- it logs a warning and then scans forward from that port. The warning is your cue to go kill the stale process rather than let two instances run side by side.

Discovery queries filter on status Online (`server_node_status_id = 2`) and `is_active = 1`. A crashed server can leave a stale Online row; it is skipped once a fresh registration supersedes it, but `/api/Workflow/run/{id}` returns 503 when no live scheduler is found.

## Database Gotchas

- **Audited PK is `txn_id`, not `id`.** On audited tables `id` is intentionally non-unique (one row per version). Idempotent seed scripts must use `ON CONFLICT (txn_id)`; `ON CONFLICT (id)` fails with "no unique or exclusion constraint matching".
- **Sequences start at 1000.** Static/seed data must use explicit ids below 1000 to avoid collisions with runtime inserts.
- **Reference/enum data for your app isn't auto-seeded.** The generated loader only loads generated files. Hand-written seeds (e.g. lookup rows for your own enum tables) belong in `usr/database/data` with a `load-usr` script, wired into your makefile (`make database` / `make seed` in jumptest).
- **The monolithic create script owns the core schema.** `build.py` runs `<namespace>.database.create.generated.sql`, which is where `core.log` and the other core tables are created — the per-table `*.table.generated.sql` files cover only your application tables.

## Running and Debugging Servers

- **Launcher terminals hide crashes.** The `bin/*.sh` / `*.cmd` launchers open a terminal window that closes on exit. If a server seems to die silently, run the binary directly (`./bin/api`) to see the panic or error.
- **Servers register on a background thread**, so a slow database never blocks serving — but it also means a server can be up before its `server_node` row appears.
- **Rust binaries are statically linked** — copying the executable alone is sufficient; no sidecar files are needed at runtime.

### Apple Silicon code signing (`Killed: 9`) — Rust

A binary that runs fine under the debugger but dies instantly with **no output** when launched directly has a stale/invalid ad-hoc code signature: the kernel SIGKILLs it *before `main`*, so there's no panic, no stdout, no log. (The debugger's entitlement masks the problem.) Root cause is usually a non-Apple linker (lld/zld/mold) configured in `~/.cargo/config.toml`.

The server makefiles re-sign the deployed copy on macOS after `cp`; the one-off fix is:

```bash
codesign --force --sign - bin/api
```

## Template Authoring Gotchas (RazorLight)

- A lone `@` is a Razor directive, so a stray `@` in generated output (e.g. `println!("{}@{}")`) gets eaten. Escape as `@@`; `&at;` also renders `@` (post-processing), along with `&lt;`/`&gt;`.
- Check rendered output for silently-consumed `@` whenever a template touches string formatting.

## Known Limitations / Roadmap

- Rust: `cancel`/`abort` on the scheduler and the agent's process-control verbs are stubs (as in .NET); the health-monitor thread is not ported.
- Rust: the pre/post **event-service** script hooks are plumbed (hooks carry the `LogicContext`) but not yet wired — `script` depends on `logic`, so the connection needs a small callback-registry indirection.
- Rust: SQL Server support exists in the provider layer, but `DbConnection::open` is Postgres-only today.
- `test-api` has no Rust port yet.
- Notification fan-out and the DB log writer are single-node designs; horizontal scale-out would move to `LISTEN/NOTIFY` or a message bus, and pooled connections.
