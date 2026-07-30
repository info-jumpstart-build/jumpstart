-- =====================================
-- Generate SELECT queries for child records
-- =====================================


-- =====================================
-- Child queries for TestRun (app.test_run)
-- =====================================

        
-- Child relationship: Test Run (test_run)
INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'app.test_run-children-testresult-test_run',
    'SELECT
        app.test_result.id,
            test_result.test_run_id,
            test_result.test_case_id,
            test_result.test_result_status_id,
            test_result.executed_at,
            test_result.executed_by_id,
            test_result.actual_result,
            test_result.notes,
            test_result.is_active,
            test_result.created_by,
            test_result.last_updated,
            test_result.last_updated_by,
            test_result.txn_id,
            test_run.name AS test_run_name,
            test_run.test_plan_id AS test_run_test_plan_id,
            test_case.test_plan_id AS test_case_test_plan_id,
            test_case.code AS test_case_code,
            test_case.title AS test_case_title,
            test_result_status.name AS test_result_status_name,
            executed_by.email AS executed_by_email,
            executed_by.principal_status_id AS executed_by_principal_status_id
    FROM app.test_result
        LEFT JOIN app.test_run test_run ON test_result.test_run_id = test_run.id AND test_run.is_active = 1
        LEFT JOIN app.test_case test_case ON test_result.test_case_id = test_case.id AND test_case.is_active = 1
        LEFT JOIN app.test_result_status test_result_status ON test_result.test_result_status_id = test_result_status.id AND test_result_status.is_active = 1
        LEFT JOIN core.principal executed_by ON test_result.executed_by_id = executed_by.id AND executed_by.is_active = 1
    WHERE app.test_result.test_run_id = ^(id) AND app.test_result.is_active = 1
    ORDER BY app.test_result.id;',
    'Select all Test Run records for TestRun with related TestRun, TestCase, TestResultStatus, Principal information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

            