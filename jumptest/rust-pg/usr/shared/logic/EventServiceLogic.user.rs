// User logic for `EventService`.
//
// Created once (FORCE=FALSE) and never overwritten. Compiled into the same
// module as EventServiceLogic.generated.rs, so the generated type and its
// `use` imports are already in scope.
//
// Add hand-written operations as ordinary methods here. They can call the
// generated `pub(crate)` operations, `DBPersist`, etc. To expose a custom
// operation to the string dispatcher (and thus to authorization / events /
// scripts), implement `dispatch_user` below and route to it from the generated
// dispatch's fallback arm.

impl EventServiceLogic {
    // Example custom operation:
    //
    // pub(crate) fn dispatch_user(
    //     &self,
    //     method: &str,
    //     ctx: &mut LogicContext,
    // ) -> Result<Value, LogicError> {
    //     match method {
    //         "recalculate" => { let _id = ctx.id(); /* ... */ Ok(Value::Null) }
    //         _ => Err(LogicError::Event(format!("unknown method '{}'", method))),
    //     }
    // }
}
