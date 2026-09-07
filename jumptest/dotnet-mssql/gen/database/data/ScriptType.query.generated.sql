-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for ScriptType (core.script_type)
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
	'core.script_type-select',
	'SELECT
		core.script_type.id,
            script_type.name,
            script_type.is_active,
            script_type.created_by,
            script_type.last_updated,
            script_type.last_updated_by,
            script_type.txn_id
	FROM core.script_type
	WHERE core.script_type.is_active = 1
	ORDER BY core.script_type.id;',
	'Select all ScriptType records with related  information',
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
	'core.script_type-get',
	'SELECT
		core.script_type.id,
            script_type.name,
            script_type.is_active,
            script_type.created_by,
            script_type.last_updated,
            script_type.last_updated_by,
            script_type.txn_id
	FROM core.script_type
	WHERE core.script_type.id = ^(id) AND core.script_type.is_active = 1;',
	'Select single ScriptType record by ID with related  information',
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
	'core.script_type-get-by-rwk',
	'SELECT
		core.script_type.id,
            script_type.name,
            script_type.is_active,
            script_type.created_by,
            script_type.last_updated,
            script_type.last_updated_by,
            script_type.txn_id
	FROM core.script_type
	WHERE script_type.name = ^(name) AND core.script_type.is_active = 1;',
	'Select single ScriptType record by RWK columns with related  information',
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
	'core.script_type-get-history',
	'SELECT
		core.script_type.*
	FROM core.script_type
	WHERE core.script_type.id = ^(id)
	ORDER BY core.script_type.txn_id DESC;',
	'Select all history records for ScriptType by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	