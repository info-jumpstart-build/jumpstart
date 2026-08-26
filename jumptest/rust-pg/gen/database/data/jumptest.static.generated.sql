-- Insert default data source record
insert into core.data_source (id, txn_id, name, is_active, created_by, last_updated, last_updated_by ) SELECT n, n, 'default', 1, current_user, current_timestamp, current_user FROM (SELECT nextval('core.data_source_identity') n) t;

-- Insert server node type values
insert into core.server_node_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'ScriptAgent', 1, current_user, current_timestamp, current_user);
insert into core.server_node_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Scheduler', 1, current_user, current_timestamp, current_user);
insert into core.server_node_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, 'ApiServer', 1, current_user, current_timestamp, current_user);

-- Insert server node status values
insert into core.server_node_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'Initializing', 1, current_user, current_timestamp, current_user);
insert into core.server_node_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Online', 1, current_user, current_timestamp, current_user);
insert into core.server_node_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, 'Busy', 1, current_user, current_timestamp, current_user);
insert into core.server_node_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (4, 4, 'Offline', 1, current_user, current_timestamp, current_user);

-- Insert principal status values
insert into core.principal_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'Enabled', 1, current_user, current_timestamp, current_user);
insert into core.principal_status (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Disabled', 1, current_user, current_timestamp, current_user);
-- Insert schedule values (Manual = no automatic cron schedule; cron FK fields left NULL)
insert into core.schedule (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'Manual', 1, current_user, current_timestamp, current_user);

-- Insert Cron Minute values
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, '*', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, '0', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, '1', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (4, 4, '2', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (5, 5, '3', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (6, 6, '4', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (7, 7, '5', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (8, 8, '6', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (9, 9, '7', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (10, 10, '8', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (11, 11, '9', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (12, 12, '10', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (13, 13, '11', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (14, 14, '12', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (15, 15, '13', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (16, 16, '14', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (17, 17, '15', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (18, 18, '16', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (19, 19, '17', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (20, 20, '18', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (21, 21, '19', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (22, 22, '20', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (23, 23, '21', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (24, 24, '22', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (25, 25, '23', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (26, 26, '24', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (27, 27, '25', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (28, 28, '26', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (29, 29, '27', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (30, 30, '28', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (31, 31, '29', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (32, 32, '30', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (33, 33, '31', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (34, 34, '32', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (35, 35, '33', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (36, 36, '34', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (37, 37, '35', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (38, 38, '36', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (39, 39, '37', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (40, 40, '38', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (41, 41, '39', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (42, 42, '40', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (43, 43, '41', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (44, 44, '42', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (45, 45, '43', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (46, 46, '44', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (47, 47, '45', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (48, 48, '46', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (49, 49, '47', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (50, 50, '48', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (51, 51, '49', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (52, 52, '50', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (53, 53, '51', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (54, 54, '52', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (55, 55, '53', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (56, 56, '54', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (57, 57, '55', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (58, 58, '56', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (59, 59, '57', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (60, 60, '58', 1, current_user, current_timestamp, current_user);
insert into core.cron_minute (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (61, 61, '59', 1, current_user, current_timestamp, current_user);

-- Insert Cron Hour values
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, '*', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, '0', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, '1', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (4, 4, '2', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (5, 5, '3', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (6, 6, '4', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (7, 7, '5', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (8, 8, '6', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (9, 9, '7', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (10, 10, '8', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (11, 11, '9', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (12, 12, '10', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (13, 13, '11', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (14, 14, '12', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (15, 15, '13', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (16, 16, '14', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (17, 17, '15', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (18, 18, '16', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (19, 19, '17', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (20, 20, '18', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (21, 21, '19', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (22, 22, '20', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (23, 23, '21', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (24, 24, '22', 1, current_user, current_timestamp, current_user);
insert into core.cron_hour (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (25, 25, '23', 1, current_user, current_timestamp, current_user);

-- Insert Cron Dom values
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, '*', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, '?', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, '1', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (4, 4, '2', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (5, 5, '3', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (6, 6, '4', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (7, 7, '5', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (8, 8, '6', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (9, 9, '7', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (10, 10, '8', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (11, 11, '9', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (12, 12, '10', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (13, 13, '11', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (14, 14, '12', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (15, 15, '13', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (16, 16, '14', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (17, 17, '15', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (18, 18, '16', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (19, 19, '17', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (20, 20, '18', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (21, 21, '19', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (22, 22, '20', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (23, 23, '21', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (24, 24, '22', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (25, 25, '23', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (26, 26, '24', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (27, 27, '25', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (28, 28, '26', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (29, 29, '27', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (30, 30, '28', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (31, 31, '29', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (32, 32, '30', 1, current_user, current_timestamp, current_user);
insert into core.cron_dom (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (33, 33, '31', 1, current_user, current_timestamp, current_user);

-- Insert Cron Month values
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, '*', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, '1', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, '2', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (4, 4, '3', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (5, 5, '4', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (6, 6, '5', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (7, 7, '6', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (8, 8, '7', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (9, 9, '8', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (10, 10, '9', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (11, 11, '10', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (12, 12, '11', 1, current_user, current_timestamp, current_user);
insert into core.cron_month (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (13, 13, '12', 1, current_user, current_timestamp, current_user);

-- Insert Cron Dow values
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, '*', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, '?', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, '1', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (4, 4, '2', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (5, 5, '3', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (6, 6, '4', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (7, 7, '5', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (8, 8, '6', 1, current_user, current_timestamp, current_user);
insert into core.cron_dow (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (9, 9, '7', 1, current_user, current_timestamp, current_user);

-- Insert Cron Every values
insert into core.cron_every (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'Every', 1, current_user, current_timestamp, current_user);
insert into core.cron_every (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Exactly', 1, current_user, current_timestamp, current_user);

-- Insert workflow type values
insert into core.workflow_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'Folder', 1, current_user, current_timestamp, current_user);
insert into core.workflow_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Process', 1, current_user, current_timestamp, current_user);

-- Insert on_failure action values
insert into core.on_failure (id, txn_id, action, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'Stop', 1, current_user, current_timestamp, current_user);
insert into core.on_failure (id, txn_id, action, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Continue', 1, current_user, current_timestamp, current_user);


insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (1, 1, '', 'images/exec-status-1.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'Dispatched', 'images/exec-status-2.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (3, 3, 'Executing', 'images/exec-status-3.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (4, 4, 'Completed', 'images/exec-status-4.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (5, 5, 'Failed', 'images/exec-status-5.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (6, 6, 'Cancelled', 'images/exec-status-6.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (7, 7, 'Timeout', 'images/exec-status-7.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (8, 8, 'Interrupted', 'images/exec-status-8.png', 1, current_user, current_timestamp, current_user);
insert into core.exec_status (id, txn_id, name, image, is_active, created_by, last_updated, last_updated_by) values (9, 9, 'Suspended', 'images/exec-status-9.png', 1, current_user, current_timestamp, current_user);

-- Insert script type values
insert into core.script_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (1, 1, 'CSharp', 1, current_user, current_timestamp, current_user);
insert into core.script_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (2, 2, 'PowerShell', 1, current_user, current_timestamp, current_user);
insert into core.script_type (id, txn_id, name, is_active, created_by, last_updated, last_updated_by) values (3, 3, 'Python', 1, current_user, current_timestamp, current_user);

-- Insert AgentLogic method operations
-- Process Management Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'start', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'stop', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'kill', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'restart', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'pause', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'resume', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Status & Reporting Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'status', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'heartbeat', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'report', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'log', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'metrics', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Resource Management Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'register', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'unregister', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'capabilities', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'allocate', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'release', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Job Execution Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'execute', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'validate', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Communication Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'ping', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'acknowledge', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'notify', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'request', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- System Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'shutdown', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'reload', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'diagnose', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'health', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Security Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'authenticate', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'authorize', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Standard Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'getAgentInfo', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptAgent', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

-- Scheduler Operations
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Scheduler', 'execute', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Scheduler', 'validate', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Scheduler', 'configure', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Scheduler', 'health', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Scheduler', 'register', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Scheduler', 'unregister', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'run', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;



		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResultStatus', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestPlan', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Org', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Operation', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRole', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronEvery', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMinute', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronHour', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDom', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronMonth', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'CronDow', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'NavMenu', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'DataSource', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'AgentStatus', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OnFailure', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecStatus', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeStatus', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ScriptType', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNodeType', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'WorkflowType', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalStatus', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestCase', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestRun', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMap', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Schedule', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Sql', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Script', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ServerNode', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Principal', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'EventService', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Process', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'TestResult', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'PrincipalOrg', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'OpRoleMember', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'Workflow', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'select', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'get', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'view', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'insert', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'put', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'update', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'delete', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'history', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;
		insert into core.operation (id, txn_id, objectname, methodname, is_active, last_updated, created_by, last_updated_by) SELECT n, n, 'ExecLog', 'children', 1, current_timestamp, current_user, current_user FROM (SELECT nextval('core.operation_identity') n) t;

		