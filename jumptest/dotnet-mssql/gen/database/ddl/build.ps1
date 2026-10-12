# PowerShell script to build the database
# Reads server information from .namespace.json or .namespace file in user's home directory

# Get the user's home directory
$jsonFile = Join-Path $env:USERPROFILE ".jumptest.json"
$legacyFile = Join-Path $env:USERPROFILE ".jumptest"

# Initialize variables with defaults
$dbType = "sqlserver"
$server = "localhost"
$port = "1433"
$database = "jumptest"
$username = ""
$password = ""

# Try to read JSON format first
if (Test-Path $jsonFile) {
    Write-Host "Reading configuration from: $jsonFile"
    $jsonContent = Get-Content $jsonFile -Raw | ConvertFrom-Json
    
    # Get the default datasource
    if ($jsonContent.datasources.default) {
        $defaultDs = $jsonContent.datasources.default
        
        if ($defaultDs.dbtype) { $dbType = $defaultDs.dbtype }
        if ($defaultDs.hostname) { $server = $defaultDs.hostname }
        if ($defaultDs.port) { $port = $defaultDs.port }
        if ($defaultDs.database) { $database = $defaultDs.database }
        if ($defaultDs.username) { $username = $defaultDs.username }
        if ($defaultDs.password) { $password = $defaultDs.password }
    }
}
# Fall back to legacy format
elseif (Test-Path $legacyFile) {
    Write-Host "Reading configuration from legacy file: $legacyFile"
    $namespaceContent = Get-Content $legacyFile -Raw
    $namespaceParts = $namespaceContent.Trim().Split(':')
    
    if ($namespaceParts.Count -lt 4) {
        Write-Error "Invalid namespace file format. Expected: dbtype:server:port:database[:username:password]"
        Write-Error "Found: $namespaceContent"
        exit 1
    }
    
    $dbType = $namespaceParts[0]
    $server = $namespaceParts[1]
    $port = $namespaceParts[2]
    $database = $namespaceParts[3]
    
    if ($namespaceParts.Count -ge 5) {
        $username = $namespaceParts[4]
    }
    if ($namespaceParts.Count -ge 6) {
        $password = $namespaceParts[5]
    }
}
else {
    Write-Error "Configuration file not found. Looking for:"
    Write-Error "  - $jsonFile"
    Write-Error "  - $legacyFile"
    Write-Error "Please create a .jumptest.json file in your home directory"
    exit 1
}

# Validate that this is for SQL Server
if ($dbType -ne "mssql" -and $dbType -ne "sqlserver") {
    Write-Error "This script is for SQL Server databases. Namespace file specifies: $dbType"
    exit 1
}

# Build the connection string
$connectionString = "$server"
if ($port -and $port -ne "1433") {
    $connectionString += ",$port"
}

Write-Host "Connecting to SQL Server: $connectionString"
Write-Host "Database: $database"

# Build sqlcmd authentication parameters
$authParams = @()
if ($username -and $password) {
    Write-Host "Using SQL Server authentication with username: $username"
    $authParams = @("-U", $username, "-P", $password)
}
else {
    Write-Host "Using Windows authentication"
    $authParams = @("-E")
}

# Execute the database creation script
Write-Host "Executing database creation script..."
sqlcmd -S $connectionString @authParams -i ".\jumptest.database.create.generated.sql"

if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute database creation script"
    exit $LASTEXITCODE
}

# Execute all other SQL files

