// Custom API routes for jumptest (hand-written; FORCE=FALSE).
//
// Compiled into the `api` crate via `mod user_api` in the generated server.
// `handle` is called for any route the generated router does not match; return
// Some(Response) to handle it, or None to fall through to a 404.
use rouille::{Request, Response};
use serde_json::json;

use logic::TestPlanLogic;

pub fn handle(request: &Request) -> Option<Response> {
    let method = request.method().to_ascii_uppercase();
    let url = request.url();
    let path = url.trim_matches('/');
    let segs: Vec<&str> = if path.is_empty() { Vec::new() } else { path.split('/').collect() };

    // POST /api/testplan/generate/{id}
    // Create a new test_run for the plan and one Unexecuted test_result per case.
    if method == "POST"
        && segs.len() == 4
        && segs[0].eq_ignore_ascii_case("api")
        && segs[1].eq_ignore_ascii_case("testplan")
        && segs[2].eq_ignore_ascii_case("generate")
    {
        if let Ok(plan_id) = segs[3].parse::<i64>() {
            return Some(match TestPlanLogic::generate(plan_id) {
                Ok(run_id) => {
                    Response::json(&json!({ "test_plan_id": plan_id, "test_run_id": run_id }))
                }
                Err(e) => Response::text(format!("generate failed: {}", e)).with_status_code(500),
            });
        }
    }

    None
}
