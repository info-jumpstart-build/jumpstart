

CREATE TABLE core.sql (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		data_source_id BIGINT  not null,
		sql_text VARCHAR(4096)  not null,
		description VARCHAR(1000) ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.sql (id, is_active);
