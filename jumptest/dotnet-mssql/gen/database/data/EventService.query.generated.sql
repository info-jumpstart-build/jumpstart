-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for EventService (core.event_service)
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
	'core.event_service-select',
	'SELECT
		core.event_service.id,
            event_service.event_type,
            event_service.objectname_filter,
            event_service.methodname_filter,
            event_service.script_id,
            event_service.is_active,
            event_service.created_by,
            event_service.last_updated,
            event_service.last_updated_by,
            event_service.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
	FROM core.event_service
        LEFT JOIN core.script script ON event_service.script_id = script.id AND script.is_active = 1
	WHERE core.event_service.is_active = 1
	ORDER BY core.event_service.id;',
	'Select all EventService records with related Script information',
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
	'core.event_service-get',
	'SELECT
		core.event_service.id,
            event_service.event_type,
            event_service.objectname_filter,
            event_service.methodname_filter,
            event_service.script_id,
            event_service.is_active,
            event_service.created_by,
            event_service.last_updated,
            event_service.last_updated_by,
            event_service.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
	FROM core.event_service
        LEFT JOIN core.script script ON event_service.script_id = script.id AND script.is_active = 1
	WHERE core.event_service.id = ^(id) AND core.event_service.is_active = 1;',
	'Select single EventService record by ID with related Script information',
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
	'core.event_service-get-by-rwk',
	'SELECT
		core.event_service.id,
            event_service.event_type,
            event_service.objectname_filter,
            event_service.methodname_filter,
            event_service.script_id,
            event_service.is_active,
            event_service.created_by,
            event_service.last_updated,
            event_service.last_updated_by,
            event_service.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
	FROM core.event_service
        LEFT JOIN core.script script ON event_service.script_id = script.id AND script.is_active = 1
	WHERE event_service.event_type = ^(event_type) AND event_service.objectname_filter = ^(objectname_filter) AND event_service.methodname_filter = ^(methodname_filter) AND event_service.script_id = ^(script_id) AND core.event_service.is_active = 1;',
	'Select single EventService record by RWK columns with related Script information',
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
	'core.event_service-get-history',
	'SELECT
		core.event_service.*
	FROM core.event_service
	WHERE core.event_service.id = ^(id)
	ORDER BY core.event_service.txn_id DESC;',
	'Select all history records for EventService by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	