$databaseName = $args[0]
$databaseServer = $args[1]
$scriptPath = $args[2]

Add-Type -Path (Join-Path -Path $currentPath -ChildPath 'x:\location\of\DbUp.dll')

$dbUp = [DbUp.DeployChanges]::To
$dbUp = [SqlServerExtensions]::SqlDatabase($dbUp, "server=$databaseServer;database=$databaseName;Trusted_Connection=Yes;Connection Timeout=120;")
$dbUp = [StandardExtensions]::WithScriptsFromFileSystem($dbUp, $scriptPath)
$dbUp = [SqlServerExtensions]::JournalToSqlTable($dbUp, 'MySchema', 'MyTable')
$dbUp = [StandardExtensions]::LogToConsole($dbUp)
$upgradeResult = $dbUp.Build().PerformUpgrade()