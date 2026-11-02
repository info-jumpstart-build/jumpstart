

CREATE TABLE core.on_failure (
		id BIGINT  not null,
		action VARCHAR(255)  not null,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.on_failure (id, is_active);
