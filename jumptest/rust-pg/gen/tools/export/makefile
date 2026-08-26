default: build

build:
	@cargo build
	@mkdir -p ../../../bin
	@cp -f target/debug/export ../../../bin/export
	@chmod +x ../../../bin/export
	@# On Apple Silicon the kernel SIGKILLs a binary whose ad-hoc signature is
	@# stale/invalid when run directly. Copying the linked binary can leave that
	@# state, so re-sign the deployed copy ad-hoc.
	@if [ "$$(uname)" = "Darwin" ]; then codesign --force --sign - ../../../bin/export || true; fi

run:
	@cargo run

clean:
	@cargo clean
