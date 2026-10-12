-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for CronDom (core.cron_dom)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dom-select',
    'SELECT
        core.cron_dom.id,
            cron_dom.name,
            cron_dom.is_active,
            cron_dom.created_by,
            cron_dom.last_updated,
            cron_dom.last_updated_by,
            cron_dom.txn_id
    FROM core.cron_dom
    WHERE core.cron_dom.is_active = 1
    ORDER BY core.cron_dom.id;',
    'Select all CronDom records with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dom-get',
    'SELECT
        core.cron_dom.id,
            cron_dom.name,
            cron_dom.is_active,
            cron_dom.created_by,
            cron_dom.last_updated,
            cron_dom.last_updated_by,
            cron_dom.txn_id
    FROM core.cron_dom
    WHERE core.cron_dom.id = ^(id) AND core.cron_dom.is_active = 1;',
    'Select single CronDom record by ID with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dom-get-by-rwk',
    'SELECT
        core.cron_dom.id,
            cron_dom.name,
            cron_dom.is_active,
            cron_dom.created_by,
            cron_dom.last_updated,
            cron_dom.last_updated_by,
            cron_dom.txn_id
    FROM core.cron_dom
    WHERE cron_dom.name = ^(name) AND core.cron_dom.is_active = 1;',
    'Select single CronDom record by RWK columns with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.cron_dom-get-history',
    'SELECT
        core.cron_dom.id,
            cron_dom.name,
            cron_dom.is_active,
            cron_dom.created_by,
            cron_dom.last_updated,
            cron_dom.last_updated_by,
            cron_dom.txn_id
    FROM core.cron_dom
    WHERE core.cron_dom.id = ^(id)
    ORDER BY core.cron_dom.txn_id DESC;',
    'Select all history records for CronDom by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        