
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.script (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		source VARCHAR(4096)  not null,
		script_type_id BIGINT  not null,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_script_id_is_active ON core.script (id, is_active);
