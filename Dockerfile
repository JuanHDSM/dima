# STAGE 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiando arquivos da solução
COPY . .

# Restaurando pacotes NuGet para todos os projetos
RUN dotnet restore "Dima.Api/Dima.Api.csproj"
RUN dotnet restore "Dima.Web/Dima.Web.csproj"

# Build do projeto ASP.NET API (Back-end)
RUN dotnet build "Dima.Api/Dima.Api.csproj" -c Release -o /app/build

# Build do projeto Blazor WebAssembly (Front-end)
RUN dotnet build "Dima.Web/Dima.Web.csproj" -c Release -o /app/build

# Publicar ambos os projetos
RUN dotnet publish "Dima.Api/Dima.Api.csproj" -c Release -o /app/publish
RUN dotnet publish "Dima.Web/Dima.Web.csproj" -c Release -o /app/publish/wwwroot

# STAGE 2: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
COPY Dima.Api/Dima.Api.csproj Dima.Api/
COPY Dima.Core/Dima.Core.csproj Dima.Core/
RUN dotnet restore Dima.Api/Dima.Api.csproj

COPY Dima.Api/ Dima.Api/
COPY Dima.Core/ Dima.Core/
WORKDIR /src/Dima.Api
RUN dotnet publish Dima.Api.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base-api
WORKDIR /app

# Copia o conteúdo publicado do build para a imagem final
COPY --from=build /app/publish .

# Definindo a porta para expor a API (ajuste conforme necessário)
EXPOSE 80

# Configurando o entrypoint para iniciar a API ASP.NET junto com o Blazor WebAssembly
ENTRYPOINT ["dotnet", "Dima.Api.dll"]
