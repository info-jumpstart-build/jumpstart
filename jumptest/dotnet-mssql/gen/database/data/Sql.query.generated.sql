-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Sql (core.sql)
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
	'core.sql-select',
	'SELECT
		core.sql.id,
            sql.name,
            sql.data_source_id,
            sql.sql_text,
            sql.description,
            sql.is_active,
            sql.created_by,
            sql.last_updated,
            sql.last_updated_by,
            sql.txn_id,
            data_source.name AS data_source_name
	FROM core.sql
        LEFT JOIN core.data_source data_source ON sql.data_source_id = data_source.id AND data_source.is_active = 1
	WHERE core.sql.is_active = 1
	ORDER BY core.sql.id;',
	'Select all Sql records with related DataSource information',
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
	'core.sql-get',
	'SELECT
		core.sql.id,
            sql.name,
            sql.data_source_id,
            sql.sql_text,
            sql.description,
            sql.is_active,
            sql.created_by,
            sql.last_updated,
            sql.last_updated_by,
            sql.txn_id,
            data_source.name AS data_source_name
	FROM core.sql
        LEFT JOIN core.data_source data_source ON sql.data_source_id = data_source.id AND data_source.is_active = 1
	WHERE core.sql.id = ^(id) AND core.sql.is_active = 1;',
	'Select single Sql record by ID with related DataSource information',
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
	'core.sql-get-by-rwk',
	'SELECT
		core.sql.id,
            sql.name,
            sql.data_source_id,
            sql.sql_text,
            sql.description,
            sql.is_active,
            sql.created_by,
            sql.last_updated,
            sql.last_updated_by,
            sql.txn_id,
            data_source.name AS data_source_name
	FROM core.sql
        LEFT JOIN core.data_source data_source ON sql.data_source_id = data_source.id AND data_source.is_active = 1
	WHERE sql.name = ^(name) AND core.sql.is_active = 1;',
	'Select single Sql record by RWK columns with related DataSource information',
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
	'core.sql-get-history',
	'SELECT
		core.sql.*
	FROM core.sql
	WHERE core.sql.id = ^(id)
	ORDER BY core.sql.txn_id DESC;',
	'Select all history records for Sql by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	