# STAGE 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos da solução para o contêiner
COPY . .

# Restaura pacotes NuGet para os projetos
RUN dotnet restore "Dima.Api/Dima.Api.csproj"
RUN dotnet restore "Dima.Web/Dima.Web.csproj"

# Publica o projeto Blazor WebAssembly (Front-end)
RUN dotnet publish "Dima.Web/Dima.Web.csproj" -c Release -o /app/build/Dima.Web

# Publica o projeto ASP.NET API (Back-end)
RUN dotnet publish "Dima.Api/Dima.Api.csproj" -c Release -o /app/publish

# STAGE 2: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o conteúdo publicado do build para a imagem final
COPY --from=build /app/publish . 

# Copia os arquivos do Blazor WebAssembly para o wwwroot da API
COPY --from=build /app/build/Dima.Web/wwwroot ./wwwroot

# Expondo a porta 80
EXPOSE 80

# Entrypoint para iniciar a API ASP.NET, que agora serve o Blazor WebAssembly
ENTRYPOINT ["dotnet", "Dima.Api.dll"]
