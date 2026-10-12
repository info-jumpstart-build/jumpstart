
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.server_node (
		id BIGINT  not null,
		server_node_type_id BIGINT  not null,
		hostname VARCHAR(255)  not null,
		ip_address VARCHAR(255)  not null,
		port INT ,
		username VARCHAR(255) ,
		url VARCHAR(255) ,
		user_domain VARCHAR(255) ,
		os_name VARCHAR(255) ,
		os_version VARCHAR(255) ,
		architecture VARCHAR(255) ,
		registered_at DATETIME2 ,
		server_node_status_id BIGINT ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_server_node_id_is_active ON core.server_node (id, is_active);
