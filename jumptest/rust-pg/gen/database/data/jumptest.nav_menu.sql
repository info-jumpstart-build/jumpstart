-- Insert parent and child navigation menu items

-- Insert parent menu item
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 0, 0, 'Testing', '', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;

-- Insert child menu items for Testing
    
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 0, 'Test Plan', '/usr/testplan', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 1, 'Test Case', '/testcase', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Testing' AND is_active = 1), 2, 'Test Run', '/testrun', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
-- Insert parent menu item
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 0, 1, 'Admin', '', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;

-- Insert child menu items for Admin
    
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 0, 'Organization', '/org', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 1, 'Principal', '/principal', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 2, 'Operations', '/operation', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'Admin' AND is_active = 1), 3, 'Role', '/oprole', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
-- Insert parent menu item
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 0, 2, 'System', '', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;

-- Insert child menu items for System
    
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 0, 'Scripts', '/script', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 1, 'Events', '/eventservice', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 2, 'Execution Log', '/execlog', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 3, 'Process', '/process', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 4, 'Schedule', '/schedule', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 5, 'Workflow', '/core/workflow', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 6, 'Server Node', '/servernode', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 7, 'Query', '/sql', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        
INSERT INTO core.nav_menu(id, txn_id, parent_id, ordinal, name, link, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, (SELECT id FROM core.nav_menu WHERE parent_id = 0 AND name = 'System' AND is_active = 1), 8, 'Data Source', '/datasource', 1, current_user, now(), current_user
FROM (SELECT nextval('core.nav_menu_identity') n) t;
        