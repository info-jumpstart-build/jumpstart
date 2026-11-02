

CREATE TABLE app.test_result (
		id BIGINT  not null,
		test_run_id BIGINT  not null,
		test_case_id BIGINT  not null,
		test_result_status_id BIGINT  not null,
		executed_at TIMESTAMP ,
		executed_by_id BIGINT ,
		actual_result TEXT ,
		notes TEXT ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON app.test_result (id, is_active);
