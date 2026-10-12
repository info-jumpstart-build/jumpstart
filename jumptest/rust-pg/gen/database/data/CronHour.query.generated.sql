-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronHour (core.cron_hour)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_hour-select',
    'SELECT
        core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
    FROM core.cron_hour
    WHERE core.cron_hour.is_active = 1
    ORDER BY core.cron_hour.id;',
    'Select all CronHour records with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_hour-get',
    'SELECT
        core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
    FROM core.cron_hour
    WHERE core.cron_hour.id = ^(id) AND core.cron_hour.is_active = 1;',
    'Select single CronHour record by ID with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_hour-get-by-rwk',
    'SELECT
        core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
    FROM core.cron_hour
    WHERE cron_hour.name = ^(name) AND core.cron_hour.is_active = 1;',
    'Select single CronHour record by RWK columns with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_hour-get-history',
    'SELECT
        core.cron_hour.id,
            cron_hour.name,
            cron_hour.is_active,
            cron_hour.created_by,
            cron_hour.last_updated,
            cron_hour.last_updated_by,
            cron_hour.txn_id
    FROM core.cron_hour
    WHERE core.cron_hour.id = ^(id)
    ORDER BY core.cron_hour.txn_id DESC;',
    'Select all history records for CronHour by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        