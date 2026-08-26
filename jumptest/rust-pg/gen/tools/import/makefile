default: build

build:
	@cargo build
	@mkdir -p ../../../bin
	@cp -f target/debug/import ../../../bin/import
	@chmod +x ../../../bin/import
	@# On Apple Silicon the kernel SIGKILLs a binary whose ad-hoc signature is
	@# stale/invalid when run directly. Copying the linked binary can leave that
	@# state, so re-sign the deployed copy ad-hoc.
	@if [ "$$(uname)" = "Darwin" ]; then codesign --force --sign - ../../../bin/import || true; fi

run:
	@cargo run

clean:
	@cargo clean
