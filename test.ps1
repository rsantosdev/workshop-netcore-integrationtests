
# Starts required services
docker-compose -f docker-compose.yml up -d
Write-Host "Compose Services UP"

# Waits until the container is in healthy state (accepting connections)
$sqlstate = docker inspect -f '{{json .State.Health.Status}}' dev_sql
do {
    Write-Host $sqlstate
    Start-Sleep 1
    $sqlstate = docker inspect -f '{{json .State.Health.Status}}' dev_sql
} while ($sqlstate -ne "`"healthy`"")

# Deletes any previous test run
Get-ChildItem .\ -include testresults, coveragereport, *.cobertura.xml -Recurse | ForEach-Object ($_) { remove-item $_.fullname -Force -Recurse }
Write-Host "Deleted previous tests results and coverage"   

# Sets Connection String and run test
$Env:ConnectionStrings__DefaultConnection = "Server=127.0.0.1,1435;Initial Catalog=workshoptest;Persist Security Info=False;User ID=sa;Password=yourStrong(!)Password;MultipleActiveResultSets=False;"   

Write-Host "Start Running Tests"
dotnet test /p:CollectCoverage=true `
    /p:Exclude="[*]Workshop.IntegrationTests.Platform.Migrations.*" `
    /p:Include="[Workshop.IntegrationTests*]*" `
    /p:CoverletOutputFormat=cobertura `
    /p:Threshold=60 `
    -l:trx .\Workshop.IntegrationTests.Tests\Workshop.IntegrationTests.Tests.csproj

$testResult = $LASTEXITCODE
Write-Host "Done Running Tests: $LASTEXITCODE"

# Generate Coverage Report
if ($null -eq $env:TF_BUILD) {
    Write-Host "Generating Coverage Report"    
    Set-Location .\Workshop.IntegrationTests.Tests\
    dotnet $env:UserProfile\.nuget\packages\reportgenerator\4.5.4\tools\netcoreapp3.0\ReportGenerator.dll "-reports:**/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:HtmlInline_AzurePipelines
    Set-Location ..
    Write-Host "Done Genearating Coverage Report"
}

# Stop required services
docker-compose -f docker-compose.yml down
Write-Host "Compose Services DOWN"

exit $testResult
