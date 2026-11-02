TARGETDIR=./bin/Debug/net9.0

default: generate

generate:
	@jumpstart ./jumptest.csv database-mssql
	@jumpstart ./jumptest.csv server-dotnet
	@jumpstart ./jumptest.csv web-blazor
	@jumpstart ./jumptest.csv tools-dotnet
	@jumpstart ./jumptest.csv test-dotnet

build: server web tools test

database:
	@cd ./gen/database/ddl && ./build.cmd
	@cd ./gen/database/data && ./load.cmd
	@if [ -d ./usr/database/data ]; then \
		cd ./usr/database/data && ./load-usr.cmd; \
	fi

# Apply only the hand-written user seed (idempotent) to an existing database.
seed:
	@cd ./usr/database/data && ./load-usr.cmd

server:
	@$(MAKE) -C ./gen/server/api
	@$(MAKE) -C ./gen/server/scheduler
	@$(MAKE) -C ./gen/server/scriptagent

web:
	@$(MAKE) -C ./gen/web

test:
	@$(MAKE) -C ./gen/test/test-persist
	
tools:
	@$(MAKE) -C ./gen/tools/export
	@$(MAKE) -C ./gen/tools/import
	
cleangen:
	@rm -rf ./gen

cleanusr:
	@rm -rf ./usr

cleanbin:
	@rm -rf ./bin

cleanweb:
	@rm -rf ./gen/web
	@rm -rf ./usr/web
	@rm -rf ./bin/web*

cleanall:
	@rm -rf ./bin
	@rm -rf ./usr
	@rm -rf ./gen
