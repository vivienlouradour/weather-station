# Weather Station  
  
Tiny weather station system, build with a single-board computer (Pine A64+) and a temperature/humidity sensor (Silicon Labs Si7021).  

## Hardware
- [Pine A64+](https://www.pine64.org/) single board computer.  

- [Si7021](https://www.pine64.org/?product=pine64-humidity-temperature-sensor "Si7021") temperature and humidity sensor.


## Project structure
### WeatherStation.Api 
Web API build with ASP.Net Core.  
Handle GET and PUT request for weather records.  
Data persistence : SQLite database with Entity Framework Core as data access layer.   
