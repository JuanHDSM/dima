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

# Publicar o projeto Blazor WebAssembly (Front-end) e copiar o resultado para a pasta wwwroot da API
RUN dotnet publish "Dima.Web/Dima.Web.csproj" -c Release -o /app/blazor_build
RUN mkdir -p /app/publish/wwwroot
RUN cp -r /app/blazor_build/wwwroot/* /app/publish/wwwroot/

# Publicar o projeto ASP.NET API (Back-end)
RUN dotnet publish "Dima.Api/Dima.Api.csproj" -c Release -o /app/publish

# STAGE 2: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o conteúdo publicado do build para a imagem final
COPY --from=build /app/publish .

# Expor a API e o front-end
EXPOSE 80
EXPOSE 443

# Configurando o entrypoint para iniciar a API ASP.NET junto com o Blazor WebAssembly
ENTRYPOINT ["dotnet", "Dima.Api.dll"]
