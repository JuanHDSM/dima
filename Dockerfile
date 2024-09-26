# STAGE 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos da solução para o contêiner
COPY . .

# Restaura pacotes NuGet para os projetos
RUN dotnet restore "Dima.Api/Dima.Api.csproj"
RUN dotnet restore "Dima.Web/Dima.Web.csproj"

# Publica o projeto Blazor WebAssembly (Front-end) em um diretório separado
RUN dotnet publish "Dima.Web/Dima.Web.csproj" -c Release -o /app/publish/web

# Publica o projeto ASP.NET API (Back-end) em outro diretório
RUN dotnet publish "Dima.Api/Dima.Api.csproj" -c Release -o /app/publish/api

RUN dotnet workload install wasm-tools

# STAGE 2: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o conteúdo da API para a imagem final
COPY --from=build /app/publish/api . 

# Copia os arquivos do Blazor WebAssembly (wwwroot) para o wwwroot da API
COPY --from=build /app/publish/web/wwwroot ./wwwroot

# Expondo a porta 80
EXPOSE 80

# Entrypoint para iniciar a API ASP.NET, que agora serve o Blazor WebAssembly
ENTRYPOINT ["dotnet", "Dima.Api.dll"]
