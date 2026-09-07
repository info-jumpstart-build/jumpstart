-- =====================================
-- Generate SELECT queries for child records
-- =====================================
DECLARE @sql_id BIGINT;



-- =====================================
-- Child queries for Workflow (core.workflow)
-- =====================================

		
-- Child relationship: Process (workflow)
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
	'core.workflow-children-execlog-workflow',
	'SELECT
		exec_log.id,
        exec_log.token,
        exec_log.workflow_id,
        exec_log.start_time,
        exec_log.end_time,
        exec_log.exec_status_id,
        exec_log.stdout,
        exec_log.stderr,
        exec_log.is_active,
        exec_log.created_by,
        exec_log.last_updated,
        exec_log.last_updated_by,
        exec_log.txn_id,
        workflow.parent_id AS workflow_parent_id,
        workflow.name AS workflow_name
	FROM core.exec_log
    LEFT JOIN core.workflow workflow ON exec_log.workflow_id = workflow.id AND workflow.is_active = 1
	WHERE exec_log.workflow_id = ^(id) AND exec_log.is_active = 1
	ORDER BY exec_log.id;',
	'Select all Process records for Workflow with related Workflow information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			
-- Child relationship: Parent (parent)
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
	'core.workflow-children-workflow-parent',
	'SELECT
		workflow.id,
        workflow.workflow_type_id,
        workflow.parent_id,
        workflow.name,
        workflow.seq,
        workflow.server_node_id,
        workflow.process_id,
        workflow.exec_status_id,
        workflow.last_start_time,
        workflow.last_end_time,
        workflow.schedule_id,
        workflow.on_failure_action_id,
        workflow.is_active,
        workflow.created_by,
        workflow.last_updated,
        workflow.last_updated_by,
        workflow.txn_id,
        workflow_type.name AS workflow_type_name,
        parent.parent_id AS parent_parent_id,
        parent.name AS parent_name,
        server_node.hostname AS server_node_hostname,
        server_node.port AS server_node_port,
        process.name AS process_name,
        exec_status.name AS exec_status_name,
        exec_status.image AS exec_status_image,
        schedule.name AS schedule_name,
        on_failure_action.action AS on_failure_action_action
	FROM core.workflow
    LEFT JOIN core.workflow_type workflow_type ON workflow.workflow_type_id = workflow_type.id AND workflow_type.is_active = 1
    LEFT JOIN core.workflow parent ON workflow.parent_id = parent.id AND parent.is_active = 1
    LEFT JOIN core.server_node server_node ON workflow.server_node_id = server_node.id AND server_node.is_active = 1
    LEFT JOIN core.process process ON workflow.process_id = process.id AND process.is_active = 1
    LEFT JOIN core.exec_status exec_status ON workflow.exec_status_id = exec_status.id AND exec_status.is_active = 1
    LEFT JOIN core.schedule schedule ON workflow.schedule_id = schedule.id AND schedule.is_active = 1
    LEFT JOIN core.on_failure on_failure_action ON workflow.on_failure_action_id = on_failure_action.id AND on_failure_action.is_active = 1
	WHERE workflow.parent_id = ^(id) AND workflow.is_active = 1
	ORDER BY workflow.id;',
	'Select all Parent records for Workflow with related WorkflowType, Workflow, ServerNode, Process, ExecStatus, Schedule, OnFailure information',
	SYSTEM_USER,
	GETDATE(),
	SYSTEM_USER,
	1,
	(SELECT id FROM core.data_source WHERE name = 'default' AND is_active = 1)
);

			