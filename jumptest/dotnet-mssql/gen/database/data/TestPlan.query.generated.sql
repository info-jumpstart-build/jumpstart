-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for TestPlan (app.test_plan)
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
	'app.test_plan-select',
	'SELECT
		app.test_plan.id,
            test_plan.name,
            test_plan.description,
            test_plan.is_active,
            test_plan.created_by,
            test_plan.last_updated,
            test_plan.last_updated_by,
            test_plan.txn_id
	FROM app.test_plan
	WHERE app.test_plan.is_active = 1
	ORDER BY app.test_plan.id;',
	'Select all TestPlan records with related  information',
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
	'app.test_plan-get',
	'SELECT
		app.test_plan.id,
            test_plan.name,
            test_plan.description,
            test_plan.is_active,
            test_plan.created_by,
            test_plan.last_updated,
            test_plan.last_updated_by,
            test_plan.txn_id
	FROM app.test_plan
	WHERE app.test_plan.id = ^(id) AND app.test_plan.is_active = 1;',
	'Select single TestPlan record by ID with related  information',
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
	'app.test_plan-get-by-rwk',
	'SELECT
		app.test_plan.id,
            test_plan.name,
            test_plan.description,
            test_plan.is_active,
            test_plan.created_by,
            test_plan.last_updated,
            test_plan.last_updated_by,
            test_plan.txn_id
	FROM app.test_plan
	WHERE test_plan.name = ^(name) AND app.test_plan.is_active = 1;',
	'Select single TestPlan record by RWK columns with related  information',
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
	'app.test_plan-get-history',
	'SELECT
		app.test_plan.*
	FROM app.test_plan
	WHERE app.test_plan.id = ^(id)
	ORDER BY app.test_plan.txn_id DESC;',
	'Select all history records for TestPlan by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	