-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for DataSource (core.data_source)
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
	'core.data_source-select',
	'SELECT
		core.data_source.id,
            data_source.name,
            data_source.is_active,
            data_source.created_by,
            data_source.last_updated,
            data_source.last_updated_by,
            data_source.txn_id
	FROM core.data_source
	WHERE core.data_source.is_active = 1
	ORDER BY core.data_source.id;',
	'Select all DataSource records with related  information',
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
	'core.data_source-get',
	'SELECT
		core.data_source.id,
            data_source.name,
            data_source.is_active,
            data_source.created_by,
            data_source.last_updated,
            data_source.last_updated_by,
            data_source.txn_id
	FROM core.data_source
	WHERE core.data_source.id = ^(id) AND core.data_source.is_active = 1;',
	'Select single DataSource record by ID with related  information',
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
	'core.data_source-get-by-rwk',
	'SELECT
		core.data_source.id,
            data_source.name,
            data_source.is_active,
            data_source.created_by,
            data_source.last_updated,
            data_source.last_updated_by,
            data_source.txn_id
	FROM core.data_source
	WHERE data_source.name = ^(name) AND core.data_source.is_active = 1;',
	'Select single DataSource record by RWK columns with related  information',
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
	'core.data_source-get-history',
	'SELECT
		core.data_source.*
	FROM core.data_source
	WHERE core.data_source.id = ^(id)
	ORDER BY core.data_source.txn_id DESC;',
	'Select all history records for DataSource by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	