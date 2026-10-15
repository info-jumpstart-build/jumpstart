-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for  (.)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    '.-select',
    'SELECT
        ..id,
            .,
            .is_active,
            .created_by,
            .last_updated,
            .last_updated_by,
            .txn_id
    FROM .
    WHERE ..is_active = 1
    ORDER BY ..id;',
    'Select all  records with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    '.-get',
    'SELECT
        ..id,
            .,
            .is_active,
            .created_by,
            .last_updated,
            .last_updated_by,
            .txn_id
    FROM .
    WHERE ..id = ^(id) AND ..is_active = 1;',
    'Select single  record by ID with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    '.-get-by-rwk',
    'SELECT
        ..id,
            .,
            .is_active,
            .created_by,
            .last_updated,
            .last_updated_by,
            .txn_id
    FROM .
    WHERE  AND ..is_active = 1;',
    'Select single  record by RWK columns with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    '.-get-history',
    'SELECT
        ..id,
            .,
            .is_active,
            .created_by,
            .last_updated,
            .last_updated_by,
            .txn_id
    FROM .
    WHERE ..id = ^(id)
    ORDER BY ..txn_id DESC;',
    'Select all history records for  by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        