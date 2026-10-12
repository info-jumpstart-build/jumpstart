TARGETDIR=./bin/Debug/net9.0

default: build

build:
	@dotnet build
	@cp -r -f ./appsettings.json $(TARGETDIR)

clean:
	@rm -rf ./bin

