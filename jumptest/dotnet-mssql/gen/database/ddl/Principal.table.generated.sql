
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.principal (
		id BIGINT  not null,
		first_name VARCHAR(50) ,
		last_name VARCHAR(50) ,
		username VARCHAR(50)  not null,
		email VARCHAR(100)  not null,
<<<<<<< HEAD
		enabled INT  not null,
=======
		principal_status_id INT  not null,
>>>>>>> main
		created_date DATETIME2 ,
		last_login_date DATETIME2 ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_principal_id_is_active ON core.principal (id, is_active);
