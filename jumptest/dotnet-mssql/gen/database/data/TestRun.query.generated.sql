-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for TestRun (app.test_run)
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
	'app.test_run-select',
	'SELECT
		app.test_run.id,
            test_run.name,
            test_run.test_plan_id,
            test_run.run_at,
            test_run.run_by,
            test_run.notes,
            test_run.is_active,
            test_run.created_by,
            test_run.last_updated,
            test_run.last_updated_by,
            test_run.txn_id,
            test_plan.name AS test_plan_name
	FROM app.test_run
        LEFT JOIN app.test_plan test_plan ON test_run.test_plan_id = test_plan.id AND test_plan.is_active = 1
	WHERE app.test_run.is_active = 1
	ORDER BY app.test_run.id;',
	'Select all TestRun records with related TestPlan information',
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
	'app.test_run-get',
	'SELECT
		app.test_run.id,
            test_run.name,
            test_run.test_plan_id,
            test_run.run_at,
            test_run.run_by,
            test_run.notes,
            test_run.is_active,
            test_run.created_by,
            test_run.last_updated,
            test_run.last_updated_by,
            test_run.txn_id,
            test_plan.name AS test_plan_name
	FROM app.test_run
        LEFT JOIN app.test_plan test_plan ON test_run.test_plan_id = test_plan.id AND test_plan.is_active = 1
	WHERE app.test_run.id = ^(id) AND app.test_run.is_active = 1;',
	'Select single TestRun record by ID with related TestPlan information',
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
	'app.test_run-get-by-rwk',
	'SELECT
		app.test_run.id,
            test_run.name,
            test_run.test_plan_id,
            test_run.run_at,
            test_run.run_by,
            test_run.notes,
            test_run.is_active,
            test_run.created_by,
            test_run.last_updated,
            test_run.last_updated_by,
            test_run.txn_id,
            test_plan.name AS test_plan_name
	FROM app.test_run
        LEFT JOIN app.test_plan test_plan ON test_run.test_plan_id = test_plan.id AND test_plan.is_active = 1
	WHERE test_run.name = ^(name) AND test_run.test_plan_id = ^(test_plan_id) AND app.test_run.is_active = 1;',
	'Select single TestRun record by RWK columns with related TestPlan information',
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
	'app.test_run-get-history',
	'SELECT
		app.test_run.*
	FROM app.test_run
	WHERE app.test_run.id = ^(id)
	ORDER BY app.test_run.txn_id DESC;',
	'Select all history records for TestRun by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	