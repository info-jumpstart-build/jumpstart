# PowerShell script to build the PostgreSQL database
# Reads server information from .namespace.json or .namespace file in user's home directory
# 
# Prerequisites:
# 1. PostgreSQL client tools (psql) must be installed and in PATH
# 2. If execution policy blocks scripts: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

# Check if psql is available
if (-not (Get-Command psql -ErrorAction SilentlyContinue)) {
    Write-Error "psql command not found. Please ensure PostgreSQL client tools are installed and in your PATH."
    Write-Error "Download from: https://www.postgresql.org/download/windows/"
    exit 1
}

# Get the user's home directory
$jsonFile = Join-Path $env:USERPROFILE ".jumptest.json"
$legacyFile = Join-Path $env:USERPROFILE ".jumptest"

# Initialize variables with defaults
$dbType = "postgresql"
$server = "localhost"
$port = "5432"
$database = "jumptest"
$username = "postgres"
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

# Validate that this is for PostgreSQL
if ($dbType -ne "postgresql" -and $dbType -ne "pgsql") {
    Write-Error "This script is for PostgreSQL databases. Namespace file specifies: $dbType"
    exit 1
}

Write-Host "Connecting to PostgreSQL: $server`:$port"
Write-Host "Database: $database"
Write-Host "Username: $username"

# Set PGPASSWORD environment variable if password is provided
if ($password) {
    $env:PGPASSWORD = $password
    Write-Host "Using PostgreSQL authentication with username: $username"
}
else {
    Write-Host "Using PostgreSQL default authentication"
}

# Execute the database creation script
Write-Host "Executing database creation script..."
$dbCreateScript = ".\jumptest.database.create.generated.sql"
if (-not (Test-Path $dbCreateScript)) {
    Write-Error "Database creation script not found: $dbCreateScript"
    exit 1
}

psql --host=$server --port=$port --dbname=postgres --username=$username --file="$dbCreateScript"

if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute database creation script: $dbCreateScript"
    Write-Error "Check your PostgreSQL connection settings and ensure the server is running."
    exit $LASTEXITCODE
}

# Execute all other SQL files

