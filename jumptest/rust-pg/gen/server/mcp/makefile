default: build

build:
	@cargo build
	@mkdir -p ../../../bin
	@cp -f target/debug/mcp ../../../bin/mcp
	@cp -f mcp.sh ../../../bin/mcp.sh
	@cp -f mcp.cmd ../../../bin/mcp.cmd
	@chmod +x ../../../bin/mcp ../../../bin/mcp.sh
	@# On Apple Silicon the kernel SIGKILLs a binary whose ad-hoc signature is
	@# stale/invalid when run directly (it still launches under a debugger). Copying
	@# the linked binary can leave that state, so re-sign the deployed copy ad-hoc.
	@if [ "$$(uname)" = "Darwin" ]; then codesign --force --sign - ../../../bin/mcp || true; fi

run:
	@cargo run

clean:
	@cargo clean
