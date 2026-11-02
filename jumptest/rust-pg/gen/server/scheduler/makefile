default: build

build:
	@cargo build
	@mkdir -p ../../../bin
	@cp -f target/debug/scheduler ../../../bin/scheduler
	@cp -f scheduler.sh ../../../bin/scheduler.sh
	@cp -f scheduler.cmd ../../../bin/scheduler.cmd
	@chmod +x ../../../bin/scheduler ../../../bin/scheduler.sh
	@# On Apple Silicon the kernel SIGKILLs a binary whose ad-hoc signature is
	@# stale/invalid when run directly (it still launches under a debugger). Copying
	@# the linked binary can leave that state, so re-sign the deployed copy ad-hoc.
	@if [ "$$(uname)" = "Darwin" ]; then codesign --force --sign - ../../../bin/scheduler || true; fi

run:
	@cargo run

clean:
	@cargo clean
