-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for ServerNode (core.server_node)
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
	'core.server_node-select',
	'SELECT
		core.server_node.id,
            server_node.server_node_type_id,
            server_node.hostname,
            server_node.ip_address,
            server_node.port,
            server_node.username,
            server_node.url,
            server_node.user_domain,
            server_node.os_name,
            server_node.os_version,
            server_node.architecture,
            server_node.registered_at,
            server_node.server_node_status_id,
            server_node.is_active,
            server_node.created_by,
            server_node.last_updated,
            server_node.last_updated_by,
            server_node.txn_id,
            server_node_type.name AS server_node_type_name,
            server_node_status.name AS server_node_status_name
	FROM core.server_node
        LEFT JOIN core.server_node_type server_node_type ON server_node.server_node_type_id = server_node_type.id AND server_node_type.is_active = 1
        LEFT JOIN core.server_node_status server_node_status ON server_node.server_node_status_id = server_node_status.id AND server_node_status.is_active = 1
	WHERE core.server_node.is_active = 1
	ORDER BY core.server_node.id;',
	'Select all ServerNode records with related ServerNodeType, ServerNodeStatus information',
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
	'core.server_node-get',
	'SELECT
		core.server_node.id,
            server_node.server_node_type_id,
            server_node.hostname,
            server_node.ip_address,
            server_node.port,
            server_node.username,
            server_node.url,
            server_node.user_domain,
            server_node.os_name,
            server_node.os_version,
            server_node.architecture,
            server_node.registered_at,
            server_node.server_node_status_id,
            server_node.is_active,
            server_node.created_by,
            server_node.last_updated,
            server_node.last_updated_by,
            server_node.txn_id,
            server_node_type.name AS server_node_type_name,
            server_node_status.name AS server_node_status_name
	FROM core.server_node
        LEFT JOIN core.server_node_type server_node_type ON server_node.server_node_type_id = server_node_type.id AND server_node_type.is_active = 1
        LEFT JOIN core.server_node_status server_node_status ON server_node.server_node_status_id = server_node_status.id AND server_node_status.is_active = 1
	WHERE core.server_node.id = ^(id) AND core.server_node.is_active = 1;',
	'Select single ServerNode record by ID with related ServerNodeType, ServerNodeStatus information',
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
	'core.server_node-get-by-rwk',
	'SELECT
		core.server_node.id,
            server_node.server_node_type_id,
            server_node.hostname,
            server_node.ip_address,
            server_node.port,
            server_node.username,
            server_node.url,
            server_node.user_domain,
            server_node.os_name,
            server_node.os_version,
            server_node.architecture,
            server_node.registered_at,
            server_node.server_node_status_id,
            server_node.is_active,
            server_node.created_by,
            server_node.last_updated,
            server_node.last_updated_by,
            server_node.txn_id,
            server_node_type.name AS server_node_type_name,
            server_node_status.name AS server_node_status_name
	FROM core.server_node
        LEFT JOIN core.server_node_type server_node_type ON server_node.server_node_type_id = server_node_type.id AND server_node_type.is_active = 1
        LEFT JOIN core.server_node_status server_node_status ON server_node.server_node_status_id = server_node_status.id AND server_node_status.is_active = 1
	WHERE server_node.hostname = ^(hostname) AND server_node.port = ^(port) AND core.server_node.is_active = 1;',
	'Select single ServerNode record by RWK columns with related ServerNodeType, ServerNodeStatus information',
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
	'core.server_node-get-history',
	'SELECT
		core.server_node.*
	FROM core.server_node
	WHERE core.server_node.id = ^(id)
	ORDER BY core.server_node.txn_id DESC;',
	'Select all history records for ServerNode by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	