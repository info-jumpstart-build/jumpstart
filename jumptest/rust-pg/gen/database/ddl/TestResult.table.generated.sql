

CREATE TABLE app.test_result (
		id BIGINT ,
		test_run_id BIGINT ,
		test_case_id BIGINT ,
		test_result_status_id BIGINT ,
		executed_at TIMESTAMP ,
		executed_by_id BIGINT ,
		actual_result TEXT ,
		notes TEXT ,
		is_active integer ,
		created_by varchar(50) ,
		last_updated timestamp ,
		last_updated_by varchar(50) ,
		txn_id bigint PRIMARY KEY
);
CREATE INDEX ON app.test_result (id, is_active);
