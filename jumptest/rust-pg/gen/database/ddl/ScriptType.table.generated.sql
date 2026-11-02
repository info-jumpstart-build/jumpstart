

CREATE TABLE core.script_type (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.script_type (id, is_active);
