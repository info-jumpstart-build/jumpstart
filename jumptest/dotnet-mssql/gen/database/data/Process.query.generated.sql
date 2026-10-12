-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Process (core.process)
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
	'core.process-select',
	'SELECT
		core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
	FROM core.process
        LEFT JOIN core.script script ON process.script_id = script.id AND script.is_active = 1
	WHERE core.process.is_active = 1
	ORDER BY core.process.id;',
	'Select all Process records with related Script information',
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
	'core.process-get',
	'SELECT
		core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
	FROM core.process
        LEFT JOIN core.script script ON process.script_id = script.id AND script.is_active = 1
	WHERE core.process.id = ^(id) AND core.process.is_active = 1;',
	'Select single Process record by ID with related Script information',
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
	'core.process-get-by-rwk',
	'SELECT
		core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
	FROM core.process
        LEFT JOIN core.script script ON process.script_id = script.id AND script.is_active = 1
	WHERE process.name = ^(name) AND core.process.is_active = 1;',
	'Select single Process record by RWK columns with related Script information',
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
	'core.process-get-history',
	'SELECT
		core.process.*
	FROM core.process
	WHERE core.process.id = ^(id)
	ORDER BY core.process.txn_id DESC;',
	'Select all history records for Process by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	