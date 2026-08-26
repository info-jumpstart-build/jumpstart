#!/usr/bin/env python3
"""
PostgreSQL Database Build Script
Reads configuration from .jumptest.json or .jumptest file
"""

import json
import os
import sys
import subprocess
from pathlib import Path

def load_config():
    """Load database configuration from JSON or legacy file"""
    home_dir = Path.home()
    json_file = home_dir / ".jumptest.json"
    legacy_file = home_dir / ".jumptest"
    
    # Default values
    config = {
        'hostname': 'localhost',
        'port': '5432',
        'database': 'jumptest',
        'username': 'postgres',
        'password': '',
        'dbtype': 'postgresql'
    }
    
    # Try JSON format first
    if json_file.exists():
        print(f"Reading configuration from: {json_file}")
        try:
            with open(json_file, 'r') as f:
                data = json.load(f)
                
            # Get the default datasource
            if 'datasources' in data and 'default' in data['datasources']:
                ds = data['datasources']['default']
                config.update({
                    'hostname': ds.get('hostname', config['hostname']),
                    'port': ds.get('port', config['port']),
                    'database': ds.get('database', config['database']),
                    'username': ds.get('username', config['username']),
                    'password': ds.get('password', config['password']),
                    'dbtype': ds.get('dbtype', config['dbtype'])
                })
        except json.JSONDecodeError as e:
            print(f"Error parsing JSON file: {e}", file=sys.stderr)
            sys.exit(1)
    
    # Fall back to legacy format
    elif legacy_file.exists():
        print(f"Reading configuration from legacy file: {legacy_file}")
        try:
            with open(legacy_file, 'r') as f:
                content = f.read().strip()
            
            parts = content.split(':')
            if len(parts) < 4:
                print(f"Invalid legacy file format. Expected: dbtype:server:port:database[:username:password]", file=sys.stderr)
                sys.exit(1)
            
            config['dbtype'] = parts[0].lower().strip()
            config['hostname'] = parts[1].strip()
            config['port'] = parts[2].strip()
            config['database'] = parts[3].strip()
            
            if len(parts) >= 5:
                config['username'] = parts[4].strip()
            if len(parts) >= 6:
                config['password'] = parts[5].strip()
                
        except Exception as e:
            print(f"Error reading legacy file: {e}", file=sys.stderr)
            sys.exit(1)
    else:
        print(f"Configuration file not found. Looking for:", file=sys.stderr)
        print(f"  - {json_file}", file=sys.stderr)
        print(f"  - {legacy_file}", file=sys.stderr)
        sys.exit(1)
    
    return config

def run_psql(config, database, sql_file):
    """Execute a SQL file using psql"""
    cmd = [
        'psql',
        f"--host={config['hostname']}",
        f"--port={config['port']}",
        f"--dbname={database}",
        f"--username={config['username']}",
        f"--file={sql_file}"
    ]
    
    env = os.environ.copy()
    if config['password']:
        env['PGPASSWORD'] = config['password']
    
    print(f"Executing: {sql_file}")
    result = subprocess.run(cmd, env=env)
    
    if result.returncode != 0:
        print(f"Failed to execute: {sql_file}", file=sys.stderr)
        sys.exit(result.returncode)

