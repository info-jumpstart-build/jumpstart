-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for Script (core.script)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.script-select',
    'SELECT
        core.script.id,
            script.name,
            script.source,
            script.script_type_id,
            script.is_active,
            script.created_by,
            script.last_updated,
            script.last_updated_by,
            script.txn_id,
            script_type.name AS script_type_name
    FROM core.script
        LEFT JOIN core.script_type script_type ON script.script_type_id = script_type.id AND script_type.is_active = 1
    WHERE core.script.is_active = 1
    ORDER BY core.script.id;',
    'Select all Script records with related ScriptType information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.script-get',
    'SELECT
        core.script.id,
            script.name,
            script.source,
            script.script_type_id,
            script.is_active,
            script.created_by,
            script.last_updated,
            script.last_updated_by,
            script.txn_id,
            script_type.name AS script_type_name
    FROM core.script
        LEFT JOIN core.script_type script_type ON script.script_type_id = script_type.id AND script_type.is_active = 1
    WHERE core.script.id = ^(id) AND core.script.is_active = 1;',
    'Select single Script record by ID with related ScriptType information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.script-get-by-rwk',
    'SELECT
        core.script.id,
            script.name,
            script.source,
            script.script_type_id,
            script.is_active,
            script.created_by,
            script.last_updated,
            script.last_updated_by,
            script.txn_id,
            script_type.name AS script_type_name
    FROM core.script
        LEFT JOIN core.script_type script_type ON script.script_type_id = script_type.id AND script_type.is_active = 1
    WHERE script.name = ^(name) AND script.script_type_id = ^(script_type_id) AND core.script.is_active = 1;',
    'Select single Script record by RWK columns with related ScriptType information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.script-get-history',
    'SELECT
        core.script.id,
            script.name,
            script.source,
            script.script_type_id,
            script.is_active,
            script.created_by,
            script.last_updated,
            script.last_updated_by,
            script.txn_id
    FROM core.script
    WHERE core.script.id = ^(id)
    ORDER BY core.script.txn_id DESC;',
    'Select all history records for Script by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        