Write-Host "Executing: app.schema.create.generated.sql"
$sqlFile = ".\app.schema.create.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: core.schema.create.generated.sql"
$sqlFile = ".\core.schema.create.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResultStatus.table.generated.sql"
$sqlFile = ".\TestResultStatus.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResultStatus.sequence.generated.sql"
$sqlFile = ".\TestResultStatus.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResultStatus.rwkindex.generated.sql"
$sqlFile = ".\TestResultStatus.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestPlan.table.generated.sql"
$sqlFile = ".\TestPlan.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestPlan.sequence.generated.sql"
$sqlFile = ".\TestPlan.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestPlan.rwkindex.generated.sql"
$sqlFile = ".\TestPlan.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Org.table.generated.sql"
$sqlFile = ".\Org.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Org.sequence.generated.sql"
$sqlFile = ".\Org.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Org.rwkindex.generated.sql"
$sqlFile = ".\Org.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Operation.table.generated.sql"
$sqlFile = ".\Operation.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Operation.sequence.generated.sql"
$sqlFile = ".\Operation.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Operation.rwkindex.generated.sql"
$sqlFile = ".\Operation.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRole.table.generated.sql"
$sqlFile = ".\OpRole.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRole.sequence.generated.sql"
$sqlFile = ".\OpRole.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRole.rwkindex.generated.sql"
$sqlFile = ".\OpRole.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronEvery.table.generated.sql"
$sqlFile = ".\CronEvery.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronEvery.sequence.generated.sql"
$sqlFile = ".\CronEvery.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronEvery.rwkindex.generated.sql"
$sqlFile = ".\CronEvery.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMinute.table.generated.sql"
$sqlFile = ".\CronMinute.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMinute.sequence.generated.sql"
$sqlFile = ".\CronMinute.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMinute.rwkindex.generated.sql"
$sqlFile = ".\CronMinute.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronHour.table.generated.sql"
$sqlFile = ".\CronHour.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronHour.sequence.generated.sql"
$sqlFile = ".\CronHour.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronHour.rwkindex.generated.sql"
$sqlFile = ".\CronHour.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDom.table.generated.sql"
$sqlFile = ".\CronDom.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDom.sequence.generated.sql"
$sqlFile = ".\CronDom.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDom.rwkindex.generated.sql"
$sqlFile = ".\CronDom.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMonth.table.generated.sql"
$sqlFile = ".\CronMonth.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMonth.sequence.generated.sql"
$sqlFile = ".\CronMonth.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronMonth.rwkindex.generated.sql"
$sqlFile = ".\CronMonth.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDow.table.generated.sql"
$sqlFile = ".\CronDow.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDow.sequence.generated.sql"
$sqlFile = ".\CronDow.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: CronDow.rwkindex.generated.sql"
$sqlFile = ".\CronDow.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: NavMenu.table.generated.sql"
$sqlFile = ".\NavMenu.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: NavMenu.sequence.generated.sql"
$sqlFile = ".\NavMenu.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: NavMenu.rwkindex.generated.sql"
$sqlFile = ".\NavMenu.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: DataSource.table.generated.sql"
$sqlFile = ".\DataSource.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: DataSource.sequence.generated.sql"
$sqlFile = ".\DataSource.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: DataSource.rwkindex.generated.sql"
$sqlFile = ".\DataSource.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: AgentStatus.table.generated.sql"
$sqlFile = ".\AgentStatus.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: AgentStatus.sequence.generated.sql"
$sqlFile = ".\AgentStatus.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: AgentStatus.rwkindex.generated.sql"
$sqlFile = ".\AgentStatus.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OnFailure.table.generated.sql"
$sqlFile = ".\OnFailure.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OnFailure.sequence.generated.sql"
$sqlFile = ".\OnFailure.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OnFailure.rwkindex.generated.sql"
$sqlFile = ".\OnFailure.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecStatus.table.generated.sql"
$sqlFile = ".\ExecStatus.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecStatus.sequence.generated.sql"
$sqlFile = ".\ExecStatus.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecStatus.rwkindex.generated.sql"
$sqlFile = ".\ExecStatus.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeStatus.table.generated.sql"
$sqlFile = ".\ServerNodeStatus.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeStatus.sequence.generated.sql"
$sqlFile = ".\ServerNodeStatus.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeStatus.rwkindex.generated.sql"
$sqlFile = ".\ServerNodeStatus.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ScriptType.table.generated.sql"
$sqlFile = ".\ScriptType.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ScriptType.sequence.generated.sql"
$sqlFile = ".\ScriptType.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ScriptType.rwkindex.generated.sql"
$sqlFile = ".\ScriptType.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeType.table.generated.sql"
$sqlFile = ".\ServerNodeType.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeType.sequence.generated.sql"
$sqlFile = ".\ServerNodeType.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNodeType.rwkindex.generated.sql"
$sqlFile = ".\ServerNodeType.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: WorkflowType.table.generated.sql"
$sqlFile = ".\WorkflowType.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: WorkflowType.sequence.generated.sql"
$sqlFile = ".\WorkflowType.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: WorkflowType.rwkindex.generated.sql"
$sqlFile = ".\WorkflowType.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalStatus.table.generated.sql"
$sqlFile = ".\PrincipalStatus.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalStatus.sequence.generated.sql"
$sqlFile = ".\PrincipalStatus.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalStatus.rwkindex.generated.sql"
$sqlFile = ".\PrincipalStatus.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestCase.table.generated.sql"
$sqlFile = ".\TestCase.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestCase.sequence.generated.sql"
$sqlFile = ".\TestCase.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestCase.rwkindex.generated.sql"
$sqlFile = ".\TestCase.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestRun.table.generated.sql"
$sqlFile = ".\TestRun.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestRun.sequence.generated.sql"
$sqlFile = ".\TestRun.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestRun.rwkindex.generated.sql"
$sqlFile = ".\TestRun.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMap.table.generated.sql"
$sqlFile = ".\OpRoleMap.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMap.sequence.generated.sql"
$sqlFile = ".\OpRoleMap.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMap.rwkindex.generated.sql"
$sqlFile = ".\OpRoleMap.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Schedule.table.generated.sql"
$sqlFile = ".\Schedule.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Schedule.sequence.generated.sql"
$sqlFile = ".\Schedule.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Schedule.rwkindex.generated.sql"
$sqlFile = ".\Schedule.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Sql.table.generated.sql"
$sqlFile = ".\Sql.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Sql.sequence.generated.sql"
$sqlFile = ".\Sql.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Sql.rwkindex.generated.sql"
$sqlFile = ".\Sql.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Script.table.generated.sql"
$sqlFile = ".\Script.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Script.sequence.generated.sql"
$sqlFile = ".\Script.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Script.rwkindex.generated.sql"
$sqlFile = ".\Script.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNode.table.generated.sql"
$sqlFile = ".\ServerNode.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNode.sequence.generated.sql"
$sqlFile = ".\ServerNode.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ServerNode.rwkindex.generated.sql"
$sqlFile = ".\ServerNode.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.table.generated.sql"
$sqlFile = ".\Principal.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.sequence.generated.sql"
$sqlFile = ".\Principal.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Principal.rwkindex.generated.sql"
$sqlFile = ".\Principal.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: EventService.table.generated.sql"
$sqlFile = ".\EventService.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: EventService.sequence.generated.sql"
$sqlFile = ".\EventService.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: EventService.rwkindex.generated.sql"
$sqlFile = ".\EventService.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Process.table.generated.sql"
$sqlFile = ".\Process.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Process.sequence.generated.sql"
$sqlFile = ".\Process.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Process.rwkindex.generated.sql"
$sqlFile = ".\Process.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.table.generated.sql"
$sqlFile = ".\TestResult.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.sequence.generated.sql"
$sqlFile = ".\TestResult.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: TestResult.rwkindex.generated.sql"
$sqlFile = ".\TestResult.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.table.generated.sql"
$sqlFile = ".\PrincipalOrg.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.sequence.generated.sql"
$sqlFile = ".\PrincipalOrg.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: PrincipalOrg.rwkindex.generated.sql"
$sqlFile = ".\PrincipalOrg.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.table.generated.sql"
$sqlFile = ".\OpRoleMember.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.sequence.generated.sql"
$sqlFile = ".\OpRoleMember.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: OpRoleMember.rwkindex.generated.sql"
$sqlFile = ".\OpRoleMember.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Workflow.table.generated.sql"
$sqlFile = ".\Workflow.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Workflow.sequence.generated.sql"
$sqlFile = ".\Workflow.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: Workflow.rwkindex.generated.sql"
$sqlFile = ".\Workflow.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecLog.table.generated.sql"
$sqlFile = ".\ExecLog.table.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecLog.sequence.generated.sql"
$sqlFile = ".\ExecLog.sequence.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Executing: ExecLog.rwkindex.generated.sql"
$sqlFile = ".\ExecLog.rwkindex.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

psql --host=$server --port=$port --dbname=$database --username=$username --file="$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to execute: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
        
Write-Host "Database build completed successfully!"