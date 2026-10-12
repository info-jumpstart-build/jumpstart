-- =====================================
-- Generate SELECT queries for child records
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Child queries for TestPlan (app.test_plan)
-- =====================================

		
-- Child relationship: Test Plan (test_plan)
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
	'app.test_plan-children-testcase-test_plan',
	'SELECT
		test_case.id,
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
	WHERE test_case.test_plan_id = ^(id) AND test_case.is_active = 1
	ORDER BY test_case.id;',
	'Select all Test Plan records for TestPlan with related TestPlan information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			
-- Child relationship: Test Plan (test_plan)
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
	'app.test_plan-children-testrun-test_plan',
	'SELECT
		test_run.id,
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
	WHERE test_run.test_plan_id = ^(id) AND test_run.is_active = 1
	ORDER BY test_run.id;',
	'Select all Test Plan records for TestPlan with related TestPlan information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			