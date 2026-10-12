-- =====================================
-- Generate SELECT queries for many-to-many ("map") relationship checklists
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Map-relationship queries for Principal (core.principal)
-- =====================================

		
-- Map relationship: Organization (org), via core.principal_org
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
	'core.principal-map-org-org',
	'SELECT
		org.id AS id,
		CONCAT_WS('' '', org.name) AS label,
		CASE WHEN principal_org.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
	FROM core.org
	LEFT JOIN core.principal_org ON principal_org.org_id = org.id AND principal_org.principal_id = ^(id) AND principal_org.is_active = 1
	WHERE org.is_active = 1
	ORDER BY org.id;',
	'Select Organization options for Principal, flagged as mapped where a principal_org row already links them',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			
-- Map relationship: Operation Role (op_role), via core.op_role_member
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
	'core.principal-map-oprole-op_role',
	'SELECT
		op_role.id AS id,
		CONCAT_WS('' '', op_role.name) AS label,
		CASE WHEN op_role_member.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
	FROM core.op_role
	LEFT JOIN core.op_role_member ON op_role_member.op_role_id = op_role.id AND op_role_member.principal_id = ^(id) AND op_role_member.is_active = 1
	WHERE op_role.is_active = 1
	ORDER BY op_role.id;',
	'Select Operation Role options for Principal, flagged as mapped where a op_role_member row already links them',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			