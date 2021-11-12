FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
# EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# COPY . .

COPY ["RideShare.sln", "RideShare"]
COPY ["/src/RideShare.Domain/", "RideShare.Domain/"]
COPY ["/src/RideShare.Application/", "RideShare.Application/"]
COPY ["/src/RideShare.Persistence/", "RideShare.Persistence/"]
COPY ["/src/RideShare.Infrastructure/", "RideShare.Infrastructure/"]
COPY ["/src/RideShare.Api/", "RideShare.Api/"]

RUN dotnet restore "RideShare.Api/RideShare.Api.csproj"



# COPY . .
WORKDIR "/src/"
RUN dotnet build "RideShare.Api" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RideShare.Api" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RideShare.Api.dll"]