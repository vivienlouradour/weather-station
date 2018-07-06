# Weather station API

## Memo migrations :  
From `WeatherStation.Api.Data` folder :   
- Commit migration : `dotnet ef migrations add migrationName`  
- Apply commit : `dotnet ef database update`  

Then (ToFix) copy-paste the `WeatherStationRecords.db` file from `WeatherStation.Api.Data` folder to `WeatherStation.Api.Core`.  

## Publish steps  
```bash
# in /WeatherStation.Api/ folder
dotnet publish -c Release -o out    

scp -r WeatherStation.Api.Core/out/ *@*.*.*.*:/docker-data/weather-station-api/  

# Docker memo
docker run -t --name weather-station-api -p 5000:80 -v /docker-data/weather-station-api/:/data/ microsoft/aspnetcore-build  
docker exec -it weather-station-api /bin/bash  
dotnet WeatherStation.Api.Core.dll
```
/!\  Remember backup WheatherStationRecords.db  /!\   

