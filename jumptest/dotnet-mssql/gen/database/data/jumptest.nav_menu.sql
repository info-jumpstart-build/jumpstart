
-- Insert parent and child navigation menu items
DECLARE @nav_id BIGINT;

-- Insert parent menu item
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, 0, 0, 'Testing', '', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

-- Insert child menu items for Testing
    
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 0, 'Test Plan', '/usr/testplan', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 1, 'Test Case', '/testcase', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 2, 'Test Run', '/testrun', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 3, 'Test Result', '/testresult', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
-- Insert parent menu item
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, 0, 1, 'Admin', '', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

-- Insert child menu items for Admin
    
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 0, 'Organization', '/org', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 1, 'Principal', '/principal', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 2, 'Operations', '/operation', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 3, 'Operation Role', '/oprole', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
-- Insert parent menu item
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, 0, 2, 'System', '', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

-- Insert child menu items for System
    
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 0, 'Scripts', '/script', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 1, 'Events', '/eventservice', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 2, 'Execution Log', '/execlog', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 3, 'Process', '/process', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 4, 'Schedule', '/schedule', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 5, 'Workflow', '/core/workflow', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 6, 'Server Node', '/servernode', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 7, 'Query', '/sql', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        
SET @nav_id = NEXT VALUE FOR core.nav_menu_identity;
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
VALUES (@nav_id, @nav_id, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 8, 'Data Source', '/datasource', 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
        