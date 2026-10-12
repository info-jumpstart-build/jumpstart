-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for WorkflowType (core.workflow_type)
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
	'core.workflow_type-select',
	'SELECT
		core.workflow_type.id,
            workflow_type.name,
            workflow_type.is_active,
            workflow_type.created_by,
            workflow_type.last_updated,
            workflow_type.last_updated_by,
            workflow_type.txn_id
	FROM core.workflow_type
	WHERE core.workflow_type.is_active = 1
	ORDER BY core.workflow_type.id;',
	'Select all WorkflowType records with related  information',
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
	'core.workflow_type-get',
	'SELECT
		core.workflow_type.id,
            workflow_type.name,
            workflow_type.is_active,
            workflow_type.created_by,
            workflow_type.last_updated,
            workflow_type.last_updated_by,
            workflow_type.txn_id
	FROM core.workflow_type
	WHERE core.workflow_type.id = ^(id) AND core.workflow_type.is_active = 1;',
	'Select single WorkflowType record by ID with related  information',
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
	'core.workflow_type-get-by-rwk',
	'SELECT
		core.workflow_type.id,
            workflow_type.name,
            workflow_type.is_active,
            workflow_type.created_by,
            workflow_type.last_updated,
            workflow_type.last_updated_by,
            workflow_type.txn_id
	FROM core.workflow_type
	WHERE workflow_type.name = ^(name) AND core.workflow_type.is_active = 1;',
	'Select single WorkflowType record by RWK columns with related  information',
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
	'core.workflow_type-get-history',
	'SELECT
		core.workflow_type.*
	FROM core.workflow_type
	WHERE core.workflow_type.id = ^(id)
	ORDER BY core.workflow_type.txn_id DESC;',
	'Select all history records for WorkflowType by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	