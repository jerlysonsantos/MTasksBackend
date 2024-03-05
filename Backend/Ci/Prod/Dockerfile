# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY Backend/*.sln .
COPY Backend/*.csproj ./aspnetapp/

COPY Backend/. ./aspnetapp/

WORKDIR /source/aspnetapp

RUN dotnet restore

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
EXPOSE 80

CMD ["dotnet", "cproject.dll"]