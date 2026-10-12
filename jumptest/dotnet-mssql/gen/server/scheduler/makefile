TARGETDIR=../../../bin/scheduler

default: build

build:
	@dotnet build
	@mkdir -p $(TARGETDIR)
	@cp -r -f ../../../usr/server/scheduler/appsettings.json $(TARGETDIR)
	@cp -f scheduler.sh ../../../bin/scheduler.sh
	@cp -f scheduler.cmd ../../../bin/scheduler.cmd

clean:
	@rm -rf ./bin
