
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table app.test_result (
		id BIGINT  not null,
		test_run_id BIGINT  not null,
		test_case_id BIGINT  not null,
		test_result_status_id BIGINT  not null,
		executed_at DATETIME2 ,
		executed_by_id BIGINT ,
		actual_result TEXT ,
		notes TEXT ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_app_test_result_id_is_active ON app.test_result (id, is_active);
