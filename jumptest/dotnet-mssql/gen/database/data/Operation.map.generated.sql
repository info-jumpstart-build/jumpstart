-- =====================================
-- Generate SELECT queries for many-to-many ("map") relationship checklists
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Map-relationship queries for Operation (core.operation)
-- =====================================

		
-- Map relationship: Operation Role (op_role), via core.op_role_map
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
	'core.operation-map-oprole-op_role',
	'SELECT
		op_role.id AS id,
		CONCAT_WS('' '', op_role.name) AS label,
		CASE WHEN op_role_map.id IS NOT NULL THEN 1 ELSE 0 END AS mapped
	FROM core.op_role
	LEFT JOIN core.op_role_map ON op_role_map.op_role_id = op_role.id AND op_role_map.op_id = ^(id) AND op_role_map.is_active = 1
	WHERE op_role.is_active = 1
	ORDER BY op_role.id;',
	'Select Operation Role options for Operation, flagged as mapped where a op_role_map row already links them',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			