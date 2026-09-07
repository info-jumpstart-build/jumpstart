
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.workflow (
		id BIGINT  not null,
		workflow_type_id BIGINT  not null,
		parent_id BIGINT ,
		name VARCHAR(255)  not null,
		seq INT ,
		server_node_id BIGINT ,
		process_id BIGINT ,
		exec_status_id BIGINT ,
		last_start_time DATETIME2  not null,
		last_end_time DATETIME2 ,
		schedule_id BIGINT ,
		on_failure_action_id BIGINT ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_workflow_id_is_active ON core.workflow (id, is_active);
