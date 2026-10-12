TARGETDIR=../../../bin/scriptagent

default: build

build:
	@dotnet build
	@mkdir -p $(TARGETDIR)
	@cp -r -f ../../../usr/server/scriptagent/appsettings.json $(TARGETDIR)
	@cp -f scriptagent.sh ../../../bin/scriptagent.sh
	@cp -f scriptagent.cmd ../../../bin/scriptagent.cmd

clean:
	@rm -rf ./bin
