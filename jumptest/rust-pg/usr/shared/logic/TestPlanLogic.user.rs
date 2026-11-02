// User logic for `TestPlan`.
//
// Created once (FORCE=FALSE) and never overwritten. Compiled into the same
// module as TestPlanLogic.generated.rs, so the generated type and its
// `use` imports are already in scope.
//
// Add hand-written operations as ordinary methods here. They can call the
// generated `pub(crate)` operations, `DBPersist`, etc. To expose a custom
// operation to the string dispatcher (and thus to authorization / events /
// scripts), implement `dispatch_user` below and route to it from the generated
// dispatch's fallback arm.

impl TestPlanLogic {
    /// Create a new **TestRun** for the plan, then create one **Unexecuted**
    /// TestResult for every active test_case in the plan (linked to the new run).
    /// Returns the new test_run id.
    pub fn generate(test_plan_id: i64) -> Result<i64, LogicError> {
        // 1. Verify the plan exists.
        let plan = DBPersist::select(
            &format!(
                "SELECT id FROM app.test_plan WHERE id = {} AND is_active = 1",
                test_plan_id
            ),
            "default",
        )?;
        if plan.is_empty() {
            return Err(LogicError::Event(format!("test_plan {} not found", test_plan_id)));
        }

        // 2. Resolve the 'Unexecuted' status id.
        let status = DBPersist::select(
            "SELECT id FROM app.test_result_status WHERE name = 'Unexecuted' AND is_active = 1",
            "default",
        )?;
        let unexecuted_id = status
            .first()
            .and_then(|r| r.get("id"))
            .and_then(|v| v.as_i64())
            .ok_or_else(|| {
                LogicError::Event(
                    "test_result_status 'Unexecuted' is not seeded (see usr/database/data)".to_string(),
                )
            })?;

        // 3. Create a new test run for this plan.
        let mut run = BaseObject::new();
        run.set("test_plan_id", Value::from(test_plan_id));
        run.set("name", Value::from(format!("Run {}", Util::now_timestamp())));
        run.set("run_at", Value::from(Util::now_iso8601()));
        let mut run_ctx = LogicContext::for_record(run);
        crate::object_exec_unchecked("test_run", "insert", &mut run_ctx)?;
        let test_run_id = run_ctx.transaction.get("id").as_i64().unwrap_or(0);
        if test_run_id == 0 {
            return Err(LogicError::Event("failed to create test_run".to_string()));
        }

        // 4. One Unexecuted result per active case in the plan.
        let cases = DBPersist::select(
            &format!(
                "SELECT id FROM app.test_case WHERE test_plan_id = {} AND is_active = 1 ORDER BY id",
                test_plan_id
            ),
            "default",
        )?;
        let mut created = 0i64;
        for case in &cases {
            let case_id = case.get("id").and_then(|v| v.as_i64()).unwrap_or(0);
            if case_id == 0 {
                continue;
            }
            let mut result = BaseObject::new();
            result.set("test_run_id", Value::from(test_run_id));
            result.set("test_case_id", Value::from(case_id));
            result.set("test_result_status_id", Value::from(unexecuted_id));
            let mut ctx = LogicContext::for_record(result);
            crate::object_exec_unchecked("test_result", "insert", &mut ctx)?;
            created += 1;
        }

        Logger::info(format!(
            "TestPlanLogic::generate: plan {} -> run {} with {} result(s)",
            test_plan_id, test_run_id, created
        ));
        Ok(test_run_id)
    }
}
