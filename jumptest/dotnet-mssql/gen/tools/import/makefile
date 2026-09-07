TARGETDIR=../../../bin/import/net9.0

default: build

build:
	@dotnet build
	@mkdir -p $(TARGETDIR)
	@cp -f ../../../usr/tools/import/appsettings.json $(TARGETDIR)

clean:
	@rm -rf ./bin
