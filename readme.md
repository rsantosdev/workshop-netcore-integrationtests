# Workshop Integration Tests

### Required SDKs and Tools

.NET Core 3.1.x
Make sure you have latest SDK version of .net core 3.1.x

EF Core Tools
- If you don't have the tool installed already run: `dotnet tool install --global dotnet-ef`
- If you already have the tool, update it: `dotnet tool update --global dotnet-ef`
- To validate if you have everything installed run: `dotnet ef`

Doker Engine
Please make sure you have latest docker version for you operating system.
- Run the following command to download the sql server version: `docker pull mcr.microsoft.com/mssql/server:2017-latest`
The above command may take while if it's the first time you are pulling sql server image

### Migrations
In order to create a new migration:
- Navigate to the Workshop.IntegrationTests.Platform folder
- Run the following command: `dotnet ef migrations -s ..\Workshop.IntegrationTests.Api\ add YourMigrationName`

In order to setup a new database or update the current one:
- Make sure you have a valid connection string in the appsettings.Development.json file located at the Api project.
- Run the following command: `dotnet ef database update -s ..\Workshop.IntegrationTests.Api\`

### First user
After creating your local database you can setup a user running the following script:
`INSERT INTO Users VALUES('6451D5C4-B6EC-4004-BAB0-9075ADAB7860', 'user@workshop.com', 'f2yIM4hbud2gmiggYvNzUHptz0cSTp6W/OdOSMT0AVU=', 'emnOk0/O1NzCDT9XkkRJVQ==')`

This will create an user with email: `user@workshop.com` and password: `workshop`
