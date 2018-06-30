using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStation.Api.Data.contract;
using WeatherStation.Api.Data.Exceptions;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Data.implementation
{
    /// <summary>
    /// Implementation of IDal with SQLite and EntityFrameworkCore
    /// </summary>
    public class WeatherStationDal : IDal
    {
        /// <summary>
        /// EF context
        /// </summary>
        private readonly WeatherStationContext _context;
        
        public WeatherStationDal(WeatherStationContext context)
        {
            _context = context;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddRecord(DateTime dateTime, float temperature, float humidity, string broacasterName)
        {
            if(
                broacasterName == null ||
                dateTime.Equals(DateTime.MinValue) 
                )
                throw new ApiArgumentException("argument error");
            Broadcaster broadcaster = _context.Broadcasters.FirstOrDefault(bc => bc.Name.Equals(broacasterName));
            if (broadcaster == null)
            {
                broadcaster = new Broadcaster()
                {
                    Name = broacasterName
                };
                _context.Broadcasters.Add(broadcaster);
                _context.SaveChanges();
            }

            var record = new Record()
            {
                DateTime = dateTime,
                Temperature = temperature,
                Humidity = humidity,
                BroadcasterId = broadcaster.BroadcasterId
            };
            
            _context.Records.Add(record);
            _context.SaveChanges();
        }


        public IEnumerable<Record> GetAllRecords(string broadcasterName)
        {
            return GetRecordsByBroadcaster(broadcasterName).ToList();
        }

        public IEnumerable<Record> GetRecordsByDateRange(string broadcasterName, DateTime begin, DateTime end)
        {
            if(begin == DateTime.MinValue || end == DateTime.MinValue)
                throw new  ApiArgumentException("Error parameter : begin and end have to be valid datetimes");
            
            return GetRecordsByBroadcaster(broadcasterName).Where(record => record.DateTime >= begin && record.DateTime <= end).ToList();
        }

        public Record GetLastRecord(string broadcasterName)
        {
            return GetRecordsByBroadcaster(broadcasterName).OrderByDescending(r => r.DateTime).FirstOrDefault();
        }

        public IEnumerable<Broadcaster> GetAllBroadcasters()
        {
            return _context.Broadcasters.ToList();
        }

        private IQueryable<Record> GetRecordsByBroadcaster(string broadcasterName)
        {
            var broadcaster =
                _context.Broadcasters.FirstOrDefault(b => b.Name.Equals(broadcasterName));
            if(broadcaster == null)
                throw new ApiArgumentException($"No broadcaster named {broadcasterName} found.");

            return _context.Records.Where(record => record.BroadcasterId == broadcaster.BroadcasterId);
        }
        
        
    }
}