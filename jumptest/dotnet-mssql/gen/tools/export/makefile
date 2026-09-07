TARGETDIR=../../../bin/export/net9.0

default: build

build:
	@dotnet build
	@mkdir -p $(TARGETDIR)
	@cp -f ../../../usr/tools/export/appsettings.json $(TARGETDIR)

clean:
	@rm -rf ./bin
