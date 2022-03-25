# About The Project

## Description
A sample RESTful Web Service of Clean Architecture and rich domain model.

<br/>

## Architecture
- ### Domain
Where it all started. It is the center of architecture. Includes only domain logic. It has no dependencies to any project or package.

- ### Application
It depends on *Domain*. Includes application logic, contracts, mappings, validations.

- ### Persistence
It depends on application core (*Application* and *Domain*). It doesn't include any logic, only includes data access implementations related to application core.

- ### Infrastructure
It includes some of technologies related to application core.

- ### Api
It is the startup project. Handles requests from client. It includes Web and Http related implementations, RESTful concepts, endpoints, dependency injections.


<br/>

## Technologies
- .Net 5.0
- AspNetCore WebAPI
- EntityFrameworkCore
- FluentValidation
- MediatR
- AutoMapper
- MSSQLServer
- Docker



<br/><br/>

# Installation

Clone from GitHub
```git bash
git clone https://github.com/tolgacakir/ride-share.git
```

<br/><br/>

# Build And Deploy

## How to run on local

<br/>

- **Add Migration**
```powershell
dotnet ef migrations add InitialMigration --project src/RideShare.Persistence
```

<br/>

- **Restore**
```powershell
dotnet restore src/RideShare.Api
```

<br/>

*Make sure the SQL Server name in the [connection string](https://github.com/tolgacakir/ride-share/blob/main/src/RideShare.Api/appsettings.json#L10) is correct.*

- **Run**

```powershell
dotnet watch run --project src/RideShare.Api
```

<br/><br/>

## How to publish on Docker

<br/>
Make sure the SQL Server name in the connection string is correct.

Make sure the generated rideshare-api image is generated from your code has the correct connection string.
<br/>

- **Add Migration**
```powershell
dotnet ef migrations add InitialMigration --project src/RideShare.Persistence
```

<br/>

- **Create Docker compose:**
```docker
docker-compose up
```

<br/>

- **Browse Swagger:**
```
http://localhost:5000/swagger/index.html
```
