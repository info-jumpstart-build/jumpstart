---
layout: default
title: MCP Server
parent: Rust Backend
nav_order: 4
---

# MCP Server

`server-rust` also generates `gen/server/mcp`, an [MCP](https://modelcontextprotocol.io) server exposing the domain model as tools, so any MCP client (Claude Desktop/Code, Claude Code's own MCP config, etc.) can list and call the same CRUD operations the REST API exposes. There is no .NET counterpart yet — this is Rust-only.

## Tool surface

One tool per domain object per CRUD verb, generated from `Model.Objects` exactly the way `object_exec.generated.rs` generates one dispatch arm per object:

| Tool | Logic-layer call | Args |
|------|-------------------|------|
| `list_<obj>` | `object_exec(obj, "select", ..)` | none |
| `get_<obj>` | `object_exec(obj, "get", ..)` | `{ id }` |
| `create_<obj>` | `object_exec(obj, "put", ..)` | `{ <user columns, minus id> }` |
| `update_<obj>` | `object_exec(obj, "update", ..)` | `{ id, <user columns, minus id> }` |
| `delete_<obj>` | `object_exec(obj, "delete", ..)` | `{ id }` |

Every tool call goes through the exact same authorized entry point the REST API uses (`object_exec` → `<Obj>Logic::exec` → `LogicExec::call`), so RBAC and audit behave identically regardless of which server a caller used.

Tool argument structs and their JSON schemas are generated Rust types (`rmcp`'s `#[tool_router]`/`#[tool]` macros + `schemars` derive the schema from the struct — Rust has no reflection, so, same as everywhere else in this stack, the generator emits the struct explicitly rather than building a schema by hand at runtime). Plain scalar columns keep their native type; `decimal`/`DateTime`/`Guid`/`byte[]` columns arrive as plain strings and are parsed back into the real type before being handed to the domain object's typed setter.

## Transports

Chosen at startup with `--transport stdio|http` (two argv tokens) or the `JUMPSTART_MCP_TRANSPORT` env var; defaults to `stdio`.

- **stdio** — the way an MCP client normally launches this binary directly. There is no per-call identity (no bearer token travels with a stdio call), so the whole process runs as one configured **trusted local principal** for its entire session: `servers.mcp.principal` in `~/.<namespace>.json` (parallel to the existing `servers.<service>.port`). Every tool call still goes through the *checked* `object_exec` path (never `exec_unchecked`), so RBAC for that configured principal still applies — this is a fixed identity, not a bypass. If `servers.mcp.principal` is unset, stdio calls run with no principal at all, and RBAC denies them exactly as it would an anonymous REST call.
- **http** — served as a streamable-HTTP/SSE route (`axum`, nested at `/mcp`) on the first free port in `5300`–`5400` (or `servers.mcp.port`). Each call is authenticated the same way the API server authenticates REST calls: an Auth0 RS256 bearer token on this service's own `"mcp"` audience (see [Auth0 setup](../auth0-setup.md)), falling back to an `X-User` header when Auth0 is unconfigured. `rmcp` injects the inbound `http::request::Parts` into the tool call's `RequestContext::extensions`, which is how the per-call identity reaches `call_tool` without every generated tool method needing its own HTTP-aware parameter — see `resolve_principal` in `main.generated.rs`.

Only the http transport self-registers a `core.server_node` row (`ServerNodeType::McpServer`); a stdio instance is an ephemeral, client-launched process with no listening address worth advertising.

## Extending with custom tools

Custom (non-CRUD) operations are never auto-generated — add them by hand in `usr/server/mcp/user_mcp_tools.rs` (`FORCE=FALSE`, created once). It defines a second `#[tool_router(router = user_tool_router)]` impl block for the same `McpServer` type the generated file defines (the inherent-impl-in-another-file convention used throughout this stack — see [Logic & the dispatch model](logic-dispatch.md)); `McpServer::new()` merges both routers, so a hand-written tool shows up in `tools/list` without touching generated code. See the worked example (wrapping `TestPlanLogic::generate`, the same operation `usr/server/api/user_api.rs` exposes as a custom REST route) in the file's own header comment.

## Why tool bodies dispatch to a dedicated thread

`mcp` is the first **async** binary in this stack (`rmcp` + `tokio`); `api`/`scheduler`/`scriptagent` are synchronous, thread-per-request binaries. The persist layer's `postgres::Client::connect` (opened fresh per operation — see `db_connection.core.rs`) is itself synchronous and, under the hood, blocks by spinning up its own runtime — which panics ("Cannot start a runtime from within a runtime") if called from any thread the surrounding tokio runtime owns, worker or blocking-pool alike.

`common::Logger` already solved exactly this problem for its own DB writer: a dedicated plain `std::thread` reading a channel (`set_db_writer`). Every generated tool body uses the same pattern via `run_on_db_thread` in `main.generated.rs`: it ships the actual logic-layer call to one dedicated worker thread and `.await`s the result over a `tokio::sync::oneshot` channel. That thread also sets/clears `OpRoleMemberLogic`'s current-user (a thread-local — see `op_role_member_logic.core.rs`) immediately around the call, which is trivially correct since the thread processes exactly one job at a time. The resolved principal reaches each tool method via the `Extension<ResolvedPrincipal>` extractor (`call_tool` inserts it into the request's extensions before dispatching), not a thread-local, since the method body and the actual DB call no longer run on the same thread at all. Any hand-written tool in `usr/server/mcp/user_mcp_tools.rs` must route its own logic-layer calls through `run_on_db_thread` the same way — see that file's own example.

## Continue

- [Logic & the dispatch model](logic-dispatch.md)
- [Generated Application → Agent Server](../generated-application/agent-server.md) (the Script Agent — unrelated despite the name; see that page for why)
