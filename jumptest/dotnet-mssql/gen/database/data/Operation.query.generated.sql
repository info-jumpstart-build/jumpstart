-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Operation (core.operation)
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
	'core.operation-select',
	'SELECT
		core.operation.id,
            operation.objectname,
            operation.methodname,
            operation.is_active,
            operation.created_by,
            operation.last_updated,
            operation.last_updated_by,
            operation.txn_id
	FROM core.operation
	WHERE core.operation.is_active = 1
	ORDER BY core.operation.id;',
	'Select all Operation records with related  information',
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
	'core.operation-get',
	'SELECT
		core.operation.id,
            operation.objectname,
            operation.methodname,
            operation.is_active,
            operation.created_by,
            operation.last_updated,
            operation.last_updated_by,
            operation.txn_id
	FROM core.operation
	WHERE core.operation.id = ^(id) AND core.operation.is_active = 1;',
	'Select single Operation record by ID with related  information',
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
	'core.operation-get-by-rwk',
	'SELECT
		core.operation.id,
            operation.objectname,
            operation.methodname,
            operation.is_active,
            operation.created_by,
            operation.last_updated,
            operation.last_updated_by,
            operation.txn_id
	FROM core.operation
	WHERE operation.objectname = ^(objectname) AND operation.methodname = ^(methodname) AND core.operation.is_active = 1;',
	'Select single Operation record by RWK columns with related  information',
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
	'core.operation-get-history',
	'SELECT
		core.operation.*
	FROM core.operation
	WHERE core.operation.id = ^(id)
	ORDER BY core.operation.txn_id DESC;',
	'Select all history records for Operation by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	