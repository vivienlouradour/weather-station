FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /app
WORKDIR /data

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WeatherStation.Api.Core/*.csproj ./WeatherStation.Api.Core/
COPY WeatherStation.Api.Data/*.csproj ./WeatherStation.Api.Data/
COPY WeatherStation.Api.BackupDatabaseBatch/*.csproj ./WeatherStation.Api.BackupDatabaseBatch/
RUN dotnet restore

# copy everything else and build app
COPY WeatherStation.Api.Core/. ./WeatherStation.Api.Core/
COPY WeatherStation.Api.Data/. ./WeatherStation.Api.Data/
COPY WeatherStation.Api.BackupDatabaseBatch/. ./WeatherStation.Api.BackupDatabaseBatch/

#WORKDIR /app/aspnetapp
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /data
COPY --from=build /data/WeatherStation.Api.Core/out ./
ENTRYPOINT ["dotnet run", "WeatherStation.Api.Core.dll"]