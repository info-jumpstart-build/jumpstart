-- =====================================
-- Generate SELECT queries for all objects
-- =====================================


-- =====================================
-- Query for OpRoleMember (core.op_role_member)
-- =====================================


INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_member-select',
    'SELECT
        core.op_role_member.id,
            op_role_member.principal_id,
            op_role_member.op_role_id,
            op_role_member.is_active,
            op_role_member.created_by,
            op_role_member.last_updated,
            op_role_member.last_updated_by,
            op_role_member.txn_id,
            principal.email AS principal_email,
            principal.principal_status_id AS principal_principal_status_id,
            op_role.name AS op_role_name
    FROM core.op_role_member
        LEFT JOIN core.principal principal ON op_role_member.principal_id = principal.id AND principal.is_active = 1
        LEFT JOIN core.op_role op_role ON op_role_member.op_role_id = op_role.id AND op_role.is_active = 1
    WHERE core.op_role_member.is_active = 1
    ORDER BY core.op_role_member.id;',
    'Select all OpRoleMember records with related Principal, OpRole information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_member-get',
    'SELECT
        core.op_role_member.id,
            op_role_member.principal_id,
            op_role_member.op_role_id,
            op_role_member.is_active,
            op_role_member.created_by,
            op_role_member.last_updated,
            op_role_member.last_updated_by,
            op_role_member.txn_id,
            principal.email AS principal_email,
            principal.principal_status_id AS principal_principal_status_id,
            op_role.name AS op_role_name
    FROM core.op_role_member
        LEFT JOIN core.principal principal ON op_role_member.principal_id = principal.id AND principal.is_active = 1
        LEFT JOIN core.op_role op_role ON op_role_member.op_role_id = op_role.id AND op_role.is_active = 1
    WHERE core.op_role_member.id = ^(id) AND core.op_role_member.is_active = 1;',
    'Select single OpRoleMember record by ID with related Principal, OpRole information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_member-get-by-rwk',
    'SELECT
        core.op_role_member.id,
            op_role_member.principal_id,
            op_role_member.op_role_id,
            op_role_member.is_active,
            op_role_member.created_by,
            op_role_member.last_updated,
            op_role_member.last_updated_by,
            op_role_member.txn_id,
            principal.email AS principal_email,
            principal.principal_status_id AS principal_principal_status_id,
            op_role.name AS op_role_name
    FROM core.op_role_member
        LEFT JOIN core.principal principal ON op_role_member.principal_id = principal.id AND principal.is_active = 1
        LEFT JOIN core.op_role op_role ON op_role_member.op_role_id = op_role.id AND op_role.is_active = 1
    WHERE op_role_member.principal_id = ^(principal_id) AND op_role_member.op_role_id = ^(op_role_id) AND core.op_role_member.is_active = 1;',
    'Select single OpRoleMember record by RWK columns with related Principal, OpRole information',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role_member-get-history',
    'SELECT
        core.op_role_member.*
    FROM core.op_role_member
    WHERE core.op_role_member.id = ^(id)
    ORDER BY core.op_role_member.txn_id DESC;',
    'Select all history records for OpRoleMember by id, ordered newest first',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;


        