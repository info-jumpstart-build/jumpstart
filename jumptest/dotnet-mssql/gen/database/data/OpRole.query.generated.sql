-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for OpRole (core.op_role)
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
	'core.op_role-select',
	'SELECT
		core.op_role.id,
            op_role.name,
            op_role.is_active,
            op_role.created_by,
            op_role.last_updated,
            op_role.last_updated_by,
            op_role.txn_id
	FROM core.op_role
	WHERE core.op_role.is_active = 1
	ORDER BY core.op_role.id;',
	'Select all OpRole records with related  information',
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
	'core.op_role-get',
	'SELECT
		core.op_role.id,
            op_role.name,
            op_role.is_active,
            op_role.created_by,
            op_role.last_updated,
            op_role.last_updated_by,
            op_role.txn_id
	FROM core.op_role
	WHERE core.op_role.id = ^(id) AND core.op_role.is_active = 1;',
	'Select single OpRole record by ID with related  information',
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
	'core.op_role-get-by-rwk',
	'SELECT
		core.op_role.id,
            op_role.name,
            op_role.is_active,
            op_role.created_by,
            op_role.last_updated,
            op_role.last_updated_by,
            op_role.txn_id
	FROM core.op_role
	WHERE op_role.name = ^(name) AND core.op_role.is_active = 1;',
	'Select single OpRole record by RWK columns with related  information',
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
	'core.op_role-get-history',
	'SELECT
		core.op_role.*
	FROM core.op_role
	WHERE core.op_role.id = ^(id)
	ORDER BY core.op_role.txn_id DESC;',
	'Select all history records for OpRole by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	