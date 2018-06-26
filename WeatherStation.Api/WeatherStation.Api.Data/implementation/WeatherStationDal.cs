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
        private WeatherStationContext _context;
        
        public WeatherStationDal()
        {
            _context = new WeatherStationContext();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddRecord(DateTime dateTime, float temperature, float humidity, string broacasterName)
        {
            Broadcaster broadcaster = _context.Broadcasters.FirstOrDefault(bc => bc.Name.Equals(broacasterName));
            if (broadcaster == null)
            {
                broadcaster = new Broadcaster()
                {
                    Name = broacasterName
                };
                _context.Broadcasters.Add(broadcaster);
            }

            Record record = new Record()
            {
                DateTime = dateTime,
                Temperature = temperature,
                Humidity = humidity,
                Broadcaster = broadcaster
            };
            
            _context.Records.Add(record);
            _context.SaveChanges();
        }


        public IEnumerable<Record> GetAllRecords()
        {
            return _context.Records.ToList();
        }

        public IEnumerable<Record> GetRecordsByDateRange(DateTime begin, DateTime end)
        {
            if(begin == DateTime.MinValue || end == DateTime.MinValue)
                throw new  ApiArgumentException("Error parameter : begin and end have to be valid datetimes");
            
            return _context.Records.Where(record => record.DateTime >= begin && record.DateTime <= end).ToList();
        }

        public Record GetLastRecord()
        {
            return _context.Records.OrderByDescending(r => r.DateTime).FirstOrDefault();
        }
    }
}