// <user-extension>
// Custom (non-CRUD) MCP tools for this app. Created once (FORCE=FALSE) and
// never overwritten by the generator -- hand-edit it freely.
//
// This file is `include!`d directly into gen/server/mcp/main.generated.rs (no
// wrapping `mod`), so it must define exactly one more `#[tool_router]` impl
// block for the same `McpServer` type the generated file defines -- the
// inherent-impl-in-another-file convention used throughout this stack (see
// e.g. logic/op_role_member_logic.core.rs's doc comment). `McpServer::new()`
// merges this router (`user_tool_router()`) into the live one alongside the
// generated CRUD tools, so anything you add below shows up in `tools/list`
// without touching generated code. Every dependency the `mcp` crate has is
// available here: `common`, `domain`, `logic`, `rmcp`, `schemars`, `serde`,
// `serde_json` (use fully-qualified paths, e.g. `logic::SomeLogic`).
//
// Each tool call already runs with the resolved principal set (see
// `resolve_principal`/`call_tool` in main.generated.rs) by the time your
// method body executes, so an authorized logic-layer call here is
// authorized/audited exactly like a generated CRUD tool's.
//
// Example (paralleling the TestPlanLogic::generate example in
// usr/server/api/user_api.rs):
//
//   #[derive(Debug, serde::Serialize, serde::Deserialize, schemars::JsonSchema)]
//   struct GenerateArgs {
//       /// TestPlan id to generate a run for.
//       plan_id: i64,
//   }
//
//   #[tool_router(router = user_tool_router)]
//   impl McpServer {
//       #[tool(name = "generate_testplan_run", description = "Create a new test_run for a plan and one Unexecuted test_result per case.")]
//       async fn generate_testplan_run(
//           &self,
//           params: Parameters<GenerateArgs>,
//       ) -> Result<Json<serde_json::Value>, McpError> {
//           logic::TestPlanLogic::generate(params.0.plan_id)
//               .map(|run_id| Json(serde_json::json!({ "test_run_id": run_id })))
//               .map_err(|e| McpError::internal_error(e.to_string(), None))
//       }
//   }
#[tool_router(router = user_tool_router, allow_empty)]
impl McpServer {}
