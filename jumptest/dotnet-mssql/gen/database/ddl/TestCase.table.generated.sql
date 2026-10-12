
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table app.test_case (
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
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_app_test_case_id_is_active ON app.test_case (id, is_active);
