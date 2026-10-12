-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronEvery (core.cron_every)
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
	'core.cron_every-select',
	'SELECT
		core.cron_every.id,
            cron_every.name,
            cron_every.is_active,
            cron_every.created_by,
            cron_every.last_updated,
            cron_every.last_updated_by,
            cron_every.txn_id
	FROM core.cron_every
	WHERE core.cron_every.is_active = 1
	ORDER BY core.cron_every.id;',
	'Select all CronEvery records with related  information',
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
	'core.cron_every-get',
	'SELECT
		core.cron_every.id,
            cron_every.name,
            cron_every.is_active,
            cron_every.created_by,
            cron_every.last_updated,
            cron_every.last_updated_by,
            cron_every.txn_id
	FROM core.cron_every
	WHERE core.cron_every.id = ^(id) AND core.cron_every.is_active = 1;',
	'Select single CronEvery record by ID with related  information',
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
	'core.cron_every-get-by-rwk',
	'SELECT
		core.cron_every.id,
            cron_every.name,
            cron_every.is_active,
            cron_every.created_by,
            cron_every.last_updated,
            cron_every.last_updated_by,
            cron_every.txn_id
	FROM core.cron_every
	WHERE cron_every.name = ^(name) AND core.cron_every.is_active = 1;',
	'Select single CronEvery record by RWK columns with related  information',
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
	'core.cron_every-get-history',
	'SELECT
		core.cron_every.*
	FROM core.cron_every
	WHERE core.cron_every.id = ^(id)
	ORDER BY core.cron_every.txn_id DESC;',
	'Select all history records for CronEvery by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	