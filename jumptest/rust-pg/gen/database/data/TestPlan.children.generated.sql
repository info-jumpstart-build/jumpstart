-- =====================================
-- Generate SELECT queries for child records
-- =====================================


-- =====================================
-- Child queries for TestPlan (app.test_plan)
-- =====================================

        
-- Child relationship: Test Plan (test_plan)
INSERT INTO core.sql (id, txn_id, name, sql_text, description, created_by, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'app.test_plan-children-testcase-test_plan',
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
    WHERE app.test_case.test_plan_id = ^(id) AND app.test_case.is_active = 1
    ORDER BY app.test_case.id;',
    'Select all Test Plan records for TestPlan with related TestPlan information',
    CURRENT_USER,
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

            
-- Child relationship: Test Plan (test_plan)
INSERT INTO core.sql (id, txn_id, name, sql_text, description, created_by, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'app.test_plan-children-testrun-test_plan',
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
    WHERE app.test_run.test_plan_id = ^(id) AND app.test_run.is_active = 1
    ORDER BY app.test_run.id;',
    'Select all Test Plan records for TestPlan with related TestPlan information',
    CURRENT_USER,
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

            