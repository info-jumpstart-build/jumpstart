-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronDow (core.cron_dow)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dow-select',
    'SELECT
        core.cron_dow.id,
            cron_dow.name,
            cron_dow.is_active,
            cron_dow.created_by,
            cron_dow.last_updated,
            cron_dow.last_updated_by,
            cron_dow.txn_id
    FROM core.cron_dow
    WHERE core.cron_dow.is_active = 1
    ORDER BY core.cron_dow.id;',
    'Select all CronDow records with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dow-get',
    'SELECT
        core.cron_dow.id,
            cron_dow.name,
            cron_dow.is_active,
            cron_dow.created_by,
            cron_dow.last_updated,
            cron_dow.last_updated_by,
            cron_dow.txn_id
    FROM core.cron_dow
    WHERE core.cron_dow.id = ^(id) AND core.cron_dow.is_active = 1;',
    'Select single CronDow record by ID with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dow-get-by-rwk',
    'SELECT
        core.cron_dow.id,
            cron_dow.name,
            cron_dow.is_active,
            cron_dow.created_by,
            cron_dow.last_updated,
            cron_dow.last_updated_by,
            cron_dow.txn_id
    FROM core.cron_dow
    WHERE cron_dow.name = ^(name) AND core.cron_dow.is_active = 1;',
    'Select single CronDow record by RWK columns with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dow-get-history',
    'SELECT
        core.cron_dow.id,
            cron_dow.name,
            cron_dow.is_active,
            cron_dow.created_by,
            cron_dow.last_updated,
            cron_dow.last_updated_by,
            cron_dow.txn_id
    FROM core.cron_dow
    WHERE core.cron_dow.id = ^(id)
    ORDER BY core.cron_dow.txn_id DESC;',
    'Select all history records for CronDow by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        