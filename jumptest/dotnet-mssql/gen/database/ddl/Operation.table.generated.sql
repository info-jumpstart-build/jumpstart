
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.operation (
		id BIGINT  not null,
		objectname VARCHAR(50) ,
		methodname VARCHAR(50) ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_operation_id_is_active ON core.operation (id, is_active);
