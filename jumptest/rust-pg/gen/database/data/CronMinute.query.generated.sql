-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronMinute (core.cron_minute)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_minute-select',
    'SELECT
        core.cron_minute.id,
            cron_minute.name,
            cron_minute.is_active,
            cron_minute.created_by,
            cron_minute.last_updated,
            cron_minute.last_updated_by,
            cron_minute.txn_id
    FROM core.cron_minute
    WHERE core.cron_minute.is_active = 1
    ORDER BY core.cron_minute.id;',
    'Select all CronMinute records with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_minute-get',
    'SELECT
        core.cron_minute.id,
            cron_minute.name,
            cron_minute.is_active,
            cron_minute.created_by,
            cron_minute.last_updated,
            cron_minute.last_updated_by,
            cron_minute.txn_id
    FROM core.cron_minute
    WHERE core.cron_minute.id = ^(id) AND core.cron_minute.is_active = 1;',
    'Select single CronMinute record by ID with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_minute-get-by-rwk',
    'SELECT
        core.cron_minute.id,
            cron_minute.name,
            cron_minute.is_active,
            cron_minute.created_by,
            cron_minute.last_updated,
            cron_minute.last_updated_by,
            cron_minute.txn_id
    FROM core.cron_minute
    WHERE cron_minute.name = ^(name) AND core.cron_minute.is_active = 1;',
    'Select single CronMinute record by RWK columns with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_minute-get-history',
    'SELECT
        core.cron_minute.id,
            cron_minute.name,
            cron_minute.is_active,
            cron_minute.created_by,
            cron_minute.last_updated,
            cron_minute.last_updated_by,
            cron_minute.txn_id
    FROM core.cron_minute
    WHERE core.cron_minute.id = ^(id)
    ORDER BY core.cron_minute.txn_id DESC;',
    'Select all history records for CronMinute by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        