
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.nav_menu (
		id BIGINT  not null,
		parent_id BIGINT  not null,
		ordinal INT  not null,
		name VARCHAR(1000)  not null,
		link VARCHAR(1000) ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_nav_menu_id_is_active ON core.nav_menu (id, is_active);
