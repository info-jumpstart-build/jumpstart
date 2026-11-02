

CREATE TABLE core.op_role_map (
		id BIGINT  not null,
		op_id BIGINT  not null,
		op_role_id BIGINT  not null,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.op_role_map (id, is_active);
