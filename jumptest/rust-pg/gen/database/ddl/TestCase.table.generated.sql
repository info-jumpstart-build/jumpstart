

CREATE TABLE app.test_case (
		id BIGINT  not null,
		test_plan_id BIGINT  not null,
		code VARCHAR(50)  not null,
		area VARCHAR(100)  not null,
		title VARCHAR(255)  not null,
		preconditions TEXT ,
		steps TEXT  not null,
		expected_result TEXT  not null,
		priority VARCHAR(10)  not null,
		component VARCHAR(100) ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON app.test_case (id, is_active);
