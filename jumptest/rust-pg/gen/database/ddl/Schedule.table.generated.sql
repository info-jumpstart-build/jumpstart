

CREATE TABLE core.schedule (
		id BIGINT  not null,
		name VARCHAR(255)  not null,
		cron_every_id BIGINT ,
		cron_minute_id BIGINT ,
		cron_hour_id BIGINT ,
		cron_dom_id BIGINT ,
		cron_month_id BIGINT ,
		cron_dow_id BIGINT ,
		schedule_label VARCHAR(255)  not null,
		next_run_time TIMESTAMP ,
		last_run_time TIMESTAMP ,
		is_active integer  not null,
		created_by varchar(50)  not null,
		last_updated timestamp  not null,
		last_updated_by varchar(50)  not null,
		txn_id bigint PRIMARY KEY not null
);
CREATE INDEX ON core.schedule (id, is_active);
