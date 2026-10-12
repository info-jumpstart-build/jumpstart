-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for OpRoleMap (core.op_role_map)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_map-select',
    'SELECT
        core.op_role_map.id,
            op_role_map.op_id,
            op_role_map.op_role_id,
            op_role_map.is_active,
            op_role_map.created_by,
            op_role_map.last_updated,
            op_role_map.last_updated_by,
            op_role_map.txn_id,
            op.objectname AS op_objectname,
            op.methodname AS op_methodname,
            op_role.name AS op_role_name
    FROM core.op_role_map
        LEFT JOIN core.operation op ON op_role_map.op_id = op.id AND op.is_active = 1
        LEFT JOIN core.op_role op_role ON op_role_map.op_role_id = op_role.id AND op_role.is_active = 1
    WHERE core.op_role_map.is_active = 1
    ORDER BY core.op_role_map.id;',
    'Select all OpRoleMap records with related Operation, OpRole information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_map-get',
    'SELECT
        core.op_role_map.id,
            op_role_map.op_id,
            op_role_map.op_role_id,
            op_role_map.is_active,
            op_role_map.created_by,
            op_role_map.last_updated,
            op_role_map.last_updated_by,
            op_role_map.txn_id,
            op.objectname AS op_objectname,
            op.methodname AS op_methodname,
            op_role.name AS op_role_name
    FROM core.op_role_map
        LEFT JOIN core.operation op ON op_role_map.op_id = op.id AND op.is_active = 1
        LEFT JOIN core.op_role op_role ON op_role_map.op_role_id = op_role.id AND op_role.is_active = 1
    WHERE core.op_role_map.id = ^(id) AND core.op_role_map.is_active = 1;',
    'Select single OpRoleMap record by ID with related Operation, OpRole information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_map-get-by-rwk',
    'SELECT
        core.op_role_map.id,
            op_role_map.op_id,
            op_role_map.op_role_id,
            op_role_map.is_active,
            op_role_map.created_by,
            op_role_map.last_updated,
            op_role_map.last_updated_by,
            op_role_map.txn_id,
            op.objectname AS op_objectname,
            op.methodname AS op_methodname,
            op_role.name AS op_role_name
    FROM core.op_role_map
        LEFT JOIN core.operation op ON op_role_map.op_id = op.id AND op.is_active = 1
        LEFT JOIN core.op_role op_role ON op_role_map.op_role_id = op_role.id AND op_role.is_active = 1
    WHERE op_role_map.op_id = ^(op_id) AND op_role_map.op_role_id = ^(op_role_id) AND core.op_role_map.is_active = 1;',
    'Select single OpRoleMap record by RWK columns with related Operation, OpRole information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_map-get-history',
    'SELECT
        core.op_role_map.id,
            op_role_map.op_id,
            op_role_map.op_role_id,
            op_role_map.is_active,
            op_role_map.created_by,
            op_role_map.last_updated,
            op_role_map.last_updated_by,
            op_role_map.txn_id
    FROM core.op_role_map
    WHERE core.op_role_map.id = ^(id)
    ORDER BY core.op_role_map.txn_id DESC;',
    'Select all history records for OpRoleMap by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        