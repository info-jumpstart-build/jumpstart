

CREATE TABLE core.operation (
		id BIGINT  not null,
		objectname VARCHAR(50) ,
		methodname VARCHAR(50) ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.operation (id, is_active);
