-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronHour (core.cron_hour)
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
	'core.cron_hour-select',
	'SELECT
		core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
	FROM core.cron_hour
	WHERE core.cron_hour.is_active = 1
	ORDER BY core.cron_hour.id;',
	'Select all CronHour records with related  information',
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
	'core.cron_hour-get',
	'SELECT
		core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
	FROM core.cron_hour
	WHERE core.cron_hour.id = ^(id) AND core.cron_hour.is_active = 1;',
	'Select single CronHour record by ID with related  information',
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
	'core.cron_hour-get-by-rwk',
	'SELECT
		core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
	FROM core.cron_hour
	WHERE cron_hour.name = ^(name) AND core.cron_hour.is_active = 1;',
	'Select single CronHour record by RWK columns with related  information',
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
	'core.cron_hour-get-history',
	'SELECT
		core.cron_hour.*
	FROM core.cron_hour
	WHERE core.cron_hour.id = ^(id)
	ORDER BY core.cron_hour.txn_id DESC;',
	'Select all history records for CronHour by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	