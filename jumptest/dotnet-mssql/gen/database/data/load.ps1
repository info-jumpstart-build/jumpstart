# PowerShell script to load static data into the database
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

# Prompt for the initial admin email before touching the database
Write-Host ""
Write-Host ("-" * 60)
Write-Host "Initial Admin User Setup"
Write-Host ("-" * 60)
Write-Host "Enter the email address for the first administrator account."
Write-Host "This must match the email used to log in via Auth0."
Write-Host ""

$adminEmail = ""
while ($true) {
    $adminEmail = (Read-Host "Admin email address").Trim()
    if (-not $adminEmail) {
        Write-Warning "Email address cannot be empty. Please try again."
        continue
    }
    if ($adminEmail -notmatch '^[^@]+@[^@]+\.[^@]+$') {
        Write-Warning "That does not look like a valid email address. Please try again."
        continue
    }
    $confirm = (Read-Host "Confirm '$adminEmail' [y/N]").Trim().ToLower()
    if ($confirm -eq 'y') { break }
    Write-Host "Cancelled - please enter the email address again."
}

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

Write-Host "Loading static data files..."

# Load all data files

Write-Host "Loading: jumptest.static.generated.sql"
$sqlFile = ".\jumptest.static.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: jumptest.nav_menu.sql"
$sqlFile = ".\jumptest.nav_menu.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: jumptest.currentuser.generated.sql"
$sqlFile = ".\jumptest.currentuser.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: jumptest.event.test.sql"
$sqlFile = ".\jumptest.event.test.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestResultStatus.query.generated.sql"
$sqlFile = ".\TestResultStatus.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestResultStatus.children.generated.sql"
$sqlFile = ".\TestResultStatus.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestResultStatus.map.generated.sql"
$sqlFile = ".\TestResultStatus.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestPlan.query.generated.sql"
$sqlFile = ".\TestPlan.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestPlan.children.generated.sql"
$sqlFile = ".\TestPlan.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestPlan.map.generated.sql"
$sqlFile = ".\TestPlan.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Org.query.generated.sql"
$sqlFile = ".\Org.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Org.children.generated.sql"
$sqlFile = ".\Org.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Org.map.generated.sql"
$sqlFile = ".\Org.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Principal.query.generated.sql"
$sqlFile = ".\Principal.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Principal.children.generated.sql"
$sqlFile = ".\Principal.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Principal.map.generated.sql"
$sqlFile = ".\Principal.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Operation.query.generated.sql"
$sqlFile = ".\Operation.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Operation.children.generated.sql"
$sqlFile = ".\Operation.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Operation.map.generated.sql"
$sqlFile = ".\Operation.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRole.query.generated.sql"
$sqlFile = ".\OpRole.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRole.children.generated.sql"
$sqlFile = ".\OpRole.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRole.map.generated.sql"
$sqlFile = ".\OpRole.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronEvery.query.generated.sql"
$sqlFile = ".\CronEvery.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronEvery.children.generated.sql"
$sqlFile = ".\CronEvery.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronEvery.map.generated.sql"
$sqlFile = ".\CronEvery.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronMinute.query.generated.sql"
$sqlFile = ".\CronMinute.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronMinute.children.generated.sql"
$sqlFile = ".\CronMinute.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronMinute.map.generated.sql"
$sqlFile = ".\CronMinute.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronHour.query.generated.sql"
$sqlFile = ".\CronHour.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronHour.children.generated.sql"
$sqlFile = ".\CronHour.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronHour.map.generated.sql"
$sqlFile = ".\CronHour.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronDom.query.generated.sql"
$sqlFile = ".\CronDom.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronDom.children.generated.sql"
$sqlFile = ".\CronDom.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronDom.map.generated.sql"
$sqlFile = ".\CronDom.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronMonth.query.generated.sql"
$sqlFile = ".\CronMonth.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronMonth.children.generated.sql"
$sqlFile = ".\CronMonth.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronMonth.map.generated.sql"
$sqlFile = ".\CronMonth.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronDow.query.generated.sql"
$sqlFile = ".\CronDow.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronDow.children.generated.sql"
$sqlFile = ".\CronDow.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: CronDow.map.generated.sql"
$sqlFile = ".\CronDow.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: NavMenu.query.generated.sql"
$sqlFile = ".\NavMenu.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: NavMenu.children.generated.sql"
$sqlFile = ".\NavMenu.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: NavMenu.map.generated.sql"
$sqlFile = ".\NavMenu.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: DataSource.query.generated.sql"
$sqlFile = ".\DataSource.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: DataSource.children.generated.sql"
$sqlFile = ".\DataSource.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: DataSource.map.generated.sql"
$sqlFile = ".\DataSource.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: AgentStatus.query.generated.sql"
$sqlFile = ".\AgentStatus.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: AgentStatus.children.generated.sql"
$sqlFile = ".\AgentStatus.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: AgentStatus.map.generated.sql"
$sqlFile = ".\AgentStatus.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OnFailure.query.generated.sql"
$sqlFile = ".\OnFailure.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OnFailure.children.generated.sql"
$sqlFile = ".\OnFailure.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OnFailure.map.generated.sql"
$sqlFile = ".\OnFailure.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ExecStatus.query.generated.sql"
$sqlFile = ".\ExecStatus.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ExecStatus.children.generated.sql"
$sqlFile = ".\ExecStatus.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ExecStatus.map.generated.sql"
$sqlFile = ".\ExecStatus.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNodeStatus.query.generated.sql"
$sqlFile = ".\ServerNodeStatus.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNodeStatus.children.generated.sql"
$sqlFile = ".\ServerNodeStatus.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNodeStatus.map.generated.sql"
$sqlFile = ".\ServerNodeStatus.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ScriptType.query.generated.sql"
$sqlFile = ".\ScriptType.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ScriptType.children.generated.sql"
$sqlFile = ".\ScriptType.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ScriptType.map.generated.sql"
$sqlFile = ".\ScriptType.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNodeType.query.generated.sql"
$sqlFile = ".\ServerNodeType.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNodeType.children.generated.sql"
$sqlFile = ".\ServerNodeType.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNodeType.map.generated.sql"
$sqlFile = ".\ServerNodeType.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: WorkflowType.query.generated.sql"
$sqlFile = ".\WorkflowType.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: WorkflowType.children.generated.sql"
$sqlFile = ".\WorkflowType.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: WorkflowType.map.generated.sql"
$sqlFile = ".\WorkflowType.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalStatus.query.generated.sql"
$sqlFile = ".\PrincipalStatus.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalStatus.children.generated.sql"
$sqlFile = ".\PrincipalStatus.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalStatus.map.generated.sql"
$sqlFile = ".\PrincipalStatus.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestCase.query.generated.sql"
$sqlFile = ".\TestCase.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestCase.children.generated.sql"
$sqlFile = ".\TestCase.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestCase.map.generated.sql"
$sqlFile = ".\TestCase.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestRun.query.generated.sql"
$sqlFile = ".\TestRun.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestRun.children.generated.sql"
$sqlFile = ".\TestRun.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestRun.map.generated.sql"
$sqlFile = ".\TestRun.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalOrg.query.generated.sql"
$sqlFile = ".\PrincipalOrg.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalOrg.children.generated.sql"
$sqlFile = ".\PrincipalOrg.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalOrg.map.generated.sql"
$sqlFile = ".\PrincipalOrg.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMap.query.generated.sql"
$sqlFile = ".\OpRoleMap.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMap.children.generated.sql"
$sqlFile = ".\OpRoleMap.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMap.map.generated.sql"
$sqlFile = ".\OpRoleMap.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMember.query.generated.sql"
$sqlFile = ".\OpRoleMember.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMember.children.generated.sql"
$sqlFile = ".\OpRoleMember.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMember.map.generated.sql"
$sqlFile = ".\OpRoleMember.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Schedule.query.generated.sql"
$sqlFile = ".\Schedule.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Schedule.children.generated.sql"
$sqlFile = ".\Schedule.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Schedule.map.generated.sql"
$sqlFile = ".\Schedule.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Sql.query.generated.sql"
$sqlFile = ".\Sql.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Sql.children.generated.sql"
$sqlFile = ".\Sql.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Sql.map.generated.sql"
$sqlFile = ".\Sql.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Script.query.generated.sql"
$sqlFile = ".\Script.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Script.children.generated.sql"
$sqlFile = ".\Script.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Script.map.generated.sql"
$sqlFile = ".\Script.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNode.query.generated.sql"
$sqlFile = ".\ServerNode.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNode.children.generated.sql"
$sqlFile = ".\ServerNode.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ServerNode.map.generated.sql"
$sqlFile = ".\ServerNode.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
<<<<<<< HEAD
Write-Host "Loading: TestResult.query.generated.sql"
$sqlFile = ".\TestResult.query.generated.sql"
=======
Write-Host "Loading: Principal.query.generated.sql"
$sqlFile = ".\Principal.query.generated.sql"
>>>>>>> main
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
<<<<<<< HEAD
Write-Host "Loading: TestResult.children.generated.sql"
$sqlFile = ".\TestResult.children.generated.sql"
=======
Write-Host "Loading: Principal.children.generated.sql"
$sqlFile = ".\Principal.children.generated.sql"
>>>>>>> main
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
<<<<<<< HEAD
Write-Host "Loading: TestResult.map.generated.sql"
$sqlFile = ".\TestResult.map.generated.sql"
=======
Write-Host "Loading: Principal.map.generated.sql"
$sqlFile = ".\Principal.map.generated.sql"
>>>>>>> main
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: EventService.query.generated.sql"
$sqlFile = ".\EventService.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: EventService.children.generated.sql"
$sqlFile = ".\EventService.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: EventService.map.generated.sql"
$sqlFile = ".\EventService.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Process.query.generated.sql"
$sqlFile = ".\Process.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Process.children.generated.sql"
$sqlFile = ".\Process.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Process.map.generated.sql"
$sqlFile = ".\Process.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestResult.query.generated.sql"
$sqlFile = ".\TestResult.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestResult.children.generated.sql"
$sqlFile = ".\TestResult.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: TestResult.map.generated.sql"
$sqlFile = ".\TestResult.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalOrg.query.generated.sql"
$sqlFile = ".\PrincipalOrg.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalOrg.children.generated.sql"
$sqlFile = ".\PrincipalOrg.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: PrincipalOrg.map.generated.sql"
$sqlFile = ".\PrincipalOrg.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMember.query.generated.sql"
$sqlFile = ".\OpRoleMember.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMember.children.generated.sql"
$sqlFile = ".\OpRoleMember.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: OpRoleMember.map.generated.sql"
$sqlFile = ".\OpRoleMember.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Workflow.query.generated.sql"
$sqlFile = ".\Workflow.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Workflow.children.generated.sql"
$sqlFile = ".\Workflow.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: Workflow.map.generated.sql"
$sqlFile = ".\Workflow.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ExecLog.query.generated.sql"
$sqlFile = ".\ExecLog.query.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ExecLog.children.generated.sql"
$sqlFile = ".\ExecLog.children.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Loading: ExecLog.map.generated.sql"
$sqlFile = ".\ExecLog.map.generated.sql"
if (-not (Test-Path $sqlFile)) {
    Write-Warning "SQL file not found, skipping: $sqlFile"
    continue
}

<<<<<<< HEAD
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -i "$sqlFile"
=======
sqlcmd -S $connectionString -d $database @authParams -v "admin_email=$adminEmail" -v "os_login=$env:USERNAME" -i "$sqlFile"
>>>>>>> main
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to load: $sqlFile"
    Write-Error "Check the SQL file for syntax errors and ensure the database connection is working."
    exit $LASTEXITCODE
}
    
Write-Host "Data loading completed successfully!"