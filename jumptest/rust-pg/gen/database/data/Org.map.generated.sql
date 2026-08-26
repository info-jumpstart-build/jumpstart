-- =====================================
-- Generate SELECT queries for many-to-many ("map") relationship checklists
-- =====================================


-- =====================================
-- Map-relationship queries for Org (core.org)
-- =====================================

        
-- Map relationship: Principal (principal), via core.principal_org
INSERT INTO core.sql (id, txn_id, name, sql_text, description, last_updated, last_updated_by, is_active, data_source_id)
SELECT n, n,
    'core.org-map-principal-principal',
    'SELECT
        principal.id AS id,
        CONCAT_WS('' '', principal.email, principal.principal_status_id) AS label,
        CASE WHEN principal_org.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
    FROM core.principal
    LEFT JOIN core.principal_org ON principal_org.principal_id = principal.id AND principal_org.org_id = ^(id) AND principal_org.is_active = 1
    WHERE principal.is_active = 1
    ORDER BY principal.id;',
    'Select Principal options for Org, flagged as mapped where a principal_org row already links them',
    NOW(),
    CURRENT_USER,
    1,
    (SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
FROM (SELECT nextval('core.sql_identity') n) t;

            