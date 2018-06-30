# Weather station API

## Memo migrations :  
From `WeatherStation.Api.Data` folder :   
- Commit migration : `dotnet ef migrations add migrationName`  
- Apply commit : `dotnet ef database update`  

Then (ToFix) copy-paste the `WeatherStationRecords.db` file from `WeatherStation.Api.Data` folder to `WeatherStation.Api.Core`.  
  
dotnet publish -c Release -o out    
scp -r out/ root@*.*.*.*:/docker-data/weather-station-api/  
docker run -t --name weather-station-api -p 5000:80 -v /docker-data/weather-station-api/:/data/ microsoft/aspnetcore-build  
docker exec -it weather-station-api /bin/bash  

