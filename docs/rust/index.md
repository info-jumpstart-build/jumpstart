---
layout: default
title: Rust Backend
nav_order: 7
has_children: true
---

# Rust Backend Reference

Jumpstart's Rust target (`server-rust`, `test-rust`, `tools-rust`) generates the application stack as a workspace of **Rust crates**. It is a full peer of the .NET target — same layers, same REST contract, same security and audit model — with the patterns adapted to a language that has no reflection, no runtime code generation, and no class inheritance.

This section documents what is *specific* to the Rust backend. The shared architecture is covered in [Generated Application](../generated-application/).

The Rust templates live under `templates/shared/rust`, `templates/server/rust`, `templates/test/rust`, and `templates/tools/rust`.

## Crate layout

Generated output is a Cargo workspace of path-dependent crates, layered lowest to highest:

| Crate | Location | .NET counterpart | Responsibility |
|-------|----------|------------------|----------------|
| `common` | `gen/shared/common` | `shared/common` | `BaseObject`, `ColumnInfo`/`ClassInfo`, `Config`, `Logger`, `Util`, JSON/enum helpers, the `DatabaseProvider` trait + Postgres/SQL Server providers, Auth0 |
| `domain` | `gen/shared/domain` | `shared/domain` | One struct per table with typed accessors, plus `View`/`History` wrappers and the core enums (`Workflow`, `Execution`, `Script`, `ServerNode`) |
| `persist` | `gen/shared/persist` | `shared/persist` | `DBPersist` (`select`/`get`/`insert`/`update`/`put`/`exec_cmd`/named-query cache), audited / basic / import strategies, the `postgres`-backed `DbConnection` |
| `logic` | `gen/shared/logic` | `shared/logic` | The dispatch model and interception (`LogicExec`, `LogicDispatch`, `LogicContext`, the hook registry), authorization, notifications |
| `script` | `gen/shared/script` | script host in `common` | `ScriptHost` running database-stored scripts on the **Rhai** engine |
| `api`, `scheduler`, `scriptagent` | `gen/server/*` | `server/*` | The three servers (rouille REST / cron dispatch / script execution) |
| `mcp` | `gen/server/mcp` | -- (Rust only) | MCP server: domain model as tools (`rmcp`), stdio + streamable-HTTP/SSE |
| `test-*` | `gen/test/*` | `test/*` | Test harnesses |
| `import`, `export` | `gen/tools/*` | `tools/*` | CSV data tools |

The shared crates are path dependencies of the servers and tests, so building any binary compiles the whole chain. Release binaries are statically linked and self-contained.

## The dictionary-backed object model

The single most important design choice shared with .NET: every domain object is **dictionary-backed**. `BaseObject` wraps a `HashMap<String, serde_json::Value>` (the live column values) plus metadata (table name, schema, RWK columns, audit flag):

```rust
pub struct BaseObject {
    pub data: HashMap<String, Value>,
    pub defaults: HashMap<String, Value>,
    pub domain_name: String,
    pub table_name: String,
    // ...
    pub rwk: Vec<String>,
}
```

Generated domain types are thin typed views over that map. For a `customer` table the generator emits a `Customer` struct that holds a `BaseObject` and exposes typed getters/setters that read and write the underlying dictionary:

```rust
pub struct Customer {
    pub base: BaseObject,
}

impl Customer {
    pub fn name(&self) -> String {
        self.base.get("name").as_str().unwrap_or_default().to_string()
    }
    pub fn set_name(&mut self, value: String) {
        self.base.set("name", serde_json::json!(value));
    }
    // ... one pair per column ...
}
```

Because the data lives in a `HashMap<String, Value>`, objects serialize to/from JSON for free, flow across the API boundary unchanged, and can be handed to scripts as object maps. Rust has no runtime reflection, so the .NET `ColumnInfoAttribute` reflection is replaced by generated metadata: each type provides `columns()` returning `Vec<ColumnInfo>`, which the persistence layer uses to build SQL and the API uses to type JSON output.

## Type mapping

Each column's PostgreSQL `DATA_TYPE` maps to a Rust type for the typed accessors:

| PostgreSQL | .NET | Rust |
|-----------|------|------|
| `bigint` | `long` | `i64` |
| `integer` / `serial` | `int` | `i32` |
| `smallint` | `short` | `i16` |
| `boolean` | `bool` | `bool` |
| `real` | `float` | `f32` |
| `double precision` | `double` | `f64` |
| `numeric` | `decimal` | `rust_decimal::Decimal` |
| `date` / `timestamp` | `DateTime` | `chrono::NaiveDateTime` |
| `uuid` | `Guid` | `uuid::Uuid` |
| `varchar` / `text` | `string` | `String` |
| `bytea` | `byte[]` | `Vec<u8>` |

Temporal, decimal, and uuid values are stored in the backing map **as strings** (ISO-8601 / decimal / canonical uuid), so they round-trip cleanly through JSON and the typed accessors parse them on read. On the wire, `numeric` columns are coerced back to JSON numbers via `ColumnInfo`, matching the .NET serializer.

## Generated + user modules

The .NET "generated + user partial class" convention becomes a pair of files spliced into one Rust module. The crate root `include!`s both a regenerated `<Obj>.generated.rs` (`gen/`, `FORCE=TRUE`) and a hand-editable `<Obj>.user.rs` (`usr/`, `FORCE=FALSE`) into the same module, so the user file's `impl` block extends the generated type:

```rust
pub mod customer {
    include!("Customer.generated.rs");                       // gen/, regenerated
    include!("../../../usr/shared/domain/Customer.user.rs"); // usr/, your code
}
```

The same pattern applies in the `domain` and `logic` crates, and the API's `user_api.rs` route hook.

> **`gen/` vs `usr/`.** Files under `gen/` are regenerated on every run — never hand-edit them. Files under `usr/` are created once and never overwritten. A corollary worth remembering: anything the *generated* code calls must be defined in *generated* code, because the generator cannot retroactively update an existing `usr/` file.

## Building

Each generated crate is an ordinary Cargo crate with a `makefile` wrapping `cargo build` and deploying the binary plus `*.sh`/`*.cmd` launchers into the project `bin/`. Database connections, logging, and Auth0 settings are read at runtime from `~/.<namespace>.json` by the `common` `Config` loader — the same per-namespace config file the .NET runtime uses.

## Continue

- [Logic & the dispatch model](logic-dispatch.md) — how interception, authorization, and the hook pipeline work, and how to extend the model from `usr/` code.
- [Scripting with Rhai](scripting.md) — running database-stored scripts in process, the capabilities exposed to them, and sample calls.
- [MCP Server](mcp-server.md) — the domain model as MCP tools, transports, and authentication.
- [Operations Notes](../operations.md) — Rust-specific build/run gotchas (code signing, launchers, ports).