def main():
    """Main build script"""
    # Load configuration
    config = load_config()
    
    print(f"Connecting to PostgreSQL: {config['hostname']}:{config['port']}")
    print(f"Database: {config['database']}")
    print(f"Username: {config['username']}")
    
    # Execute database creation script
    print("\nExecuting database creation script...")
    run_psql(config, 'postgres', './jumptest.database.create.generated.sql')
    
    # Execute all other SQL files
    sql_files = [
        './app.schema.create.generated.sql',
        './core.schema.create.generated.sql',
        './TestResultStatus.table.generated.sql',
        './TestResultStatus.sequence.generated.sql',
        './TestResultStatus.rwkindex.generated.sql',
        './TestPlan.table.generated.sql',
        './TestPlan.sequence.generated.sql',
        './TestPlan.rwkindex.generated.sql',
        './Org.table.generated.sql',
        './Org.sequence.generated.sql',
        './Org.rwkindex.generated.sql',
        './Operation.table.generated.sql',
        './Operation.sequence.generated.sql',
        './Operation.rwkindex.generated.sql',
        './OpRole.table.generated.sql',
        './OpRole.sequence.generated.sql',
        './OpRole.rwkindex.generated.sql',
        './CronEvery.table.generated.sql',
        './CronEvery.sequence.generated.sql',
        './CronEvery.rwkindex.generated.sql',
        './CronMinute.table.generated.sql',
        './CronMinute.sequence.generated.sql',
        './CronMinute.rwkindex.generated.sql',
        './CronHour.table.generated.sql',
        './CronHour.sequence.generated.sql',
        './CronHour.rwkindex.generated.sql',
        './CronDom.table.generated.sql',
        './CronDom.sequence.generated.sql',
        './CronDom.rwkindex.generated.sql',
        './CronMonth.table.generated.sql',
        './CronMonth.sequence.generated.sql',
        './CronMonth.rwkindex.generated.sql',
        './CronDow.table.generated.sql',
        './CronDow.sequence.generated.sql',
        './CronDow.rwkindex.generated.sql',
        './NavMenu.table.generated.sql',
        './NavMenu.sequence.generated.sql',
        './NavMenu.rwkindex.generated.sql',
        './DataSource.table.generated.sql',
        './DataSource.sequence.generated.sql',
        './DataSource.rwkindex.generated.sql',
        './AgentStatus.table.generated.sql',
        './AgentStatus.sequence.generated.sql',
        './AgentStatus.rwkindex.generated.sql',
        './OnFailure.table.generated.sql',
        './OnFailure.sequence.generated.sql',
        './OnFailure.rwkindex.generated.sql',
        './ExecStatus.table.generated.sql',
        './ExecStatus.sequence.generated.sql',
        './ExecStatus.rwkindex.generated.sql',
        './ServerNodeStatus.table.generated.sql',
        './ServerNodeStatus.sequence.generated.sql',
        './ServerNodeStatus.rwkindex.generated.sql',
        './ScriptType.table.generated.sql',
        './ScriptType.sequence.generated.sql',
        './ScriptType.rwkindex.generated.sql',
        './ServerNodeType.table.generated.sql',
        './ServerNodeType.sequence.generated.sql',
        './ServerNodeType.rwkindex.generated.sql',
        './WorkflowType.table.generated.sql',
        './WorkflowType.sequence.generated.sql',
        './WorkflowType.rwkindex.generated.sql',
        './PrincipalStatus.table.generated.sql',
        './PrincipalStatus.sequence.generated.sql',
        './PrincipalStatus.rwkindex.generated.sql',
        './TestCase.table.generated.sql',
        './TestCase.sequence.generated.sql',
        './TestCase.rwkindex.generated.sql',
        './TestRun.table.generated.sql',
        './TestRun.sequence.generated.sql',
        './TestRun.rwkindex.generated.sql',
        './OpRoleMap.table.generated.sql',
        './OpRoleMap.sequence.generated.sql',
        './OpRoleMap.rwkindex.generated.sql',
        './Schedule.table.generated.sql',
        './Schedule.sequence.generated.sql',
        './Schedule.rwkindex.generated.sql',
        './Sql.table.generated.sql',
        './Sql.sequence.generated.sql',
        './Sql.rwkindex.generated.sql',
        './Script.table.generated.sql',
        './Script.sequence.generated.sql',
        './Script.rwkindex.generated.sql',
        './ServerNode.table.generated.sql',
        './ServerNode.sequence.generated.sql',
        './ServerNode.rwkindex.generated.sql',
        './Principal.table.generated.sql',
        './Principal.sequence.generated.sql',
        './Principal.rwkindex.generated.sql',
        './EventService.table.generated.sql',
        './EventService.sequence.generated.sql',
        './EventService.rwkindex.generated.sql',
        './Process.table.generated.sql',
        './Process.sequence.generated.sql',
        './Process.rwkindex.generated.sql',
        './TestResult.table.generated.sql',
        './TestResult.sequence.generated.sql',
        './TestResult.rwkindex.generated.sql',
        './PrincipalOrg.table.generated.sql',
        './PrincipalOrg.sequence.generated.sql',
        './PrincipalOrg.rwkindex.generated.sql',
        './OpRoleMember.table.generated.sql',
        './OpRoleMember.sequence.generated.sql',
        './OpRoleMember.rwkindex.generated.sql',
        './Workflow.table.generated.sql',
        './Workflow.sequence.generated.sql',
        './Workflow.rwkindex.generated.sql',
        './ExecLog.table.generated.sql',
        './ExecLog.sequence.generated.sql',
        './ExecLog.rwkindex.generated.sql',
    ]
    
    for sql_file in sql_files:
        run_psql(config, config['database'], sql_file)
    
    print("\nDatabase build completed successfully!")

if __name__ == '__main__':
    main()