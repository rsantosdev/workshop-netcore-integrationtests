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

# Sets Connection String and run test
$Env:ConnectionStrings__DefaultConnection = "Server=127.0.0.1,1435;Initial Catalog=workshoptest;Persist Security Info=False;User ID=sa;Password=yourStrong(!)Password;MultipleActiveResultSets=False;"   

dotnet test

# Stop required services
docker-compose -f docker-compose.yml down
Write-Host "Compose Services DOWN"