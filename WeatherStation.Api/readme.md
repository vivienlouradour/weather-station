# Weather station API

## Memo migrations :  
From `WeatherStation.Api.Data` folder :   
- Commit migration : `dotnet ef migrations add migrationName`  
- Apply commit : `dotnet ef database update`  

Then (ToFix) copy-paste the `WeatherStationRecords.db` file from `WeatherStation.Api.Data` folder to `WeatherStation.Api.Core`.  

## Publish steps  
```bash
scp -r WeatherStation.Api.Core/out/ *@*.*.*.*:/docker-data/weather-station-api/  

# Docker memo
# in /WeatherStation.Api/ folder
docker build --pull -t weather-station-api-image .   
docker run -d --name weather-station-api -p 5000:80 [-v /docker-data/weather-station-api/:/data/] weather-station-api-image

# Not necessary (done by the dockerfile)
docker exec -it weather-station-api /bin/bash  
dotnet WeatherStation.Api.Core.dll
```
/!\  Remember backup WheatherStationRecords.db  /!\   

