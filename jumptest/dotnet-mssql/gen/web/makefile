TARGETDIR=../../bin/web

default: build

build:
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
