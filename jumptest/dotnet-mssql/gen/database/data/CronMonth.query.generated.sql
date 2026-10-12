-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronMonth (core.cron_month)
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
	'core.cron_month-select',
	'SELECT
		core.cron_month.id,
            cron_month.name,
            cron_month.is_active,
            cron_month.created_by,
            cron_month.last_updated,
            cron_month.last_updated_by,
            cron_month.txn_id
	FROM core.cron_month
	WHERE core.cron_month.is_active = 1
	ORDER BY core.cron_month.id;',
	'Select all CronMonth records with related  information',
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
	'core.cron_month-get',
	'SELECT
		core.cron_month.id,
            cron_month.name,
            cron_month.is_active,
            cron_month.created_by,
            cron_month.last_updated,
            cron_month.last_updated_by,
            cron_month.txn_id
	FROM core.cron_month
	WHERE core.cron_month.id = ^(id) AND core.cron_month.is_active = 1;',
	'Select single CronMonth record by ID with related  information',
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
	'core.cron_month-get-by-rwk',
	'SELECT
		core.cron_month.id,
            cron_month.name,
            cron_month.is_active,
            cron_month.created_by,
            cron_month.last_updated,
            cron_month.last_updated_by,
            cron_month.txn_id
	FROM core.cron_month
	WHERE cron_month.name = ^(name) AND core.cron_month.is_active = 1;',
	'Select single CronMonth record by RWK columns with related  information',
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
	'core.cron_month-get-history',
	'SELECT
		core.cron_month.*
	FROM core.cron_month
	WHERE core.cron_month.id = ^(id)
	ORDER BY core.cron_month.txn_id DESC;',
	'Select all history records for CronMonth by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	