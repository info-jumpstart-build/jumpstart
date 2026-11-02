// User logic for `TestRun`.
//
// Created once (FORCE=FALSE) and never overwritten. Compiled into the same
// module as TestRunLogic.generated.rs, so the generated type and its
// `use` imports are already in scope.
//
// Add hand-written operations as ordinary methods here. They can call the
// generated `pub(crate)` operations, `DBPersist`, etc. To expose a custom
// operation to the string dispatcher (and thus to authorization / events /
// scripts), implement `dispatch_user` below and route to it from the generated
// dispatch's fallback arm.

impl TestRunLogic {
    // Add hand-written operations here. (The Generate behavior now lives on
    // TestPlan: TestPlanLogic::generate creates a TestRun and its results.)
}
