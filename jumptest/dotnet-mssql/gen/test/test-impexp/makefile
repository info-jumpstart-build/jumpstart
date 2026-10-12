TARGETDIR=./bin/Debug/net9.0

default: build

build:
	@dotnet build
	@cp -f ./appsettings.json $(TARGETDIR)

clean:
	@rm -rf ./bin
