-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Process (core.process)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.process-select',
    'SELECT
        core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
    FROM core.process
        LEFT JOIN core.script script ON process.script_id = script.id AND script.is_active = 1
    WHERE core.process.is_active = 1
    ORDER BY core.process.id;',
    'Select all Process records with related Script information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.process-get',
    'SELECT
        core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
    FROM core.process
        LEFT JOIN core.script script ON process.script_id = script.id AND script.is_active = 1
    WHERE core.process.id = ^(id) AND core.process.is_active = 1;',
    'Select single Process record by ID with related Script information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.process-get-by-rwk',
    'SELECT
        core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id,
            script.name AS script_name,
            script.script_type_id AS script_script_type_id
    FROM core.process
        LEFT JOIN core.script script ON process.script_id = script.id AND script.is_active = 1
    WHERE process.name = ^(name) AND core.process.is_active = 1;',
    'Select single Process record by RWK columns with related Script information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.process-get-history',
    'SELECT
        core.process.id,
            process.name,
            process.script_id,
            process.is_active,
            process.created_by,
            process.last_updated,
            process.last_updated_by,
            process.txn_id
    FROM core.process
    WHERE core.process.id = ^(id)
    ORDER BY core.process.txn_id DESC;',
    'Select all history records for Process by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        