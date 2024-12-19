# STAGE 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos da solução para o contêiner
COPY . .

# Restaurar pacotes NuGet para todos os projetos
RUN dotnet restore "Dima.Api/Dima.Api.csproj"
RUN dotnet restore "Dima.Web/Dima.Web.csproj"

# Build do projeto ASP.NET API (Back-end)
RUN dotnet build "Dima.Api/Dima.Api.csproj" -c Release -o /app/build

# Build do projeto Blazor WebAssembly (Front-end)
RUN dotnet build "Dima.Web/Dima.Web.csproj" -c Release -o /app/build

# Publicar o projeto Blazor WebAssembly (Front-end)
RUN dotnet publish "Dima.Web/Dima.Web.csproj" -c Release -o /app/build/Dima.Web

# Publicar o projeto ASP.NET API (Back-end)
RUN dotnet publish "Dima.Api/Dima.Api.csproj" -c Release -o /app/publish

# STAGE 2: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar o conteúdo publicado do backend para a imagem final
COPY --from=build /app/publish .

# Copiar os arquivos do Blazor WebAssembly para o wwwroot da API
COPY --from=build /app/build/Dima.Web/wwwroot ./wwwroot

# Configurar variáveis de ambiente de globalização
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV DOTNET_SYSTEM_GLOBALIZATION_PREDEFINED_CULTURES_ONLY=false

# Expor as portas padrão
EXPOSE 80
EXPOSE 443

# Configurar o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "Dima.Api.dll"]
