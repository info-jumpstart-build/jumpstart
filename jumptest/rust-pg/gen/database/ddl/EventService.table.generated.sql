

CREATE TABLE core.event_service (
		id BIGINT  not null,
		event_type VARCHAR(255)  not null,
		objectname_filter VARCHAR(255)  not null,
		methodname_filter VARCHAR(255)  not null,
		script_id BIGINT  not null,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.event_service (id, is_active);
