TARGETDIR=../../bin/web

default: build

build:
<<<<<<< HEAD
	@dotnet restore
	@dotnet build
	@mkdir -p $(TARGETDIR)
	@cp -r -f . $(TARGETDIR)
	@# Copy hand-written web assets from usr/web into the deployed bin/web.
	@# (Custom pages under usr/web/pages are compiled into the build above;
	@#  this copies static files served at runtime, e.g. usr/web/wwwroot.)
	@cp -r -f ../../usr/web/. $(TARGETDIR)


clean:
	@rm -rf ./bin
	@rm -rf ./obj

restore:
	@dotnet restore

run:
	@dotnet run

publish:
	@dotnet publish -c Release
=======
	@npm install
	@npm run build
	@mkdir -p $(TARGETDIR)
	@# Copy the whole project -- not just dist/ -- so bin/web is a
	@# self-contained, runnable copy (package.json, src, vite.config.ts,
	@# etc). web.sh/web.cmd (copied to ./bin) cd into bin/web and run
	@# `npm run dev` directly, the same way the Blazor makefile's
	@# `cp -r -f . $(TARGETDIR)` leaves a full runnable project behind.
	@cp -r -f . $(TARGETDIR)
	@# node_modules gets copied above along with everything else, but it
	@# can't just be reused in place: npm's node_modules/.bin/* entries are
	@# relative symlinks, and native platform binaries (esbuild, rollup,
	@# etc.) are resolved for the machine/path they were installed under --
	@# neither reliably survives a raw directory copy to a new location
	@# (this is what caused the "Cannot find module .../node_modules/dist/
	@# node/cli.js" error). Wipe it and reinstall fresh in $(TARGETDIR).
	@rm -rf $(TARGETDIR)/node_modules
	@# Copy hand-written web assets from usr/web into the deployed bin/web
	@# (e.g. usr/web/public/config.json).
	@cp -r -f ../../usr/web/. $(TARGETDIR)
	@cd $(TARGETDIR) && npm install

clean:
	@rm -rf ./dist
	@rm -rf ./node_modules
	@rm -rf package-lock.json

install:
	@npm install

dev:
	@npm run dev

preview:
	@npm run preview
>>>>>>> main
