default: build

build:
	@cargo build
	@mkdir -p ../../../bin
	@cp -f target/debug/scriptagent ../../../bin/scriptagent
	@cp -f scriptagent.sh ../../../bin/scriptagent.sh
	@cp -f scriptagent.cmd ../../../bin/scriptagent.cmd
	@chmod +x ../../../bin/scriptagent ../../../bin/scriptagent.sh
	@# On Apple Silicon the kernel SIGKILLs a binary whose ad-hoc signature is
	@# stale/invalid when run directly (it still launches under a debugger). Copying
	@# the linked binary can leave that state, so re-sign the deployed copy ad-hoc.
	@if [ "$$(uname)" = "Darwin" ]; then codesign --force --sign - ../../../bin/scriptagent || true; fi

run:
	@cargo run

clean:
	@cargo clean
