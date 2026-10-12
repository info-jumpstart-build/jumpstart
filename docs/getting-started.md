---
layout: default
title: Getting Started
nav_order: 2
---

# Getting Started

## Prerequisites

- **.NET 9 SDK** -- [Download](https://dotnet.microsoft.com/download) (required for the generator; also for the .NET backend and Blazor frontend)
- **Rust toolchain** (stable) -- [rustup.rs](https://rustup.rs) (for the Rust backend)
- **Node.js 20+** -- [nodejs.org](https://nodejs.org) (for the React frontend)
- **PostgreSQL 15+** or **SQL Server 2019+**
- **Git**

## Installation

### Clone and Build

```bash
git clone https://github.com/your-org/jumpstart.git
cd jumpstart/src
dotnet build
```

The build produces the `jumpstart` executable along with the `templates/` directory.

## Configuration

### ~/.jumpstart.json (generator config)

Jumpstart reads its configuration from `~/.jumpstart.json`:

```json
{
  "modelpath": "~/projects/myapp/myapp.csv",
  "templatedefs": ["database-pgsql", "server-rust", "web-nodejs", "test-rust", "tools-rust", "root"],
  "auth0": {
    "domain": "your-tenant.us.auth0.com",
    "clientId": "SPA_CLIENT_ID",
    "audience": "https://myapp"
  }
}
```

| Field | Description |
|-------|-------------|
| `modelpath` | Path to your metadata CSV (supports `~` expansion). The filename becomes the application namespace. |
| `templatedefs` | Template definition names to generate |
| `auth0` | Optional. SPA tenant settings baked into the generated web client (see [Auth0 Setup](auth0-setup.md)) |
| `noauth` | Optional. `true` is equivalent to passing `--noauth` on the command line (see below) |

Available template definitions:

| Definition | Output |
|------------|--------|
| `database-pgsql` | PostgreSQL DDL and data scripts |
| `database-mssql` | SQL Server DDL and data scripts |
| `server-dotnet` | .NET backend (common, domain, persist, logic, API, scheduler, script agent) |
| `server-rust` | Rust backend (the same layers as Cargo crates, plus the Rhai `script` crate) |
| `web-blazor` | Blazor WebAssembly frontend |
| `web-nodejs` | React + TypeScript (Vite) frontend |
| `test-dotnet` / `test-rust` | Test harnesses (persist, script, scheduler, script agent; API tests on .NET) |
| `tools-dotnet` / `tools-rust` | CSV import/export utilities |
| `root` | The project's top-level `makefile` (see below) |

Pick one database, one server, one web, and the matching test/tools definitions. Backends and frontends are interchangeable -- both frontends speak the same REST/SSE contract as both backends.

Include `root` in `templatedefs` to also (re)generate the top-level `makefile` that drives everything else. Its `generate:` target is built directly from whichever registries you listed -- so it only exists to reflect your `templatedefs` back at you as a runnable makefile, and it stays in sync automatically if you add or remove a registry later. It only makes sense in this `~/.jumpstart.json`-driven mode, since that's the only place the full list of registries for a project is known at once; see [`jumptest/rust-pg/makefile`](../jumptest/rust-pg/makefile) for what it looks like generated.

### ~/.&lt;namespace&gt;.json (runtime config)

Each *generated application* reads its runtime configuration from `~/.<namespace>.json` (e.g. `~/.myapp.json`): database connections, log level and writers, and Auth0 settings. See [namespace.json.example](namespace.json.example) and [Operations Notes](operations.md).

```json
{
  "namespace": "myapp",
  "datasources": {
    "default": {
      "dbtype": "postgresql",
      "hostname": "localhost",
      "port": "5432",
      "database": "myapp",
      "username": "myapp_user",
      "password": "change-me"
    }
  },
  "loglevel": "debug",
  "logwriters": "console,file"
}
```

## Creating a Metadata CSV

Your application model is defined in a CSV file. Each row describes a column in a table:

```csv
TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME,TABLE_LABEL,NAV_MENU,COLUMN_NAME,COLUMN_LABEL,FK_TYPE,FK_TABLE,TEST_DATA_SET,ORDINAL_POSITION,COLUMN_DEFAULT,RWK,IS_NULLABLE,DATA_TYPE,MSSQL_DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,URI,IS_AUDITED
myapp,app,customer,Customer,Sales,id,Customer ID,,,companies,1,NULL,0,NO,BIGINT,bigint,NULL,,1
,app,customer,,,name,Name,,,companies,2,NULL,1,NO,VARCHAR,nvarchar,255,,1
,app,customer,,,email,Email,,,emailAddresses,3,NULL,1,NO,VARCHAR,nvarchar,100,,1
,app,customer,,,created_date,Created,,,,4,(getdate()),0,YES,TIMESTAMP,datetime,NULL,,1
```

Key points:
- `TABLE_CATALOG` is set once (first row) and becomes the application namespace
- `TABLE_SCHEMA` groups tables into database schemas (e.g., `app`, `core`, `sec`)
- `TABLE_LABEL` and `NAV_MENU` are set on the first row of each table
- `RWK=1` marks "Real World Key" columns -- displayed in list views and combined into a unique index
- `IS_AUDITED=1` gives the table row-versioned history; `0` makes it a simple table
- `URI` optionally overrides the page route (used to point a table at a custom page)

See the [Metadata Specification](metadata.md) for the complete column reference.

## Running the Generator

### Using ~/.jumpstart.json (recommended)

```bash
jumpstart
```

### Using command-line arguments

```bash
jumpstart myapp.csv server-rust
```

The generator will:
1. Load your metadata CSV and merge it with `core.csv` (built-in system tables)
2. Add global audit columns from `global.csv`
3. Process relationships (parent, enum, map, views)
4. Generate output files for each template definition

### Turning off authentication (`--noauth`)

Add `--noauth` to skip generating the Auth0 login flow and JWT enforcement in the
generated app -- useful for a quick local run before an Auth0 tenant is set up:

```bash
jumpstart myapp.csv web-nodejs --noauth
jumpstart myapp.csv server-rust --noauth
```

It works with the zero-argument (`~/.jumpstart.json`) form too, either by adding the
flag (`jumpstart --noauth`) or setting `"noauth": true` in the JSON file. With
`--noauth`:
- The **web frontends** (Blazor, React) skip the Auth0 login/redirect flow entirely -- every page is reachable without signing in, and API calls carry no bearer token.
- The **API servers** (.NET, Rust) skip JWT validation on inbound requests. Authorization checks (`core.op_role_member`) still run, against whichever identity the runtime fallback provides -- the OS user for .NET, or the `X-User` request header for Rust.

`--noauth` only affects the browser-facing login/JWT gate; it does not touch the
separate machine-to-machine authentication between servers (see
[M2M Authentication](auth0-m2m.md)), which is already unauthenticated by default
until you configure it.

A typical project drives this from a makefile (see [`jumptest/rust-pg/makefile`](../jumptest/rust-pg/makefile)):

```makefile
build:
	@jumpstart ./myapp.csv database-pgsql
	@jumpstart ./myapp.csv server-rust
	@jumpstart ./myapp.csv web-nodejs
	@jumpstart ./myapp.csv tools-rust
	@jumpstart ./myapp.csv test-rust
```

## Generated Output Structure

Output is split into three top-level directories:

```
./
├── gen/                     # regenerated on every run — never hand-edit
│   ├── database/
│   │   ├── ddl/             #   CREATE TABLE, sequences, RWK indexes, views
│   │   └── data/            #   seed data, nav menus, named queries
│   ├── shared/
│   │   ├── common/          #   config, logging, DB providers, utilities
│   │   ├── domain/          #   domain model (classes / crate)
│   │   ├── persist/         #   data access layer
│   │   ├── logic/           #   business logic + interception
│   │   └── script/          #   (Rust) Rhai script engine
│   ├── server/
│   │   ├── api/             #   REST API server
│   │   ├── scheduler/       #   workflow scheduler
│   │   └── scriptagent/     #   script execution agent
│   ├── web/                 #   Blazor or React app
│   ├── test/                #   test-persist, test-script, test-scheduler, ...
│   └── tools/               #   import, export
├── usr/                     # created once, never overwritten — your code
│   ├── shared/domain/       #   <Obj>.user.* domain extensions
│   ├── shared/logic/        #   <Obj>Logic.user.* custom operations
│   ├── shared/persist/      #   appsettings stubs
│   ├── server/api/          #   custom API routes (user_api.rs)
│   ├── database/data/       #   hand-written seed data (make seed)
│   └── web/                 #   custom pages, public config
└── bin/                     # deployed binaries + *.sh / *.cmd launchers
                              #   start.sh/.cmd and stop.sh/.cmd launch and
                              #   stop api+scheduler+scriptagent+web together
```

The rule that makes regeneration safe: templates with `FORCE=TRUE` write into `gen/`; templates with `FORCE=FALSE` create one-time stubs in `usr/`. You can re-run the generator at any time without losing your work.

## Building and Running the Generated Application

### 1. Create the Database

```bash
cd gen/database/ddl
./build.sh          # PostgreSQL (build.ps1 on Windows / SQL Server)

cd ../data
./load.sh           # load generated seed data
```

If you keep hand-written seed data in `usr/database/data`, apply it with its `load-usr.sh` (the jumptest makefile wires this into `make database` / `make seed`).

### 2. Build the Servers

Each server directory has a makefile that builds the binary and deploys it (plus a launcher script) to `bin/`:

```bash
make -C gen/server/api
make -C gen/server/scheduler
make -C gen/server/scriptagent
```

### 3. Run

```bash
./bin/api.sh          # or ./bin/api to run in the current terminal
./bin/scheduler.sh
./bin/scriptagent.sh
```

Each server binds the first free port in its range and registers itself as a `ServerNode` in the database:

| Server | Port range |
|--------|-----------|
| Scheduler | 5000-5100 |
| Script agent | 5100-5200 |
| API | 5200-5300 |

The web client expects the API at `http://localhost:5200` -- see [Operations Notes](operations.md) if another process is holding that port.

### 4. Build and Run the Web Client

```bash
make -C gen/web       # Blazor: dotnet build · React: npm install + vite build
./bin/web.sh
```

### Or: Start/Stop Everything Together

Once every component has been built at least once, `bin/start.sh` (`start.cmd` on Windows) launches api, scheduler, scriptagent, and web together, each in its own terminal window; `bin/stop.sh` (`stop.cmd`) stops them all again.

## Running Tests

```bash
make -C gen/test/test-persist      # persistence round-trip test
make -C gen/test/test-script       # script host test
make -C gen/test/test-scheduler    # scheduler workflow test
make -C gen/test/test-scriptagent  # agent /execute test
```

See [Testing & Tools](testing.md) for what each harness covers, plus the import/export tools and the `jumptest` self-testing application.

## Next Steps

- [Metadata Specification](metadata.md) -- Full CSV column reference and relationship types
- [Generator](generator.md) -- How the code generator processes templates
- [Generated Application](generated-application/) -- Detailed documentation of each generated layer
- [Auth0 Setup](auth0-setup.md) -- Wire up login and server-to-server auth
