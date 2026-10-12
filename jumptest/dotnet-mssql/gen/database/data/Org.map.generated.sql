-- =====================================
-- Generate SELECT queries for many-to-many ("map") relationship checklists
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Map-relationship queries for Org (core.org)
-- =====================================

		
-- Map relationship: Principal (principal), via core.principal_org
SET @sql_id = NEXT VALUE FOR core.sql_identity;
INSERT INTO core.sql (
	id,
	txn_id,
	name,
	sql_text,
	description,
	created_by,
	last_updated,
	last_updated_by,
	is_active,
	data_source_id
) VALUES (
	@sql_id,
	@sql_id,
	'core.org-map-principal-principal',
	'SELECT
		principal.id AS id,
<<<<<<< HEAD
		CONCAT_WS('' '', principal.email, principal.enabled) AS label,
=======
		CONCAT_WS('' '', principal.email, principal.principal_status_id) AS label,
>>>>>>> main
		CASE WHEN principal_org.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
	FROM core.principal
	LEFT JOIN core.principal_org ON principal_org.principal_id = principal.id AND principal_org.org_id = ^(id) AND principal_org.is_active = 1
	WHERE principal.is_active = 1
	ORDER BY principal.id;',
	'Select Principal options for Org, flagged as mapped where a principal_org row already links them',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			