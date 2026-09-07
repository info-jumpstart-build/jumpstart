-- =====================================
-- 1) Create Account for Current OS Principal
-- admin_email is passed in from load.py via: psql -v admin_email=user@example.com
-- =====================================
\set os_user `echo $LOGNAME`

INSERT INTO core."principal"(id, txn_id, first_name, last_name, username, email, principal_status_id, created_date, last_login_date, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'Current', 'Principal', :'os_user', :'admin_email', 1, now(), now(), 1, 'system', now(), 'system'
FROM (SELECT nextval('core.principal_identity') n) t;

-- =====================================
-- 2) Create Account for OS Login Principal
--    This is the identity server components (ScriptAgent/Scheduler/ApiServer) will
--    likely run under. It's kept separate from the authentication principal above
--    (which is keyed by admin_email) since the OS login and the human admin's login
--    email are not necessarily the same account.
-- =====================================
\set service_email `echo $LOGNAME@localhost`

INSERT INTO core."principal"(id, txn_id, first_name, last_name, username, email, principal_status_id, created_date, last_login_date, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'Service', 'Account', :'os_user', :'service_email', 1, now(), now(), 1, 'system', now(), 'system'
FROM (SELECT nextval('core.principal_identity') n) t;

-- =====================================
-- 3) Create Admin Role
-- =====================================
INSERT INTO core.op_role(id, txn_id, name, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'Administrator', 1, 'system', now(), 'system'
FROM (SELECT nextval('core.op_role_identity') n) t;


-- =====================================
-- 4) Map os_user Principal to Administrator Role
--    Using sub-selects on core."principal" (by email)
--    and core.op_role (by name)
-- =====================================
WITH os_user_principal_id AS (
    SELECT id FROM core."principal" WHERE email = :'admin_email' AND is_active = 1
),
os_user_role_id AS (
    SELECT id FROM core.op_role WHERE name = 'Administrator' AND is_active = 1
)
INSERT INTO core.op_role_member(id, txn_id, principal_id, op_role_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, os_user_principal_id.id, os_user_role_id.id, 1, 'system', now(), 'system'
FROM os_user_principal_id
CROSS JOIN os_user_role_id
CROSS JOIN LATERAL (SELECT nextval('core.op_role_member_identity') AS n) seq;


-- =====================================
-- 5) Map OS Login Principal to Administrator Role
--    Using sub-selects on core."principal" (by email)
--    and core.op_role (by name)
-- =====================================
WITH service_principal_id AS (
    SELECT id FROM core."principal" WHERE email = :'service_email' AND is_active = 1
),
service_role_id AS (
    SELECT id FROM core.op_role WHERE name = 'Administrator' AND is_active = 1
)
INSERT INTO core.op_role_member(id, txn_id, principal_id, op_role_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, service_principal_id.id, service_role_id.id, 1, 'system', now(), 'system'
FROM service_principal_id
CROSS JOIN service_role_id
CROSS JOIN LATERAL (SELECT nextval('core.op_role_member_identity') AS n) seq;


-- 6) Grant All Operations to Admin Role
--    (Map every operation in core.operation to the admin role)
--    Using a sub-select on core.op_role to fetch the ID by "name"
-- =====================================
WITH admin_role_id AS (
    SELECT id FROM core.op_role WHERE name = 'Administrator' AND is_active = 1
)
INSERT INTO core.op_role_map(id, txn_id, op_id, op_role_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, op.id, admin_role_id.id, 1, 'system', now(), 'system'
FROM core.operation op
CROSS JOIN admin_role_id
CROSS JOIN LATERAL (SELECT nextval('core.op_role_map_identity') AS n) seq
WHERE op.is_active = 1;

-- =====================================
-- Done!
-- =====================================
