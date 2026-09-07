-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for ExecStatus (core.exec_status)
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
	'core.exec_status-select',
	'SELECT
		core.exec_status.id,
            exec_status.name,
            exec_status.image,
            exec_status.is_active,
            exec_status.created_by,
            exec_status.last_updated,
            exec_status.last_updated_by,
            exec_status.txn_id
	FROM core.exec_status
	WHERE core.exec_status.is_active = 1
	ORDER BY core.exec_status.id;',
	'Select all ExecStatus records with related  information',
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
	'core.exec_status-get',
	'SELECT
		core.exec_status.id,
            exec_status.name,
            exec_status.image,
            exec_status.is_active,
            exec_status.created_by,
            exec_status.last_updated,
            exec_status.last_updated_by,
            exec_status.txn_id
	FROM core.exec_status
	WHERE core.exec_status.id = ^(id) AND core.exec_status.is_active = 1;',
	'Select single ExecStatus record by ID with related  information',
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
	'core.exec_status-get-by-rwk',
	'SELECT
		core.exec_status.id,
            exec_status.name,
            exec_status.image,
            exec_status.is_active,
            exec_status.created_by,
            exec_status.last_updated,
            exec_status.last_updated_by,
            exec_status.txn_id
	FROM core.exec_status
	WHERE exec_status.name = ^(name) AND exec_status.image = ^(image) AND core.exec_status.is_active = 1;',
	'Select single ExecStatus record by RWK columns with related  information',
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
	'core.exec_status-get-history',
	'SELECT
		core.exec_status.*
	FROM core.exec_status
	WHERE core.exec_status.id = ^(id)
	ORDER BY core.exec_status.txn_id DESC;',
	'Select all history records for ExecStatus by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	