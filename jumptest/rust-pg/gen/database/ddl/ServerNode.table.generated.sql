

CREATE TABLE core.server_node (
		id BIGINT  not null,
		server_node_type_id BIGINT  not null,
		hostname VARCHAR(255)  not null,
		ip_address VARCHAR(255)  not null,
		port INTEGER ,
		username VARCHAR(255) ,
		url VARCHAR(255) ,
		user_domain VARCHAR(255) ,
		os_name VARCHAR(255) ,
		os_version VARCHAR(255) ,
		architecture VARCHAR(255) ,
		registered_at TIMESTAMP ,
		server_node_status_id BIGINT ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.server_node (id, is_active);
