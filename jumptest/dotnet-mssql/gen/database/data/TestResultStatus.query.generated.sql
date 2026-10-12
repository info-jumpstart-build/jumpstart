-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for TestResultStatus (app.test_result_status)
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
	'app.test_result_status-select',
	'SELECT
		app.test_result_status.id,
            test_result_status.name,
            test_result_status.is_active,
            test_result_status.created_by,
            test_result_status.last_updated,
            test_result_status.last_updated_by,
            test_result_status.txn_id
	FROM app.test_result_status
	WHERE app.test_result_status.is_active = 1
	ORDER BY app.test_result_status.id;',
	'Select all TestResultStatus records with related  information',
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
	'app.test_result_status-get',
	'SELECT
		app.test_result_status.id,
            test_result_status.name,
            test_result_status.is_active,
            test_result_status.created_by,
            test_result_status.last_updated,
            test_result_status.last_updated_by,
            test_result_status.txn_id
	FROM app.test_result_status
	WHERE app.test_result_status.id = ^(id) AND app.test_result_status.is_active = 1;',
	'Select single TestResultStatus record by ID with related  information',
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
	'app.test_result_status-get-by-rwk',
	'SELECT
		app.test_result_status.id,
            test_result_status.name,
            test_result_status.is_active,
            test_result_status.created_by,
            test_result_status.last_updated,
            test_result_status.last_updated_by,
            test_result_status.txn_id
	FROM app.test_result_status
	WHERE test_result_status.name = ^(name) AND app.test_result_status.is_active = 1;',
	'Select single TestResultStatus record by RWK columns with related  information',
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
	'app.test_result_status-get-history',
	'SELECT
		app.test_result_status.*
	FROM app.test_result_status
	WHERE app.test_result_status.id = ^(id)
	ORDER BY app.test_result_status.txn_id DESC;',
	'Select all history records for TestResultStatus by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	