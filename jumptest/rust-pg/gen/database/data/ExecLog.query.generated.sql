-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for ExecLog (core.exec_log)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.exec_log-select',
    'SELECT
        core.exec_log.id,
            exec_log.token,
            exec_log.workflow_id,
            exec_log.start_time,
            exec_log.end_time,
            exec_log.exec_status_id,
            exec_log.stdout,
            exec_log.stderr,
            exec_log.is_active,
            exec_log.created_by,
            exec_log.last_updated,
            exec_log.last_updated_by,
            exec_log.txn_id,
            workflow.parent_id AS workflow_parent_id,
            workflow.name AS workflow_name
    FROM core.exec_log
        LEFT JOIN core.workflow workflow ON exec_log.workflow_id = workflow.id AND workflow.is_active = 1
    WHERE core.exec_log.is_active = 1
    ORDER BY core.exec_log.id;',
    'Select all ExecLog records with related Workflow information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.exec_log-get',
    'SELECT
        core.exec_log.id,
            exec_log.token,
            exec_log.workflow_id,
            exec_log.start_time,
            exec_log.end_time,
            exec_log.exec_status_id,
            exec_log.stdout,
            exec_log.stderr,
            exec_log.is_active,
            exec_log.created_by,
            exec_log.last_updated,
            exec_log.last_updated_by,
            exec_log.txn_id,
            workflow.parent_id AS workflow_parent_id,
            workflow.name AS workflow_name
    FROM core.exec_log
        LEFT JOIN core.workflow workflow ON exec_log.workflow_id = workflow.id AND workflow.is_active = 1
    WHERE core.exec_log.id = ^(id) AND core.exec_log.is_active = 1;',
    'Select single ExecLog record by ID with related Workflow information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.exec_log-get-by-rwk',
    'SELECT
        core.exec_log.id,
            exec_log.token,
            exec_log.workflow_id,
            exec_log.start_time,
            exec_log.end_time,
            exec_log.exec_status_id,
            exec_log.stdout,
            exec_log.stderr,
            exec_log.is_active,
            exec_log.created_by,
            exec_log.last_updated,
            exec_log.last_updated_by,
            exec_log.txn_id,
            workflow.parent_id AS workflow_parent_id,
            workflow.name AS workflow_name
    FROM core.exec_log
        LEFT JOIN core.workflow workflow ON exec_log.workflow_id = workflow.id AND workflow.is_active = 1
    WHERE exec_log.token = ^(token) AND exec_log.workflow_id = ^(workflow_id) AND exec_log.start_time = ^(start_time) AND exec_log.end_time = ^(end_time) AND core.exec_log.is_active = 1;',
    'Select single ExecLog record by RWK columns with related Workflow information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.exec_log-get-history',
    'SELECT
        core.exec_log.id,
            exec_log.token,
            exec_log.workflow_id,
            exec_log.start_time,
            exec_log.end_time,
            exec_log.exec_status_id,
            exec_log.stdout,
            exec_log.stderr,
            exec_log.is_active,
            exec_log.created_by,
            exec_log.last_updated,
            exec_log.last_updated_by,
            exec_log.txn_id
    FROM core.exec_log
    WHERE core.exec_log.id = ^(id)
    ORDER BY core.exec_log.txn_id DESC;',
    'Select all history records for ExecLog by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        