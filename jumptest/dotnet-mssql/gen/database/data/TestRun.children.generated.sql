-- =====================================
-- Generate SELECT queries for child records
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Child queries for TestRun (app.test_run)
-- =====================================

		
-- Child relationship: Test Run (test_run)
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
	'app.test_run-children-testresult-test_run',
	'SELECT
		test_result.id,
        test_result.test_run_id,
        test_result.test_case_id,
        test_result.test_result_status_id,
        test_result.executed_at,
<<<<<<< HEAD
        test_result.executed_by,
=======
        test_result.executed_by_id,
>>>>>>> main
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
<<<<<<< HEAD
        test_result_status.name AS test_result_status_name
=======
        test_result_status.name AS test_result_status_name,
        executed_by.email AS executed_by_email,
        executed_by.principal_status_id AS executed_by_principal_status_id
>>>>>>> main
	FROM app.test_result
    LEFT JOIN app.test_run test_run ON test_result.test_run_id = test_run.id AND test_run.is_active = 1
    LEFT JOIN app.test_case test_case ON test_result.test_case_id = test_case.id AND test_case.is_active = 1
    LEFT JOIN app.test_result_status test_result_status ON test_result.test_result_status_id = test_result_status.id AND test_result_status.is_active = 1
<<<<<<< HEAD
	WHERE test_result.test_run_id = ^(id) AND test_result.is_active = 1
	ORDER BY test_result.id;',
	'Select all Test Run records for TestRun with related TestRun, TestCase, TestResultStatus information',
=======
    LEFT JOIN core.principal executed_by ON test_result.executed_by_id = executed_by.id AND executed_by.is_active = 1
	WHERE test_result.test_run_id = ^(id) AND test_result.is_active = 1
	ORDER BY test_result.id;',
	'Select all Test Run records for TestRun with related TestRun, TestCase, TestResultStatus, Principal information',
>>>>>>> main
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			