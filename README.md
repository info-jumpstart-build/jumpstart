# Jumpstart

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![Rust](https://img.shields.io/badge/Rust-stable-orange.svg)](https://www.rust-lang.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15+-blue.svg)](https://www.postgresql.org/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-blue.svg)](https://www.microsoft.com/en-us/sql-server/sql-server-2022)

> **Jumpstart** is a metadata-driven code generation framework that turns a CSV data model into a complete, production-ready full-stack application — database, backend servers, web frontend, test suites, and data tools.

## Overview

You describe your application as tabular metadata (one CSV row per column). Jumpstart generates everything else, in your choice of stack:

| Layer | Targets |
|-------|---------|
| **Database** | PostgreSQL, SQL Server — DDL, sequences, views, seed data |
| **Backend** | **.NET 9** (ASP.NET Core) or **Rust** (rouille) — API, logic, persistence, scheduler, script agent |
| **Frontend** | **Blazor WebAssembly** or **React + TypeScript (Vite)** |
| **Testing** | Generated integration test harnesses per layer |
| **Tools** | CSV import/export utilities |

The two backends and two frontends are peers: they implement the same REST contract, the same dictionary-backed object model, the same authorization and audit design, and the same real-time notification protocol (Server-Sent Events). Any frontend works against any backend.

## Key Features

- **Metadata-driven generation** — a single CSV defines tables, columns, labels, relationships (enum / parent / map), navigation, and audit behavior.
- **Layered architecture** — domain, persistence, logic (with AOP interception), API, and web layers with strict separation.
- **Row-versioned audit** — audited tables keep every version of every record in place (`txn_id`-keyed), giving full history without a separate audit store.
- **RBAC authorization** — every logic operation is checked against operation/role/member tables; scripts and custom methods inherit the same checks.
- **Workflow engine** — cron-driven scheduler and script agents with self-registration, live status, and real-time UI updates over SSE.
- **In-process scripting** — database-stored scripts: C#, PowerShell, Python on .NET; sandboxed Rhai on Rust.
- **Auth0 integration** — SPA login (OIDC + PKCE) and machine-to-machine JWTs between servers.
- **Safe regeneration** — generated code (`gen/`) is always overwritten; your code (`usr/`) is never touched. Re-run the generator any time.

## Prerequisites

- **.NET 9 SDK** — required to build and run the generator (and the .NET backend)
- **Rust toolchain** (stable) — for the Rust backend
- **Node.js 20+** — for the React frontend
- **PostgreSQL 15+** or **SQL Server 2019+**

## Quick Start

### 1. Build the generator

```bash
git clone https://github.com/your-org/jumpstart.git
cd jumpstart/src
dotnet build
```

### 2. Configure `~/.jumpstart.json`

```json
{
  "modelpath": "~/projects/myapp/myapp.csv",
  "templatedefs": ["database-pgsql", "server-rust", "web-nodejs", "test-rust", "tools-rust"]
}
```

The model filename becomes the application namespace (`myapp.csv` → `myapp`).

### 3. Generate

```bash
jumpstart                              # uses ~/.jumpstart.json
jumpstart myapp.csv server-rust        # or explicit: <model.csv> <template-def>
jumpstart myapp.csv web-nodejs --noauth  # skip generating the Auth0 login/JWT flow
```

### 4. Build and run

```bash
cd gen/database/ddl  && ./build.sh     # create the database
cd ../data           && ./load.sh      # load seed data
make -C gen/server/api                 # build + deploy the API server to bin/
make -C gen/web                        # build the web client
./bin/api.sh                           # run (API picks a port in 5200-5300)
```

Runtime configuration (database connections, logging, Auth0) lives in `~/.<namespace>.json` — see [docs/namespace.json.example](docs/namespace.json.example).

## Template Definitions

Each template definition is a CSV registry (`templates/<name>.csv`) mapping templates to output paths:

| Definition | Generates |
|------------|-----------|
| `database-pgsql` / `database-mssql` | DDL, sequences, RWK indexes, views, seed data, build/load scripts |
| `server-dotnet` | .NET backend: common, domain, persist, logic, API, scheduler, script agent |
| `server-rust` | Rust backend: the same layers as Cargo crates, plus the Rhai script crate |
| `web-blazor` | Blazor WebAssembly frontend |
| `web-nodejs` | React + TypeScript (Vite) frontend |
| `test-dotnet` / `test-rust` | Test harnesses: persist, script, scheduler, script agent (+ API on .NET) |
| `tools-dotnet` / `tools-rust` | CSV import/export utilities |

## Generated Project Layout

```
myapp/
├── gen/                  # regenerated every run — never edit
│   ├── database/         #   ddl/ and data/ scripts
│   ├── shared/           #   common, domain, persist, logic (+ script on Rust)
│   ├── server/           #   api, scheduler, scriptagent
│   ├── web/              #   Blazor or React app
│   ├── test/             #   test-persist, test-script, test-scheduler, ...
│   └── tools/            #   import, export
├── usr/                  # created once, never overwritten — your code
│   ├── shared/domain     #   <Obj>.user.* domain extensions
│   ├── shared/logic      #   <Obj>Logic.user.* custom operations
│   ├── server/api        #   custom API routes
│   ├── database/data     #   hand-written seed data
│   └── web/              #   custom pages
└── bin/                  # deployed binaries + launch scripts
```

The `gen/` vs `usr/` split is the core convention: templates marked `FORCE=TRUE` regenerate into `gen/`; templates marked `FORCE=FALSE` create one-time stubs in `usr/` that are yours to edit. Anything generated code calls must live in generated code — the generator never modifies an existing `usr/` file.

## Extending with Custom Methods

Custom business operations are first-class citizens:

- **.NET** — logic and controller classes are `partial`; override virtual operations or add endpoints in `usr/` partial classes.
- **Rust** — add methods in `usr/shared/logic/<Obj>Logic.user.rs`. Wrap them in `LogicExec::call_with` to get authorization and audit under their own `(domain, method)` name, expose them to string dispatch via the `dispatch_user` hook, and add REST routes in `usr/server/api/user_api.rs`.

Grant access by inserting an `operation` row and mapping it to a role — custom methods use exactly the same RBAC pipeline as built-in CRUD.

## Testing

Jumpstart generates its test tooling alongside the application:

- **test-persist** — inserts and updates every domain object twice with generated test data, then reads back and verifies.
- **test-script** — exercises the script host (Rhai / C#).
- **test-scheduler** / **test-scriptagent** — drive the workflow REST surfaces end to end.
- **test-api** (.NET) — HTTP integration tests per endpoint.
- **jumptest** — a self-testing application: a Jumpstart model of the software-testing domain (test plans, cases, runs, results) that Jumpstart generates and then uses to test itself. See [`jumptest/`](jumptest/).

```bash
make -C gen/test/test-persist    # build + run a harness
```

## Documentation

Full documentation is in [docs/](docs/index.md):

- [Getting Started](docs/getting-started.md)
- [Metadata Specification](docs/metadata.md)
- [Generator Internals](docs/generator.md)
- [Generated Application](docs/generated-application/index.md) — database, servers, web, dual-stack details
- [Rust Backend Reference](docs/rust/index.md) — crate layout, dispatch model, Rhai scripting
- [Testing & Tools](docs/testing.md)
- [Auth0 Setup](docs/auth0-setup.md) and [M2M Authentication](docs/auth0-m2m.md)
- [Operations Notes](docs/operations.md) — ports, config, troubleshooting

## License

MIT — see [LICENSE](LICENSE).
