---
layout: default
title: Generated Application
nav_order: 5
---

# Generated Application

The generator produces a complete, layered application in your choice of backend (.NET 9 or Rust) and frontend (Blazor WebAssembly or React). Both backends implement the same architecture, REST contract, security model, and real-time protocol; both frontends work against both backends.

## Architecture

```
+---------------------+
|  Blazor WASM or     |     Browser
|  React (Vite)       |
+--------+------------+
         | HTTP / SSE
+--------v---------+
|  REST API        |     API Server (ports 5200-5300)
|  ASP.NET Core /  |
|  rouille (Rust)  |
+--------+---------+
         |
+--------v---------+
|   Logic Layer    |     Business logic + interception (auth, logging, events)
+--------+---------+
         |
+--------v---------+
|  Persist Layer   |     DBPersist (audited row-versioning / basic)
+--------+---------+
         |
+--------v---------+
| PostgreSQL / SQL |     Database
|     Server       |
+------------------+

+------------------+     +------------------+     +------------------+
|   Scheduler      |     |  Script Agent(s) |     |   MCP Server     |
|  (5000-5100)     |<--->|   (5100-5200)    |     |  (5300-5400)     |
+------------------+     +------------------+     +------------------+
  Workflow orchestration    Script execution        MCP tool calls
                                                      (Rust only)
```

## Layers

| Layer | Location | .NET | Rust | Responsibility |
|-------|----------|------|------|---------------|
| **Database** | `gen/database/` | -- | -- | DDL, sequences, RWK indexes, views, seed data |
| **Common** | `gen/shared/common/` | class library | `common` crate | Config, logging, DB providers, utilities, Auth0 |
| **Domain** | `gen/shared/domain/` | class library | `domain` crate | Dictionary-backed domain objects with typed accessors |
| **Persistence** | `gen/shared/persist/` | class library | `persist` crate | DBPersist: SQL building, CRUD, audit strategies |
| **Logic** | `gen/shared/logic/` | class library | `logic` crate | Business rules, interception, RBAC authorization |
| **Script** | `gen/shared/script/` | (in common) | `script` crate | Database-stored script execution |
| **API** | `gen/server/api/` | ASP.NET Core | rouille binary | REST endpoints, SSE notifications, JWT auth |
| **Scheduler** | `gen/server/scheduler/` | worker service | binary (cron crate) | Workflow dispatch and cron scheduling |
| **Script agent** | `gen/server/scriptagent/` | worker service | binary | Script execution node |
| **MCP server** | `gen/server/mcp/` | -- | binary (`rmcp`) | Domain model as MCP tools (Rust only) |
| **Web** | `gen/web/` | Blazor WASM | React + Vite | Pages, components, navigation, live updates |
| **Tests** | `gen/test/` | console apps | binaries | Per-layer integration test harnesses |
| **Tools** | `gen/tools/` | console apps | binaries | CSV import/export |

## The gen / usr / bin Convention

Generated output is split into three top-level directories:

- **`gen/`** — regenerated on every generator run. Never hand-edit.
- **`usr/`** — created once (`FORCE=FALSE` templates), never overwritten. All customization lives here.
- **`bin/`** — build target: server binaries and `*.sh`/`*.cmd` launchers, deployed by each project's makefile.

## Extension Convention

Every domain object gets a generated + user file pair, compiled into one type:

| | Generated (in `gen/`) | User (in `usr/`) | Mechanism |
|---|----------------------|------------------|-----------|
| .NET | `Customer.generated.cs` | `Customer.user.cs` | `partial class` |
| Rust | `Customer.generated.rs` | `Customer.user.rs` | `include!` into one module |

The same pattern applies to logic classes (`CustomerLogic.*`), custom API routes (`user_api.rs` on Rust, partial controllers on .NET), web pages, and hand-written seed data (`usr/database/data`). You can re-run the generator at any time without losing customizations.

## Sections

- [Database](database.md) -- Table design, keys, audited row-versioning, indexes, foreign keys
- [Application Server](application-server.md) -- API, logic, persistence, interception, auth — both backends
- [Scheduling Server](scheduling-server.md) -- Workflow orchestration and dispatch
- [Agent Server](agent-server.md) -- Script execution and real-time notifications
- [MCP Server](mcp-server.md) -- Domain model as MCP tools (Rust only)
- [Web Client](web-client.md) -- Blazor and React frontends
