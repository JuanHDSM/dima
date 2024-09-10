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

# Build stage for the Blazor WebAssembly frontend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-wasm
WORKDIR /src

COPY Dima.Web/Dima.Web.csproj Dima.Web/
COPY Dima.Core/Dima.Core.csproj Dima.Core/
RUN dotnet restore Dima.Web/Dima.Web.csproj

COPY Dima.Web/ Dima.Web/
COPY Dima.Core/ Dima.Core/

# Install Python (required for Blazor WebAssembly build)
RUN apt-get update && \
    apt-get install -y python3 python3-pip && \
    apt-get clean

RUN dotnet workload install wasm-tools
RUN dotnet tool install -g dotnet-serve

# Add the .dotnet/tools directory to the PATH
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src/Dima.Web
RUN dotnet publish Dima.Web.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base-wasm
WORKDIR /app

COPY --from=build-wasm /app/out/wwwroot ./wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet-serve", "-d", "wwwroot"]
