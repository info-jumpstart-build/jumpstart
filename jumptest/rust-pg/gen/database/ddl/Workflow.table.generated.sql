

CREATE TABLE core.workflow (
		id BIGINT  not null,
		workflow_type_id BIGINT  not null,
		parent_id BIGINT ,
		name VARCHAR(255)  not null,
		seq INTEGER ,
		server_node_id BIGINT ,
		process_id BIGINT ,
		exec_status_id BIGINT ,
		last_start_time TIMESTAMP  not null,
		last_end_time TIMESTAMP ,
		schedule_id BIGINT ,
		on_failure_action_id BIGINT ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.workflow (id, is_active);
