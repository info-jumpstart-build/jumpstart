TARGETDIR=./bin/Debug/net9.0

default: build

generate:
	@jumpstart ./jumptest.csv database-mssql
	@jumpstart ./jumptest.csv server-dotnet
	@jumpstart ./jumptest.csv web-nodejs
	@jumpstart ./jumptest.csv tools-dotnet
	@jumpstart ./jumptest.csv test-dotnet


build: server web tools test

database:
	@chmod +x ./gen/database/ddl/build.ps1
	@chmod +x ./gen/database/data/load.ps1
	@cd ./gen/database/ddl && powershell ./build.ps1
	@cd ./gen/database/data && powershell ./load.ps1
	@if [ -d ./usr/database/data ]; then \
		chmod +x ./usr/database/data/load-usr.ps1; \
		cd ./usr/database/data &&  powershell ./load-usr.ps1; \
	fi

# Apply only the hand-written user seed (idempotent) to an existing database.
seed:
	@chmod +x ./usr/database/data/load-usr.ps1
	@cd ./usr/database/data && powershell ./load-usr.ps1

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
