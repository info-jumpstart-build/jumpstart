#!/usr/bin/env python3
"""
PostgreSQL Data Load Script
Reads configuration from .jumptest.json or .jumptest file
Loads data files into the database
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

def run_psql(config, sql_file, variables=None):
    """Execute a SQL file using psql"""
    cmd = [
        'psql',
        f"--host={config['hostname']}",
        f"--port={config['port']}",
        f"--dbname={config['database']}",
        f"--username={config['username']}",
    ]

    for key, value in (variables or {}).items():
        cmd += ['--variable', f"{key}={value}"]

    cmd.append(f"--file={sql_file}")

    env = os.environ.copy()
    if config['password']:
        env['PGPASSWORD'] = config['password']

    print(f"Loading: {sql_file}")
    result = subprocess.run(cmd, env=env)

    if result.returncode != 0:
        print(f"Failed to load: {sql_file}", file=sys.stderr)
        sys.exit(result.returncode)

def prompt_admin_email():
    """Prompt for the initial admin user's email address"""
    print()
    print("-" * 60)
    print("Initial Admin User Setup")
    print("-" * 60)
    print("Enter the email address for the first administrator account.")
    print("This must match the email used to log in via Auth0.")
    print()
    while True:
        email = input("Admin email address: ").strip()
        if not email:
            print("Email address cannot be empty. Please try again.")
            continue
        if '@' not in email or '.' not in email.split('@')[-1]:
            print("That does not look like a valid email address. Please try again.")
            continue
        confirm = input(f"Confirm '{email}' [y/N]: ").strip().lower()
        if confirm == 'y':
            return email
        print("Cancelled - please enter the email address again.")

def main():
    """Main load script"""
    # Load configuration
    config = load_config()

    print(f"Connecting to PostgreSQL: {config['hostname']}:{config['port']}")
    print(f"Database: {config['database']}")
    print(f"Username: {config['username']}")

    # Prompt for the initial admin email before touching the database
    admin_email = prompt_admin_email()
    variables = {'admin_email': admin_email}

    # Load all data files
    data_files = [
        './jumptest.static.generated.sql',
        './jumptest.nav_menu.sql',
        './jumptest.currentuser.generated.sql',
        './jumptest.event.test.sql',
        './TestResultStatus.query.generated.sql',
        './TestResultStatus.children.generated.sql',
        './TestResultStatus.map.generated.sql',
        './TestPlan.query.generated.sql',
        './TestPlan.children.generated.sql',
        './TestPlan.map.generated.sql',
        './Org.query.generated.sql',
        './Org.children.generated.sql',
        './Org.map.generated.sql',
        './Operation.query.generated.sql',
        './Operation.children.generated.sql',
        './Operation.map.generated.sql',
        './OpRole.query.generated.sql',
        './OpRole.children.generated.sql',
        './OpRole.map.generated.sql',
        './CronEvery.query.generated.sql',
        './CronEvery.children.generated.sql',
        './CronEvery.map.generated.sql',
        './CronMinute.query.generated.sql',
        './CronMinute.children.generated.sql',
        './CronMinute.map.generated.sql',
        './CronHour.query.generated.sql',
        './CronHour.children.generated.sql',
        './CronHour.map.generated.sql',
        './CronDom.query.generated.sql',
        './CronDom.children.generated.sql',
        './CronDom.map.generated.sql',
        './CronMonth.query.generated.sql',
        './CronMonth.children.generated.sql',
        './CronMonth.map.generated.sql',
        './CronDow.query.generated.sql',
        './CronDow.children.generated.sql',
        './CronDow.map.generated.sql',
        './NavMenu.query.generated.sql',
        './NavMenu.children.generated.sql',
        './NavMenu.map.generated.sql',
        './DataSource.query.generated.sql',
        './DataSource.children.generated.sql',
        './DataSource.map.generated.sql',
        './AgentStatus.query.generated.sql',
        './AgentStatus.children.generated.sql',
        './AgentStatus.map.generated.sql',
        './OnFailure.query.generated.sql',
        './OnFailure.children.generated.sql',
        './OnFailure.map.generated.sql',
        './ExecStatus.query.generated.sql',
        './ExecStatus.children.generated.sql',
        './ExecStatus.map.generated.sql',
        './ServerNodeStatus.query.generated.sql',
        './ServerNodeStatus.children.generated.sql',
        './ServerNodeStatus.map.generated.sql',
        './ScriptType.query.generated.sql',
        './ScriptType.children.generated.sql',
        './ScriptType.map.generated.sql',
        './ServerNodeType.query.generated.sql',
        './ServerNodeType.children.generated.sql',
        './ServerNodeType.map.generated.sql',
        './WorkflowType.query.generated.sql',
        './WorkflowType.children.generated.sql',
        './WorkflowType.map.generated.sql',
        './PrincipalStatus.query.generated.sql',
        './PrincipalStatus.children.generated.sql',
        './PrincipalStatus.map.generated.sql',
        './TestCase.query.generated.sql',
        './TestCase.children.generated.sql',
        './TestCase.map.generated.sql',
        './TestRun.query.generated.sql',
        './TestRun.children.generated.sql',
        './TestRun.map.generated.sql',
        './OpRoleMap.query.generated.sql',
        './OpRoleMap.children.generated.sql',
        './OpRoleMap.map.generated.sql',
        './Schedule.query.generated.sql',
        './Schedule.children.generated.sql',
        './Schedule.map.generated.sql',
        './Sql.query.generated.sql',
        './Sql.children.generated.sql',
        './Sql.map.generated.sql',
        './Script.query.generated.sql',
        './Script.children.generated.sql',
        './Script.map.generated.sql',
        './ServerNode.query.generated.sql',
        './ServerNode.children.generated.sql',
        './ServerNode.map.generated.sql',
        './Principal.query.generated.sql',
        './Principal.children.generated.sql',
        './Principal.map.generated.sql',
        './EventService.query.generated.sql',
        './EventService.children.generated.sql',
        './EventService.map.generated.sql',
        './Process.query.generated.sql',
        './Process.children.generated.sql',
        './Process.map.generated.sql',
        './TestResult.query.generated.sql',
        './TestResult.children.generated.sql',
        './TestResult.map.generated.sql',
        './PrincipalOrg.query.generated.sql',
        './PrincipalOrg.children.generated.sql',
        './PrincipalOrg.map.generated.sql',
        './OpRoleMember.query.generated.sql',
        './OpRoleMember.children.generated.sql',
        './OpRoleMember.map.generated.sql',
        './Workflow.query.generated.sql',
        './Workflow.children.generated.sql',
        './Workflow.map.generated.sql',
        './ExecLog.query.generated.sql',
        './ExecLog.children.generated.sql',
        './ExecLog.map.generated.sql',
    ]

    if not data_files:
        print("No data files to load.")
        return

    print(f"\nLoading {len(data_files)} data files...")

    for sql_file in data_files:
        if not os.path.exists(sql_file):
            print(f"Warning: File not found, skipping: {sql_file}")
            continue

        run_psql(config, sql_file, variables)

    print("\nData loading completed successfully!")
    print(f"Admin account created for: {admin_email}")

if __name__ == '__main__':
    main()