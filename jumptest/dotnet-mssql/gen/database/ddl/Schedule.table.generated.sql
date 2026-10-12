
SET NOCOUNT ON;
SET ANSI_WARNINGS OFF;
SET ANSI_PADDING OFF;
SET QUOTED_IDENTIFIER OFF;

USE [jumptest];
GO
create table core.schedule (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		cron_every_id BIGINT ,
		cron_minute_id BIGINT ,
		cron_hour_id BIGINT ,
		cron_dom_id BIGINT ,
		cron_month_id BIGINT ,
		cron_dow_id BIGINT ,
		schedule_label VARCHAR(255)  not null,
		next_run_time DATETIME2 ,
		last_run_time DATETIME2 ,
		is_active INT  not null,
		created_by VARCHAR(50)  not null,
		last_updated DATETIME2  not null,
		last_updated_by VARCHAR(50)  not null,
		txn_id BIGINT PRIMARY KEY not null
);
CREATE INDEX IX_core_schedule_id_is_active ON core.schedule (id, is_active);
