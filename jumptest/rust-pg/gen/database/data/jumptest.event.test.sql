INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'all-object insert event',
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
}', 1, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'all-object update event',
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
}', 1, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test hello-world script',
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
}', 1, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test infinite loop script',
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
                Console.WriteLine($"Long loop: {i}");
                Thread.Sleep(1000);
            }
        }
    }
}', 1, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test failure script',
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
}', 1, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

-- PowerShell test scripts
INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test powershell hello-world script',
'Write-Host ''Hello, World from PowerShell!''', 2, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test powershell loop script',
'Write-Host ''Executing PowerShell loop script''
for ($i = 0; $i -lt 5; $i++) {
    Write-Host "  Iteration $i"
    Start-Sleep -Milliseconds 500
}', 2, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test powershell exception script',
'Write-Host ''About to throw an exception...''
throw ''Test exception from PowerShell script''', 2, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

-- Python test scripts
INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test python hello-world script',
'print(''Hello, World from Python!'')', 3, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test python loop script',
'print(''Executing Python loop script'')
import time
for i in range(5):
    print(f"  Iteration {i}")
    time.sleep(0.5)', 3, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;

INSERT INTO core.script(id, txn_id, name, source, script_type_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test python exception script',
'print(''About to throw an exception...'')
raise Exception(''Test exception from Python script'')', 3, 1, current_user, current_timestamp, current_user
FROM (SELECT nextval('core.script_identity') n) t;


-- Insert process records for all test scripts
INSERT INTO core.process(id, txn_id, name, script_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, s.name, s.id, 1, current_user, current_timestamp, current_user
FROM core.script s
CROSS JOIN LATERAL (SELECT nextval('core.process_identity') AS n) seq
WHERE s.name LIKE 'test%' AND s.is_active = 1;

-- Insert explicit process for hello-world script
INSERT INTO core.process(id, txn_id, name, script_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'test hello world process', s.id, 1, current_user, current_timestamp, current_user
FROM core.script s
CROSS JOIN LATERAL (SELECT nextval('core.process_identity') AS n) seq
WHERE s.name = 'test hello-world script' AND s.is_active = 1;


-- Insert workflow record for test workflow (type=Process, link to hello-world process)
INSERT INTO core.workflow(id, txn_id, workflow_type_id, parent_id, name, process_id, is_active, created_by, last_updated, last_updated_by, last_start_time, last_end_time)
SELECT n, n, 2, null, 'test workflow', p.id, 1, current_user, current_timestamp, current_user, '1900-01-01', '1900-01-01'
FROM core.process p
CROSS JOIN LATERAL (SELECT nextval('core.workflow_identity') AS n) seq
WHERE p.name = 'test hello world process' AND p.is_active = 1;



WITH all_object_insert_script AS (
    SELECT id FROM core."script" WHERE name = 'all-object insert event' AND is_active = 1
)
INSERT INTO core.event_service(id, txn_id, event_type, objectname_filter, methodname_filter, script_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'pre', '%', 'insert', all_object_insert_script.id, 1, current_user, current_timestamp, current_user
FROM all_object_insert_script
CROSS JOIN LATERAL (SELECT nextval('core.event_service_identity') AS n) seq;

WITH all_object_update_script AS (
    SELECT id FROM core."script" WHERE name = 'all-object update event' AND is_active = 1
)
INSERT INTO core.event_service(id, txn_id, event_type, objectname_filter, methodname_filter, script_id, is_active, created_by, last_updated, last_updated_by)
SELECT n, n, 'post', '%', 'update', all_object_update_script.id, 1, current_user, current_timestamp, current_user
FROM all_object_update_script
CROSS JOIN LATERAL (SELECT nextval('core.event_service_identity') AS n) seq;
