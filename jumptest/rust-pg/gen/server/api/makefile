default: build

build:
	@cargo build
	@mkdir -p ../../../bin
	@cp -f target/debug/api ../../../bin/api
	@cp -f api.sh ../../../bin/api.sh
	@cp -f api.cmd ../../../bin/api.cmd
	@chmod +x ../../../bin/api ../../../bin/api.sh
	@# On Apple Silicon the kernel SIGKILLs a binary whose ad-hoc signature is
	@# stale/invalid when run directly (it still launches under a debugger). Copying
	@# the linked binary can leave that state, so re-sign the deployed copy ad-hoc.
	@if [ "$$(uname)" = "Darwin" ]; then codesign --force --sign - ../../../bin/api || true; fi

run:
	@cargo run

clean:
	@cargo clean
