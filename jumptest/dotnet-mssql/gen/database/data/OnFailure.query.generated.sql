-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for OnFailure (core.on_failure)
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
	'core.on_failure-select',
	'SELECT
		core.on_failure.id,
            on_failure.action,
            on_failure.is_active,
            on_failure.created_by,
            on_failure.last_updated,
            on_failure.last_updated_by,
            on_failure.txn_id
	FROM core.on_failure
	WHERE core.on_failure.is_active = 1
	ORDER BY core.on_failure.id;',
	'Select all OnFailure records with related  information',
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
	'core.on_failure-get',
	'SELECT
		core.on_failure.id,
            on_failure.action,
            on_failure.is_active,
            on_failure.created_by,
            on_failure.last_updated,
            on_failure.last_updated_by,
            on_failure.txn_id
	FROM core.on_failure
	WHERE core.on_failure.id = ^(id) AND core.on_failure.is_active = 1;',
	'Select single OnFailure record by ID with related  information',
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
	'core.on_failure-get-by-rwk',
	'SELECT
		core.on_failure.id,
            on_failure.action,
            on_failure.is_active,
            on_failure.created_by,
            on_failure.last_updated,
            on_failure.last_updated_by,
            on_failure.txn_id
	FROM core.on_failure
	WHERE on_failure.action = ^(action) AND core.on_failure.is_active = 1;',
	'Select single OnFailure record by RWK columns with related  information',
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
	'core.on_failure-get-history',
	'SELECT
		core.on_failure.*
	FROM core.on_failure
	WHERE core.on_failure.id = ^(id)
	ORDER BY core.on_failure.txn_id DESC;',
	'Select all history records for OnFailure by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	