

CREATE TABLE core.principal_org (
		id BIGINT ,
		org_id BIGINT ,
		principal_id BIGINT ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.principal_org (id, is_active);
