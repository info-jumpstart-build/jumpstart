

CREATE TABLE core.script (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		source VARCHAR(4096)  not null,
		script_type_id BIGINT  not null,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.script (id, is_active);
