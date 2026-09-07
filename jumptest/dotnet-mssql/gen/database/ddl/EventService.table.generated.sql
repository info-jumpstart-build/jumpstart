
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.event_service (
		id BIGINT  not null,
		event_type VARCHAR(255)  not null,
		objectname_filter VARCHAR(255)  not null,
		methodname_filter VARCHAR(255)  not null,
		script_id BIGINT  not null,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_event_service_id_is_active ON core.event_service (id, is_active);
