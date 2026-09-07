-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for PrincipalStatus (core.principal_status)
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
	'core.principal_status-select',
	'SELECT
		core.principal_status.id,
            principal_status.name,
            principal_status.is_active,
            principal_status.created_by,
            principal_status.last_updated,
            principal_status.last_updated_by,
            principal_status.txn_id
	FROM core.principal_status
	WHERE core.principal_status.is_active = 1
	ORDER BY core.principal_status.id;',
	'Select all PrincipalStatus records with related  information',
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
	'core.principal_status-get',
	'SELECT
		core.principal_status.id,
            principal_status.name,
            principal_status.is_active,
            principal_status.created_by,
            principal_status.last_updated,
            principal_status.last_updated_by,
            principal_status.txn_id
	FROM core.principal_status
	WHERE core.principal_status.id = ^(id) AND core.principal_status.is_active = 1;',
	'Select single PrincipalStatus record by ID with related  information',
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
	'core.principal_status-get-by-rwk',
	'SELECT
		core.principal_status.id,
            principal_status.name,
            principal_status.is_active,
            principal_status.created_by,
            principal_status.last_updated,
            principal_status.last_updated_by,
            principal_status.txn_id
	FROM core.principal_status
	WHERE principal_status.name = ^(name) AND core.principal_status.is_active = 1;',
	'Select single PrincipalStatus record by RWK columns with related  information',
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
	'core.principal_status-get-history',
	'SELECT
		core.principal_status.*
	FROM core.principal_status
	WHERE core.principal_status.id = ^(id)
	ORDER BY core.principal_status.txn_id DESC;',
	'Select all history records for PrincipalStatus by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	