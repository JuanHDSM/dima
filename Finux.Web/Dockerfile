FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore Finux.Web/Finux.Web.csproj
RUN dotnet publish Finux.Web/Finux.Web.csproj \
    -c Release \
    -o /app/publish

FROM nginx:alpine

COPY nginx/nginx.conf /etc/nginx/nginx.conf

COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80