FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Dima.Api/Dima.Api.csproj"
RUN dotnet restore "Dima.Web/Dima.Web.csproj"

RUN dotnet build "Dima.Api/Dima.Api.csproj" -c Release -o /app/build

RUN dotnet build "Dima.Web/Dima.Web.csproj" -c Release -o /app/build

RUN dotnet publish "Dima.Web/Dima.Web.csproj" -c Release -o /app/build/Dima.Web

RUN dotnet publish "Dima.Api/Dima.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish . 

COPY --from=build /app/build/Dima.Web/wwwroot ./wwwroot

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Dima.Api.dll"]