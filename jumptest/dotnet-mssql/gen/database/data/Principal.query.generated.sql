-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Principal (core.principal)
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
	'core.principal-select',
	'SELECT
		core.principal.id,
            principal.first_name,
            principal.last_name,
            principal.username,
            principal.email,
<<<<<<< HEAD
            principal.enabled,
=======
            principal.principal_status_id,
>>>>>>> main
            principal.created_date,
            principal.last_login_date,
            principal.is_active,
            principal.created_by,
            principal.last_updated,
            principal.last_updated_by,
<<<<<<< HEAD
            principal.txn_id
	FROM core.principal
	WHERE core.principal.is_active = 1
	ORDER BY core.principal.id;',
	'Select all Principal records with related  information',
=======
            principal.txn_id,
            principal_status.name AS principal_status_name
	FROM core.principal
        LEFT JOIN core.principal_status principal_status ON principal.principal_status_id = principal_status.id AND principal_status.is_active = 1
	WHERE core.principal.is_active = 1
	ORDER BY core.principal.id;',
	'Select all Principal records with related PrincipalStatus information',
>>>>>>> main
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
	'core.principal-get',
	'SELECT
		core.principal.id,
            principal.first_name,
            principal.last_name,
            principal.username,
            principal.email,
<<<<<<< HEAD
            principal.enabled,
=======
            principal.principal_status_id,
>>>>>>> main
            principal.created_date,
            principal.last_login_date,
            principal.is_active,
            principal.created_by,
            principal.last_updated,
            principal.last_updated_by,
<<<<<<< HEAD
            principal.txn_id
	FROM core.principal
	WHERE core.principal.id = ^(id) AND core.principal.is_active = 1;',
	'Select single Principal record by ID with related  information',
=======
            principal.txn_id,
            principal_status.name AS principal_status_name
	FROM core.principal
        LEFT JOIN core.principal_status principal_status ON principal.principal_status_id = principal_status.id AND principal_status.is_active = 1
	WHERE core.principal.id = ^(id) AND core.principal.is_active = 1;',
	'Select single Principal record by ID with related PrincipalStatus information',
>>>>>>> main
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
	'core.principal-get-by-rwk',
	'SELECT
		core.principal.id,
            principal.first_name,
            principal.last_name,
            principal.username,
            principal.email,
<<<<<<< HEAD
            principal.enabled,
=======
            principal.principal_status_id,
>>>>>>> main
            principal.created_date,
            principal.last_login_date,
            principal.is_active,
            principal.created_by,
            principal.last_updated,
            principal.last_updated_by,
<<<<<<< HEAD
            principal.txn_id
	FROM core.principal
	WHERE principal.email = ^(email) AND principal.enabled = ^(enabled) AND core.principal.is_active = 1;',
	'Select single Principal record by RWK columns with related  information',
=======
            principal.txn_id,
            principal_status.name AS principal_status_name
	FROM core.principal
        LEFT JOIN core.principal_status principal_status ON principal.principal_status_id = principal_status.id AND principal_status.is_active = 1
	WHERE principal.email = ^(email) AND principal.principal_status_id = ^(principal_status_id) AND core.principal.is_active = 1;',
	'Select single Principal record by RWK columns with related PrincipalStatus information',
>>>>>>> main
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
	'core.principal-get-history',
	'SELECT
		core.principal.*
	FROM core.principal
	WHERE core.principal.id = ^(id)
	ORDER BY core.principal.txn_id DESC;',
	'Select all history records for Principal by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	