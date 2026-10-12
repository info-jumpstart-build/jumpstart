TARGETDIR=../../../bin/api

default: build

build:
	@dotnet build
	@mkdir -p $(TARGETDIR)
	@cp -r -f ../../../usr/server/api/appsettings.json $(TARGETDIR)
	@cp -f api.sh ../../../bin/api.sh
	@cp -f api.cmd ../../../bin/api.cmd

clean:
	@rm -rf ./bin