Write-Host "Executing: app.schema.create.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\app.schema.create.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: app.schema.create.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: core.schema.create.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\core.schema.create.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: core.schema.create.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResultStatus.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResultStatus.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResultStatus.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResultStatus.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResultStatus.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResultStatus.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResultStatus.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResultStatus.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResultStatus.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestPlan.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestPlan.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestPlan.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestPlan.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestPlan.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestPlan.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestPlan.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestPlan.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestPlan.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Org.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Org.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Org.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Org.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Org.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Org.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Org.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Org.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Org.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
<<<<<<< HEAD
Write-Host "Executing: Principal.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Principal.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Principal.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Principal.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Principal.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Principal.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Principal.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
=======
>>>>>>> main
Write-Host "Executing: Operation.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Operation.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Operation.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Operation.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Operation.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Operation.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Operation.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Operation.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Operation.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRole.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRole.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRole.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRole.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRole.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRole.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRole.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRole.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRole.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronEvery.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronEvery.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronEvery.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronEvery.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronEvery.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronEvery.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronEvery.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronEvery.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronEvery.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMinute.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronMinute.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronMinute.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMinute.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronMinute.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronMinute.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMinute.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronMinute.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronMinute.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronHour.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronHour.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronHour.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronHour.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronHour.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronHour.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronHour.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronHour.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronHour.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDom.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronDom.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronDom.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDom.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronDom.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronDom.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDom.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronDom.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronDom.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMonth.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronMonth.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronMonth.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMonth.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronMonth.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronMonth.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMonth.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronMonth.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronMonth.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDow.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronDow.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronDow.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDow.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronDow.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronDow.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDow.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\CronDow.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: CronDow.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: NavMenu.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\NavMenu.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: NavMenu.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: NavMenu.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\NavMenu.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: NavMenu.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: NavMenu.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\NavMenu.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: NavMenu.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: DataSource.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\DataSource.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: DataSource.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: DataSource.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\DataSource.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: DataSource.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: DataSource.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\DataSource.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: DataSource.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: AgentStatus.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\AgentStatus.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: AgentStatus.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: AgentStatus.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\AgentStatus.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: AgentStatus.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: AgentStatus.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\AgentStatus.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: AgentStatus.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OnFailure.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OnFailure.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OnFailure.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OnFailure.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OnFailure.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OnFailure.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OnFailure.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OnFailure.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OnFailure.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecStatus.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ExecStatus.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ExecStatus.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecStatus.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ExecStatus.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ExecStatus.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecStatus.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ExecStatus.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ExecStatus.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeStatus.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNodeStatus.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNodeStatus.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeStatus.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNodeStatus.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNodeStatus.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeStatus.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNodeStatus.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNodeStatus.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ScriptType.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ScriptType.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ScriptType.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ScriptType.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ScriptType.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ScriptType.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ScriptType.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ScriptType.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ScriptType.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeType.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNodeType.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNodeType.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeType.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNodeType.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNodeType.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeType.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNodeType.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNodeType.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: WorkflowType.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\WorkflowType.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: WorkflowType.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: WorkflowType.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\WorkflowType.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: WorkflowType.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: WorkflowType.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\WorkflowType.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: WorkflowType.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
<<<<<<< HEAD
=======
Write-Host "Executing: PrincipalStatus.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalStatus.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalStatus.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalStatus.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalStatus.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalStatus.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalStatus.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalStatus.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalStatus.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
>>>>>>> main
Write-Host "Executing: TestCase.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestCase.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestCase.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestCase.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestCase.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestCase.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestCase.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestCase.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestCase.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestRun.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestRun.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestRun.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestRun.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestRun.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestRun.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestRun.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestRun.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestRun.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
<<<<<<< HEAD
Write-Host "Executing: PrincipalOrg.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalOrg.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalOrg.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalOrg.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalOrg.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalOrg.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalOrg.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
=======
>>>>>>> main
Write-Host "Executing: OpRoleMap.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMap.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMap.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMap.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMap.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMap.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMap.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMap.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMap.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
<<<<<<< HEAD
Write-Host "Executing: OpRoleMember.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMember.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMember.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMember.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMember.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMember.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMember.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
=======
>>>>>>> main
Write-Host "Executing: Schedule.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Schedule.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Schedule.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Schedule.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Schedule.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Schedule.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Schedule.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Schedule.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Schedule.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Sql.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Sql.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Sql.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Sql.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Sql.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Sql.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Sql.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Sql.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Sql.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Script.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Script.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Script.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Script.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Script.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Script.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Script.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Script.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Script.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNode.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNode.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNode.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNode.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNode.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNode.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNode.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ServerNode.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ServerNode.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
<<<<<<< HEAD
Write-Host "Executing: TestResult.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResult.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResult.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResult.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResult.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResult.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResult.rwkindex.generated.sql"
=======
Write-Host "Executing: Principal.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Principal.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Principal.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Principal.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Principal.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Principal.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Principal.rwkindex.generated.sql"
>>>>>>> main
    exit $LASTEXITCODE
}
        
Write-Host "Executing: EventService.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\EventService.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: EventService.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: EventService.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\EventService.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: EventService.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: EventService.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\EventService.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: EventService.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Process.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Process.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Process.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Process.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Process.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Process.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Process.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Process.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Process.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
<<<<<<< HEAD
=======
Write-Host "Executing: TestResult.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResult.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResult.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResult.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResult.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\TestResult.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: TestResult.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalOrg.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalOrg.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalOrg.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalOrg.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\PrincipalOrg.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: PrincipalOrg.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMember.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMember.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMember.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMember.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\OpRoleMember.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: OpRoleMember.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
>>>>>>> main
Write-Host "Executing: Workflow.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Workflow.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Workflow.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Workflow.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Workflow.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Workflow.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Workflow.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\Workflow.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: Workflow.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecLog.table.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ExecLog.table.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ExecLog.table.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecLog.sequence.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ExecLog.sequence.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ExecLog.sequence.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecLog.rwkindex.generated.sql"
sqlcmd -S $connectionString @authParams -i ".\ExecLog.rwkindex.generated.sql"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: ExecLog.rwkindex.generated.sql"
    exit $LASTEXITCODE
}
        
Write-Host "Database build completed successfully!"