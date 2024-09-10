FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-wasm
WORKDIR /src

COPY Dima.Web/Dima.Web.csproj Dima.Web/
RUN dotnet restore Dima.Web/Dima.Web.csproj

COPY Dima.Web/ Dima.Web/
WORKDIR /src/Dima.Web
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src

COPY Dima.Api/Dima.Api.csproj Dima.Api/
RUN dotnet restore Dima.Api/Dima.Api.csproj

COPY Dima.Api/ Dima.Api/
WORKDIR /src/Dima.Api
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build-api /app/out .

COPY --from=build-wasm /app/out/wwwroot ./wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet", "Dima.Api.dll"]
