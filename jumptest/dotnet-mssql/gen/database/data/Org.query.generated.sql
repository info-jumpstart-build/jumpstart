-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Org (core.org)
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
	'core.org-select',
	'SELECT
		core.org.id,
            org.name,
            org.is_active,
            org.created_by,
            org.last_updated,
            org.last_updated_by,
            org.txn_id
	FROM core.org
	WHERE core.org.is_active = 1
	ORDER BY core.org.id;',
	'Select all Org records with related  information',
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
	'core.org-get',
	'SELECT
		core.org.id,
            org.name,
            org.is_active,
            org.created_by,
            org.last_updated,
            org.last_updated_by,
            org.txn_id
	FROM core.org
	WHERE core.org.id = ^(id) AND core.org.is_active = 1;',
	'Select single Org record by ID with related  information',
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
	'core.org-get-by-rwk',
	'SELECT
		core.org.id,
            org.name,
            org.is_active,
            org.created_by,
            org.last_updated,
            org.last_updated_by,
            org.txn_id
	FROM core.org
	WHERE org.name = ^(name) AND core.org.is_active = 1;',
	'Select single Org record by RWK columns with related  information',
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
	'core.org-get-history',
	'SELECT
		core.org.*
	FROM core.org
	WHERE core.org.id = ^(id)
	ORDER BY core.org.txn_id DESC;',
	'Select all history records for Org by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	