-- Insert test scripts
DECLARE @script_id BIGINT;

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'all-object insert event',
'using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class AllObjectInsertEventScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine("all-object insert event");
        }
    }
}', 1, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'all-object update event',
'using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class AllObjectUpdateEventScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine("all-object update event");
        }
    }
}', 1, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test hello-world script',
'using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class HelloWorldScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}', 1, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test infinite loop script',
'using System;
using System.Threading;
using System.IO;
using jumptest;

namespace Generated
{
    public class InfiniteLoopScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("Long loop: " + i);
                Thread.Sleep(1000);
            }
        }
    }
}', 1, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test failure script',
'using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class FailureScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            throw new Exception("Test failure");
        }
    }
}', 1, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

-- PowerShell test scripts
SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test powershell hello-world script',
'Write-Host ''Hello, World from PowerShell!''', 2, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test powershell loop script',
'Write-Host ''Executing PowerShell loop script''
for ($i = 0; $i -lt 5; $i++) {
    Write-Host "  Iteration $i"
    Start-Sleep -Milliseconds 500
}', 2, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test powershell exception script',
'Write-Host ''About to throw an exception...''
throw ''Test exception from PowerShell script''', 2, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

-- Python test scripts
SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test python hello-world script',
'print(''Hello, World from Python!'')', 3, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test python loop script',
'print(''Executing Python loop script'')
import time
for i in range(5):
    print(f"  Iteration {i}")
    time.sleep(0.5)', 3, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

SET @script_id = NEXT VALUE FOR core.script_identity;
INSERT INTO [core].[script](id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
VALUES (@script_id, @script_id, 'test python exception script',
'print(''About to throw an exception...'')
raise Exception(''Test exception from Python script'')', 3, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);

-- Insert process records for all test scripts
-- NEXT VALUE FOR cannot be used inside a derived table/CROSS APPLY, so a contiguous
-- block of ids is reserved up front via sp_sequence_get_range and assigned one per row
-- (via ROW_NUMBER) -- this keeps id = txn_id for every inserted row.
DECLARE @process_count INT, @process_range_first BIGINT = 0;
SELECT @process_count = COUNT(*) FROM [core].[script] WHERE name LIKE 'test%' AND is_active = 1;
IF @process_count > 0
BEGIN
    DECLARE @process_range_variant SQL_VARIANT;
    EXEC sys.sp_sequence_get_range @sequence_name = N'core.process_identity', @range_size = @process_count, @range_first_value = @process_range_variant OUTPUT;
    SET @process_range_first = CAST(@process_range_variant AS BIGINT);
END
INSERT INTO [core].[process](id, txn_id, name, script_id, is_active, created_by, last_updated, last_updated_by)
SELECT @process_range_first + ROW_NUMBER() OVER (ORDER BY s.id) - 1,
       @process_range_first + ROW_NUMBER() OVER (ORDER BY s.id) - 1,
       s.name, s.id, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER
FROM [core].[script] s
WHERE s.name LIKE 'test%' AND s.is_active = 1;

-- Insert workflow record for test workflow (type=Process, link to hello-world process)
DECLARE @workflow_id BIGINT;
SELECT @workflow_id = NEXT VALUE FOR core.workflow_identity;
INSERT INTO [core].[workflow](id, txn_id, workflow_type_id, parent_id, name, process_id, is_active, created_by, last_updated, last_updated_by, last_start_time, last_end_time)
SELECT @workflow_id, @workflow_id, 2, null, 'test workflow', p.id, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER, '1900-01-01', '1900-01-01'
FROM [core].[process] p
WHERE p.name = 'test hello world process' AND p.is_active = 1;

-- Insert event services
DECLARE @all_object_insert_script_id BIGINT = (SELECT id FROM [core].[script] WHERE name = 'all-object insert event' AND is_active = 1);
DECLARE @all_object_update_script_id BIGINT = (SELECT id FROM [core].[script] WHERE name = 'all-object update event' AND is_active = 1);
DECLARE @event_service_id BIGINT;

IF @all_object_insert_script_id IS NOT NULL
BEGIN
    SET @event_service_id = NEXT VALUE FOR core.event_service_identity;
    INSERT INTO [core].[event_service](id, txn_id, event_type, objectname_filter, methodname_filter, script_id, is_active, created_by, last_updated, last_updated_by)
    VALUES (@event_service_id, @event_service_id, 'pre', '%', 'insert', @all_object_insert_script_id, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
END

IF @all_object_update_script_id IS NOT NULL
BEGIN
    SET @event_service_id = NEXT VALUE FOR core.event_service_identity;
    INSERT INTO [core].[event_service](id, txn_id, event_type, objectname_filter, methodname_filter, script_id, is_active, created_by, last_updated, last_updated_by)
    VALUES (@event_service_id, @event_service_id, 'post', '%', 'update', @all_object_update_script_id, 1, SYSTEM_USER, GETDATE(), SYSTEM_USER);
END
