-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for TestCase (app.test_case)
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
	'app.test_case-select',
	'SELECT
		app.test_case.id,
            test_case.test_plan_id,
            test_case.code,
            test_case.area,
            test_case.title,
            test_case.preconditions,
            test_case.steps,
            test_case.expected_result,
            test_case.priority,
            test_case.component,
            test_case.is_active,
            test_case.created_by,
            test_case.last_updated,
            test_case.last_updated_by,
            test_case.txn_id,
            test_plan.name AS test_plan_name
	FROM app.test_case
        LEFT JOIN app.test_plan test_plan ON test_case.test_plan_id = test_plan.id AND test_plan.is_active = 1
	WHERE app.test_case.is_active = 1
	ORDER BY app.test_case.id;',
	'Select all TestCase records with related TestPlan information',
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
	'app.test_case-get',
	'SELECT
		app.test_case.id,
            test_case.test_plan_id,
            test_case.code,
            test_case.area,
            test_case.title,
            test_case.preconditions,
            test_case.steps,
            test_case.expected_result,
            test_case.priority,
            test_case.component,
            test_case.is_active,
            test_case.created_by,
            test_case.last_updated,
            test_case.last_updated_by,
            test_case.txn_id,
            test_plan.name AS test_plan_name
	FROM app.test_case
        LEFT JOIN app.test_plan test_plan ON test_case.test_plan_id = test_plan.id AND test_plan.is_active = 1
	WHERE app.test_case.id = ^(id) AND app.test_case.is_active = 1;',
	'Select single TestCase record by ID with related TestPlan information',
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
	'app.test_case-get-by-rwk',
	'SELECT
		app.test_case.id,
            test_case.test_plan_id,
            test_case.code,
            test_case.area,
            test_case.title,
            test_case.preconditions,
            test_case.steps,
            test_case.expected_result,
            test_case.priority,
            test_case.component,
            test_case.is_active,
            test_case.created_by,
            test_case.last_updated,
            test_case.last_updated_by,
            test_case.txn_id,
            test_plan.name AS test_plan_name
	FROM app.test_case
        LEFT JOIN app.test_plan test_plan ON test_case.test_plan_id = test_plan.id AND test_plan.is_active = 1
	WHERE test_case.test_plan_id = ^(test_plan_id) AND test_case.code = ^(code) AND test_case.title = ^(title) AND app.test_case.is_active = 1;',
	'Select single TestCase record by RWK columns with related TestPlan information',
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
	'app.test_case-get-history',
	'SELECT
		app.test_case.*
	FROM app.test_case
	WHERE app.test_case.id = ^(id)
	ORDER BY app.test_case.txn_id DESC;',
	'Select all history records for TestCase by id, ordered newest first',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

	