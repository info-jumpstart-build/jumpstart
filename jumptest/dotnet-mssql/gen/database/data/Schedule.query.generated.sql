-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Schedule (core.schedule)
-- =====================================

DECLARE @sql_id BIGINT;

SET @sql_id = NEXT VALUE FOR core.sql_identity;
INSERT INTO core.sql (
	id,
	txn_id,
	name,
	sql_text,
	description,
	created_by,
	last_updated,
	last_updated_by,
	is_active,
	data_source_id
) VALUES (
	@sql_id,
	@sql_id,
	'core.schedule-select',
	'SELECT
		core.schedule.id,
            schedule.name,
            schedule.cron_every_id,
            schedule.cron_minute_id,
            schedule.cron_hour_id,
            schedule.cron_dom_id,
            schedule.cron_month_id,
            schedule.cron_dow_id,
            schedule.schedule_label,
            schedule.next_run_time,
            schedule.last_run_time,
            schedule.is_active,
            schedule.created_by,
            schedule.last_updated,
            schedule.last_updated_by,
            schedule.txn_id,
            cron_every.name AS cron_every_name,
            cron_minute.name AS cron_minute_name,
            cron_hour.name AS cron_hour_name,
            cron_dom.name AS cron_dom_name,
            cron_month.name AS cron_month_name,
            cron_dow.name AS cron_dow_name
	FROM core.schedule
        LEFT JOIN core.cron_every cron_every ON schedule.cron_every_id = cron_every.id AND cron_every.is_active = 1
        LEFT JOIN core.cron_minute cron_minute ON schedule.cron_minute_id = cron_minute.id AND cron_minute.is_active = 1
        LEFT JOIN core.cron_hour cron_hour ON schedule.cron_hour_id = cron_hour.id AND cron_hour.is_active = 1
        LEFT JOIN core.cron_dom cron_dom ON schedule.cron_dom_id = cron_dom.id AND cron_dom.is_active = 1
        LEFT JOIN core.cron_month cron_month ON schedule.cron_month_id = cron_month.id AND cron_month.is_active = 1
        LEFT JOIN core.cron_dow cron_dow ON schedule.cron_dow_id = cron_dow.id AND cron_dow.is_active = 1
	WHERE core.schedule.is_active = 1
	ORDER BY core.schedule.id;',
	'Select all Schedule records with related CronEvery, CronMinute, CronHour, CronDom, CronMonth, CronDow information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

SET @sql_id = NEXT VALUE FOR core.sql_identity;
INSERT INTO core.sql (
	id,
	txn_id,
	name,
	sql_text,
	description,
	created_by,
	last_updated,
	last_updated_by,
	is_active,
	data_source_id
) VALUES (
	@sql_id,
	@sql_id,
	'core.schedule-get',
	'SELECT
		core.schedule.id,
            schedule.name,
            schedule.cron_every_id,
            schedule.cron_minute_id,
            schedule.cron_hour_id,
            schedule.cron_dom_id,
            schedule.cron_month_id,
            schedule.cron_dow_id,
            schedule.schedule_label,
            schedule.next_run_time,
            schedule.last_run_time,
            schedule.is_active,
            schedule.created_by,
            schedule.last_updated,
            schedule.last_updated_by,
            schedule.txn_id,
            cron_every.name AS cron_every_name,
            cron_minute.name AS cron_minute_name,
            cron_hour.name AS cron_hour_name,
            cron_dom.name AS cron_dom_name,
            cron_month.name AS cron_month_name,
            cron_dow.name AS cron_dow_name
	FROM core.schedule
        LEFT JOIN core.cron_every cron_every ON schedule.cron_every_id = cron_every.id AND cron_every.is_active = 1
        LEFT JOIN core.cron_minute cron_minute ON schedule.cron_minute_id = cron_minute.id AND cron_minute.is_active = 1
        LEFT JOIN core.cron_hour cron_hour ON schedule.cron_hour_id = cron_hour.id AND cron_hour.is_active = 1
        LEFT JOIN core.cron_dom cron_dom ON schedule.cron_dom_id = cron_dom.id AND cron_dom.is_active = 1
        LEFT JOIN core.cron_month cron_month ON schedule.cron_month_id = cron_month.id AND cron_month.is_active = 1
        LEFT JOIN core.cron_dow cron_dow ON schedule.cron_dow_id = cron_dow.id AND cron_dow.is_active = 1
	WHERE core.schedule.id = ^(id) AND core.schedule.is_active = 1;',
	'Select single Schedule record by ID with related CronEvery, CronMinute, CronHour, CronDom, CronMonth, CronDow information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

SET @sql_id = NEXT VALUE FOR core.sql_identity;
INSERT INTO core.sql (
	id,
	txn_id,
	name,
	sql_text,
	description,
	created_by,
	last_updated,
	last_updated_by,
	is_active,
	data_source_id
) VALUES (
	@sql_id,
	@sql_id,
	'core.schedule-get-by-rwk',
	'SELECT
		core.schedule.id,
            schedule.name,
            schedule.cron_every_id,
            schedule.cron_minute_id,
            schedule.cron_hour_id,
            schedule.cron_dom_id,
            schedule.cron_month_id,
            schedule.cron_dow_id,
            schedule.schedule_label,
            schedule.next_run_time,
            schedule.last_run_time,
            schedule.is_active,
            schedule.created_by,
            schedule.last_updated,
            schedule.last_updated_by,
            schedule.txn_id,
            cron_every.name AS cron_every_name,
            cron_minute.name AS cron_minute_name,
            cron_hour.name AS cron_hour_name,
            cron_dom.name AS cron_dom_name,
            cron_month.name AS cron_month_name,
            cron_dow.name AS cron_dow_name
	FROM core.schedule
        LEFT JOIN core.cron_every cron_every ON schedule.cron_every_id = cron_every.id AND cron_every.is_active = 1
        LEFT JOIN core.cron_minute cron_minute ON schedule.cron_minute_id = cron_minute.id AND cron_minute.is_active = 1
        LEFT JOIN core.cron_hour cron_hour ON schedule.cron_hour_id = cron_hour.id AND cron_hour.is_active = 1
        LEFT JOIN core.cron_dom cron_dom ON schedule.cron_dom_id = cron_dom.id AND cron_dom.is_active = 1
        LEFT JOIN core.cron_month cron_month ON schedule.cron_month_id = cron_month.id AND cron_month.is_active = 1
        LEFT JOIN core.cron_dow cron_dow ON schedule.cron_dow_id = cron_dow.id AND cron_dow.is_active = 1
	WHERE schedule.name = ^(name) AND core.schedule.is_active = 1;',
	'Select single Schedule record by RWK columns with related CronEvery, CronMinute, CronHour, CronDom, CronMonth, CronDow information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

SET @sql_id = NEXT VALUE FOR core.sql_identity;
INSERT INTO core.sql (
	id,
	txn_id,
	name,
	sql_text,
	description,
	created_by,
	last_updated,
	last_updated_by,
	is_active,
	data_source_id
) VALUES (
	@sql_id,
	@sql_id,
	'core.schedule-get-history',
	'SELECT
		core.schedule.*
	FROM core.schedule
	WHERE core.schedule.id = ^(id)
	ORDER BY core.schedule.txn_id DESC;',
	'Select all history records for Schedule by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	