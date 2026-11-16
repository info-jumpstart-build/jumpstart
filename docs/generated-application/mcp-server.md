---
layout: default
title: MCP Server
parent: Generated Application
nav_order: 7
---

# MCP Server

Rust-only (no .NET counterpart). `gen/server/mcp` exposes the domain model as [MCP](https://modelcontextprotocol.io) tools instead of REST routes — one `list_<obj>`/`get_<obj>`/`create_<obj>`/`update_<obj>`/`delete_<obj>` set per domain object, each dispatching through the same authorized `object_exec` entry point the [Application Server](application-server.md)'s REST API uses, so RBAC and audit behave identically regardless of which server a caller used.

It runs over stdio (an MCP client launches the binary directly; the whole session runs as one configured trusted local principal) or streamable-HTTP/SSE on ports `5300`–`5400` (each call authenticated the same way the REST API authenticates a request — an Auth0 bearer token on this service's own `"mcp"` audience). Custom, non-CRUD tools are added by hand in `usr/server/mcp/user_mcp_tools.rs`, the same generated+user split used everywhere else in this stack.

Full detail: [Rust Backend → MCP Server](../rust/mcp-server.md).
