# Metamask

An application designed to hide the metadata of a website for good-hearted office pranks.

> Note: This repository is for educational purposes. It's designed to demonstrate putting a simple .NET Core site together.

## Technology

* .NET Core 2 MVC
* Postgres Server
* Entity Framework
* xUnit
* Bootstrap / JQuery

## Running the application locally

Since this application requires Postgres server you'll need to it running. While you don't need to create the database, the src/Metamask.Web/appsettings.Development.json file will need to have the connection string updated to point to your database.

> Note: I prefer to use Docker to run Postgres Server. To do this check out this article: https://www.mattkruskamp.com/blog/2017/running-development-databases-with-docker/

### Visual Studio

Open the src/Metamask.sln, set the startup project to Metamask.Web, and press f5. The application will start and the database will automatically be created.

### Command line

Assuming .NET Core is installed on the system:

```
cd src/Metamask.Web
dotnet run Metamask.Web.csproj
```

Again, the application should start and the database will be created automatically.