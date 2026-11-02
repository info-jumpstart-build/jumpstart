

CREATE TABLE core.exec_log (
		id BIGINT  not null,
		token VARCHAR(255)  not null,
		workflow_id BIGINT ,
		start_time TIMESTAMP  not null,
		end_time TIMESTAMP ,
		exec_status_id BIGINT ,
		stdout VARCHAR(4096) ,
		stderr VARCHAR(4096) ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.exec_log (id, is_active);
