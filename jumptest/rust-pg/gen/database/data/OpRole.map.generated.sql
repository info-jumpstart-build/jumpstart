-- =====================================
-- Generate SELECT queries for many-to-many ("map") relationship checklists
-- =====================================


-- =====================================
-- Map-relationship queries for OpRole (core.op_role)
-- =====================================

        
-- Map relationship: Operations (op), via core.op_role_map
INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role-map-operation-op',
    'SELECT
        operation.id AS id,
        CONCAT_WS('' '', operation.objectname, operation.methodname) AS label,
        CASE WHEN op_role_map.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
    FROM core.operation
    LEFT JOIN core.op_role_map ON op_role_map.op_id = operation.id AND op_role_map.op_role_id = ^(id) AND op_role_map.is_active = 1
    WHERE operation.is_active = 1
    ORDER BY operation.id;',
    'Select Operations options for OpRole, flagged as mapped where a op_role_map row already links them',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

            
-- Map relationship: Principal (principal), via core.op_role_member
INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.op_role-map-principal-principal',
    'SELECT
        principal.id AS id,
        CONCAT_WS('' '', principal.email, principal.principal_status_id) AS label,
        CASE WHEN op_role_member.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
    FROM core.principal
    LEFT JOIN core.op_role_member ON op_role_member.principal_id = principal.id AND op_role_member.op_role_id = ^(id) AND op_role_member.is_active = 1
    WHERE principal.is_active = 1
    ORDER BY principal.id;',
    'Select Principal options for OpRole, flagged as mapped where a op_role_member row already links them',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

            