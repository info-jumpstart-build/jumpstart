-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for AgentStatus (core.agent_status)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.agent_status-select',
    'SELECT
        core.agent_status.id,
            agent_status.name,
            agent_status.is_active,
            agent_status.created_by,
            agent_status.last_updated,
            agent_status.last_updated_by,
            agent_status.txn_id
    FROM core.agent_status
    WHERE core.agent_status.is_active = 1
    ORDER BY core.agent_status.id;',
    'Select all AgentStatus records with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.agent_status-get',
    'SELECT
        core.agent_status.id,
            agent_status.name,
            agent_status.is_active,
            agent_status.created_by,
            agent_status.last_updated,
            agent_status.last_updated_by,
            agent_status.txn_id
    FROM core.agent_status
    WHERE core.agent_status.id = ^(id) AND core.agent_status.is_active = 1;',
    'Select single AgentStatus record by ID with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.agent_status-get-by-rwk',
    'SELECT
        core.agent_status.id,
            agent_status.name,
            agent_status.is_active,
            agent_status.created_by,
            agent_status.last_updated,
            agent_status.last_updated_by,
            agent_status.txn_id
    FROM core.agent_status
    WHERE agent_status.name = ^(name) AND core.agent_status.is_active = 1;',
    'Select single AgentStatus record by RWK columns with related  information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.agent_status-get-history',
    'SELECT
        core.agent_status.id,
            agent_status.name,
            agent_status.is_active,
            agent_status.created_by,
            agent_status.last_updated,
            agent_status.last_updated_by,
            agent_status.txn_id
    FROM core.agent_status
    WHERE core.agent_status.id = ^(id)
    ORDER BY core.agent_status.txn_id DESC;',
    'Select all history records for AgentStatus by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        