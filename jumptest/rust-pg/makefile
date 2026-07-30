TARGETDIR=./bin/Debug/net9.0

default: generate

generate:
	@jumpstart ./jumptest.csv database-pgsql
	@jumpstart ./jumptest.csv server-rust
	@jumpstart ./jumptest.csv web-nodejs
	@jumpstart ./jumptest.csv tools-rust
	@jumpstart ./jumptest.csv test-rust

build:
	@$(MAKE) -C ./gen/server/api
	@$(MAKE) -C ./gen/server/scheduler
	@$(MAKE) -C ./gen/server/scriptagent
	@$(MAKE) -C ./gen/web
	@$(MAKE) -C ./gen/tools/export
	@$(MAKE) -C ./gen/tools/import

database:
	@chmod +x ./gen/database/ddl/build.sh
	@chmod +x ./gen/database/data/load.sh
	@cd ./gen/database/ddl && ./build.sh
	@cd ./gen/database/data && ./load.sh
	@if [ -d ./usr/database/data ]; then \
		chmod +x ./usr/database/data/load-usr.sh; \
		cd ./usr/database/data && ./load-usr.sh; \
	fi

# Apply only the hand-written user seed (idempotent) to an existing database.
seed:
	@chmod +x ./usr/database/data/load-usr.sh
	@cd ./usr/database/data && ./load-usr.sh

server:
	@$(MAKE) -C ./gen/server/api
	@$(MAKE) -C ./gen/server/scheduler
	@$(MAKE) -C ./gen/server/scriptagent

api:
	@$(MAKE) -C ./gen/server/api

scheduler:
	@$(MAKE) -C ./gen/server/scheduler

scriptagent:
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
