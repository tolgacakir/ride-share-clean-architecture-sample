#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RideShare.Api/RideShare.Api.csproj", "RideShare.Api/"]
RUN dotnet restore "RideShare.Api/RideShare.Api.csproj"
COPY . .
WORKDIR "/src/RideShare.Api"
RUN dotnet build "RideShare.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RideShare.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RideShare.Api.dll"]