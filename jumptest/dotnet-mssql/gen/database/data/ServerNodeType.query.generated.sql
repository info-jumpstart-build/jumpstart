-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for ServerNodeType (core.server_node_type)
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
	'core.server_node_type-select',
	'SELECT
		core.server_node_type.id,
            server_node_type.name,
            server_node_type.is_active,
            server_node_type.created_by,
            server_node_type.last_updated,
            server_node_type.last_updated_by,
            server_node_type.txn_id
	FROM core.server_node_type
	WHERE core.server_node_type.is_active = 1
	ORDER BY core.server_node_type.id;',
	'Select all ServerNodeType records with related  information',
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
	'core.server_node_type-get',
	'SELECT
		core.server_node_type.id,
            server_node_type.name,
            server_node_type.is_active,
            server_node_type.created_by,
            server_node_type.last_updated,
            server_node_type.last_updated_by,
            server_node_type.txn_id
	FROM core.server_node_type
	WHERE core.server_node_type.id = ^(id) AND core.server_node_type.is_active = 1;',
	'Select single ServerNodeType record by ID with related  information',
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
	'core.server_node_type-get-by-rwk',
	'SELECT
		core.server_node_type.id,
            server_node_type.name,
            server_node_type.is_active,
            server_node_type.created_by,
            server_node_type.last_updated,
            server_node_type.last_updated_by,
            server_node_type.txn_id
	FROM core.server_node_type
	WHERE server_node_type.name = ^(name) AND core.server_node_type.is_active = 1;',
	'Select single ServerNodeType record by RWK columns with related  information',
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
	'core.server_node_type-get-history',
	'SELECT
		core.server_node_type.*
	FROM core.server_node_type
	WHERE core.server_node_type.id = ^(id)
	ORDER BY core.server_node_type.txn_id DESC;',
	'Select all history records for ServerNodeType by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	