
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table app.test_plan (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		description TEXT ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_app_test_plan_id_is_active ON app.test_plan (id, is_active);
