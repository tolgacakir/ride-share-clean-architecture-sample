# ride-share
A sample repository of Clean Architecture and rich domain model

## How to run on local

- Change current directory:
```
cd src/RideShare.Api
```


- Restore
```
dotnet restore
```


- Run
```
dotnet watch run
```


## How to publish on Docker

- Change current directory:
```
cd src/RideShare.Persistence
```


- Add migration:
```
dotnet ef migrations add mig1
```


- Create Docker compose:
```
cd ..
cd .. 
```
(same with .sln file)
```
docker-compose up
```
