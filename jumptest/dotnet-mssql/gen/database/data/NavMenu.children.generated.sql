-- =====================================
-- Generate SELECT queries for child records
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Child queries for NavMenu (core.nav_menu)
-- =====================================

		
-- Child relationship: Parent Menu ID (parent)
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
	'core.nav_menu-children-navmenu-parent',
	'SELECT
		nav_menu.id,
        nav_menu.parent_id,
        nav_menu.ordinal,
        nav_menu.name,
        nav_menu.link,
        nav_menu.is_active,
        nav_menu.created_by,
        nav_menu.last_updated,
        nav_menu.last_updated_by,
        nav_menu.txn_id,
        parent.name AS parent_name
	FROM core.nav_menu
    LEFT JOIN core.nav_menu parent ON nav_menu.parent_id = parent.id AND parent.is_active = 1
	WHERE nav_menu.parent_id = ^(id) AND nav_menu.is_active = 1
	ORDER BY nav_menu.id;',
	'Select all Parent Menu ID records for NavMenu with related NavMenu information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			