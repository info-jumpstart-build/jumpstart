
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.exec_log (
		id BIGINT  not null,
		token VARCHAR(255)  not null,
		workflow_id BIGINT ,
		start_time DATETIME2  not null,
		end_time DATETIME2 ,
		exec_status_id BIGINT ,
		stdout VARCHAR(4096) ,
		stderr VARCHAR(4096) ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_exec_log_id_is_active ON core.exec_log (id, is_active);
