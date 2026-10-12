-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for PrincipalOrg (core.principal_org)
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
	'core.principal_org-select',
	'SELECT
		core.principal_org.id,
            principal_org.org_id,
            principal_org.principal_id,
            principal_org.is_active,
            principal_org.created_by,
            principal_org.last_updated,
            principal_org.last_updated_by,
            principal_org.txn_id,
            org.name AS org_name,
            principal.email AS principal_email,
<<<<<<< HEAD
            principal.enabled AS principal_enabled
=======
            principal.principal_status_id AS principal_principal_status_id
>>>>>>> main
	FROM core.principal_org
        LEFT JOIN core.org org ON principal_org.org_id = org.id AND org.is_active = 1
        LEFT JOIN core.principal principal ON principal_org.principal_id = principal.id AND principal.is_active = 1
	WHERE core.principal_org.is_active = 1
	ORDER BY core.principal_org.id;',
	'Select all PrincipalOrg records with related Org, Principal information',
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
	'core.principal_org-get',
	'SELECT
		core.principal_org.id,
            principal_org.org_id,
            principal_org.principal_id,
            principal_org.is_active,
            principal_org.created_by,
            principal_org.last_updated,
            principal_org.last_updated_by,
            principal_org.txn_id,
            org.name AS org_name,
            principal.email AS principal_email,
<<<<<<< HEAD
            principal.enabled AS principal_enabled
=======
            principal.principal_status_id AS principal_principal_status_id
>>>>>>> main
	FROM core.principal_org
        LEFT JOIN core.org org ON principal_org.org_id = org.id AND org.is_active = 1
        LEFT JOIN core.principal principal ON principal_org.principal_id = principal.id AND principal.is_active = 1
	WHERE core.principal_org.id = ^(id) AND core.principal_org.is_active = 1;',
	'Select single PrincipalOrg record by ID with related Org, Principal information',
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
	'core.principal_org-get-by-rwk',
	'SELECT
		core.principal_org.id,
            principal_org.org_id,
            principal_org.principal_id,
            principal_org.is_active,
            principal_org.created_by,
            principal_org.last_updated,
            principal_org.last_updated_by,
            principal_org.txn_id,
            org.name AS org_name,
            principal.email AS principal_email,
<<<<<<< HEAD
            principal.enabled AS principal_enabled
=======
            principal.principal_status_id AS principal_principal_status_id
>>>>>>> main
	FROM core.principal_org
        LEFT JOIN core.org org ON principal_org.org_id = org.id AND org.is_active = 1
        LEFT JOIN core.principal principal ON principal_org.principal_id = principal.id AND principal.is_active = 1
	WHERE principal_org.org_id = ^(org_id) AND principal_org.principal_id = ^(principal_id) AND core.principal_org.is_active = 1;',
	'Select single PrincipalOrg record by RWK columns with related Org, Principal information',
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
	'core.principal_org-get-history',
	'SELECT
		core.principal_org.*
	FROM core.principal_org
	WHERE core.principal_org.id = ^(id)
	ORDER BY core.principal_org.txn_id DESC;',
	'Select all history records for PrincipalOrg by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	