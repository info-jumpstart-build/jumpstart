-- =====================================
-- 1) Create Account for Current OS Principal
-- admin_email is passed in from load.ps1 via: sqlcmd -v admin_email=user@example.com
-- =====================================
DECLARE @admin_email NVARCHAR(200) = '$(admin_email)';
DECLARE @os_user NVARCHAR(50) =
    CASE
        WHEN CHARINDEX('\', SYSTEM_USER) > 0
        THEN RIGHT(SYSTEM_USER, LEN(SYSTEM_USER) - CHARINDEX('\', SYSTEM_USER))
        ELSE SYSTEM_USER
    END;
-- os_login is passed in from load.ps1 via: sqlcmd -v os_login=$env:USERNAME
-- (SYSTEM_USER above reflects the *SQL login* used for this connection, which under
-- SQL authentication is not the same as the Windows account the server components
-- actually run as -- os_login is the real Windows/OS user, sourced from PowerShell.)
DECLARE @os_login NVARCHAR(50) = '$(os_login)';
DECLARE @principal_id BIGINT, @service_principal_id BIGINT, @role_id BIGINT, @role_member_id BIGINT;

SELECT @principal_id = NEXT VALUE FOR core.principal_identity;
INSERT INTO core.principal(
    id,
    txn_id,
    first_name,
    last_name,
    username,
    email,
    principal_status_id,
    created_date,
    last_login_date,
    is_active,
    created_by,
    last_updated,
    last_updated_by
)
VALUES (
    @principal_id,
    @principal_id,
    'Current',
    'Principal',
    @os_user,
    @admin_email,
    1,
    GETDATE(),
    null,
    1,
    'system',
    GETDATE(),
    'system'
);

-- =====================================
-- 2) Create Account for OS Login Principal
--    This is the identity server components (ScriptAgent/Scheduler/ApiServer) will
--    likely run under. It's kept separate from the authentication principal above
--    (which is keyed by admin_email) since the OS login and the human admin's login
--    email are not necessarily the same account.
-- =====================================
SELECT @service_principal_id = NEXT VALUE FOR core.principal_identity;
INSERT INTO core.principal(
    id,
    txn_id,
    first_name,
    last_name,
    username,
    email,
    principal_status_id,
    created_date,
    last_login_date,
    is_active,
    created_by,
    last_updated,
    last_updated_by
)
VALUES (
    @service_principal_id,
    @service_principal_id,
    'Service',
    'Account',
    @os_login,
    @os_login + '@localhost',
    1,
    GETDATE(),
    null,
    1,
    'system',
    GETDATE(),
    'system'
);

-- =====================================
-- 3) Create Admin Role
-- =====================================
SELECT @role_id = NEXT VALUE FOR core.op_role_identity;
INSERT INTO core.op_role(
    id,
    txn_id,
    name,
    is_active,
    created_by,
    last_updated,
    last_updated_by
)
VALUES (
    @role_id,
    @role_id,
    'Administrator',
    1,
    'system',
    GETDATE(),
    'system'
);

-- =====================================
-- 4) Map os_user Principal to Administrator Role
--    Using sub-selects on core.principal (by email)
--    and core.op_role (by name)
-- =====================================
SELECT @role_member_id = NEXT VALUE FOR core.op_role_member_identity;

WITH os_user_principal_id AS (
    SELECT id FROM core.principal WHERE email = @admin_email AND is_active = 1
),
os_user_role_id AS (
    SELECT id FROM core.op_role WHERE name = 'Administrator' AND is_active = 1
)
INSERT INTO core.op_role_member(
    id,
    txn_id,
    principal_id,
    op_role_id,
    is_active,
    created_by,
    last_updated,
    last_updated_by
)
SELECT
    @role_member_id,
    @role_member_id,
    os_user_principal_id.id,
    os_user_role_id.id,
    1,
    'system',
    GETDATE(),
    'system'
FROM os_user_principal_id
CROSS JOIN os_user_role_id;

-- =====================================
-- 5) Map OS Login Principal to Administrator Role
-- =====================================
SELECT @role_member_id = NEXT VALUE FOR core.op_role_member_identity;
INSERT INTO core.op_role_member(
    id,
    txn_id,
    principal_id,
    op_role_id,
    is_active,
    created_by,
    last_updated,
    last_updated_by
)
VALUES (
    @role_member_id,
    @role_member_id,
    @service_principal_id,
    @role_id,
    1,
    'system',
    GETDATE(),
    'system'
);

-- =====================================
-- 6) Grant All Operations to Admin Role
--    (Map every operation in core.operation to the admin role)
--    NEXT VALUE FOR cannot be used inside a derived table/CROSS APPLY, so a contiguous
--    block of ids is reserved up front via sp_sequence_get_range and assigned one per
--    row (via ROW_NUMBER) -- this keeps id = txn_id for every inserted row.
-- =====================================
DECLARE @admin_role_id BIGINT;
SELECT @admin_role_id = id FROM core.op_role WHERE name = 'Administrator' AND is_active = 1;

DECLARE @op_count INT, @op_range_first BIGINT = 0;
SELECT @op_count = COUNT(*) FROM core.operation WHERE is_active = 1;
IF @op_count > 0
BEGIN
    DECLARE @op_range_variant SQL_VARIANT;
    EXEC sys.sp_sequence_get_range @sequence_name = N'core.op_role_map_identity', @range_size = @op_count, @range_first_value = @op_range_variant OUTPUT;
    SET @op_range_first = CAST(@op_range_variant AS BIGINT);
END

INSERT INTO core.op_role_map(
    id,
    txn_id,
    op_id,
    op_role_id,
    is_active,
    created_by,
    last_updated,
    last_updated_by
)
SELECT @op_range_first + ROW_NUMBER() OVER (ORDER BY op.id) - 1,
       @op_range_first + ROW_NUMBER() OVER (ORDER BY op.id) - 1,
       op.id, @admin_role_id, 1, 'system', GETDATE(), 'system'
FROM core.operation op
WHERE op.is_active = 1;

-- =====================================
-- Done!
-- =====================================
