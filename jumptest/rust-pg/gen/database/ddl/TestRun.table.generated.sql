

CREATE TABLE app.test_run (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		test_plan_id BIGINT  not null,
		run_at TIMESTAMP ,
		run_by VARCHAR(50) ,
		notes TEXT ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON app.test_run (id, is_active);
