-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for ServerNodeStatus (core.server_node_status)
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
	'core.server_node_status-select',
	'SELECT
		core.server_node_status.id,
            server_node_status.name,
            server_node_status.is_active,
            server_node_status.created_by,
            server_node_status.last_updated,
            server_node_status.last_updated_by,
            server_node_status.txn_id
	FROM core.server_node_status
	WHERE core.server_node_status.is_active = 1
	ORDER BY core.server_node_status.id;',
	'Select all ServerNodeStatus records with related  information',
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
	'core.server_node_status-get',
	'SELECT
		core.server_node_status.id,
            server_node_status.name,
            server_node_status.is_active,
            server_node_status.created_by,
            server_node_status.last_updated,
            server_node_status.last_updated_by,
            server_node_status.txn_id
	FROM core.server_node_status
	WHERE core.server_node_status.id = ^(id) AND core.server_node_status.is_active = 1;',
	'Select single ServerNodeStatus record by ID with related  information',
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
	'core.server_node_status-get-by-rwk',
	'SELECT
		core.server_node_status.id,
            server_node_status.name,
            server_node_status.is_active,
            server_node_status.created_by,
            server_node_status.last_updated,
            server_node_status.last_updated_by,
            server_node_status.txn_id
	FROM core.server_node_status
	WHERE server_node_status.name = ^(name) AND core.server_node_status.is_active = 1;',
	'Select single ServerNodeStatus record by RWK columns with related  information',
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
	'core.server_node_status-get-history',
	'SELECT
		core.server_node_status.*
	FROM core.server_node_status
	WHERE core.server_node_status.id = ^(id)
	ORDER BY core.server_node_status.txn_id DESC;',
	'Select all history records for ServerNodeStatus by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	