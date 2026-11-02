

CREATE TABLE core.principal (
		id BIGINT  not null,
		first_name VARCHAR(50) ,
		last_name VARCHAR(50) ,
		username VARCHAR(50)  not null,
		email VARCHAR(100)  not null,
		principal_status_id INTEGER  not null,
		created_date TIMESTAMP ,
		last_login_date TIMESTAMP ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.principal (id, is_active);
