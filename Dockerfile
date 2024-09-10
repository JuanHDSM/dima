# Build stage for the ASP.NET Core backend API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src

COPY Dima.Api/Dima.Api.csproj Dima.Api/
COPY Dima.Core/Dima.Core.csproj Dima.Core/
RUN dotnet restore Dima.Api/Dima.Api.csproj

COPY Dima.Api/ Dima.Api/
COPY Dima.Core/ Dima.Core/
WORKDIR /src/Dima.Api
RUN dotnet publish Dima.Api.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base-api
WORKDIR /app

COPY --from=build-api /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "Dima.Api.dll"]

