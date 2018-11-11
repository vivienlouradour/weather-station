using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherStation.Api.Data.contract;
using WeatherStation.Api.Data.Exceptions;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Data.implementation
{
    /// <inheritdoc />
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

        public async Task AddRecordAsync(DateTime dateTime, float temperature, float humidity, string broacasterName)
        {
            if(
                broacasterName == null ||
                dateTime.Equals(DateTime.MinValue) 
                )
                throw new ApiArgumentException("argument error");
            Broadcaster broadcaster = await _context.Broadcasters.FirstOrDefaultAsync(bc => bc.Name.Equals(broacasterName));
            if (broadcaster == null)
            {
                broadcaster = new Broadcaster()
                {
                    Name = broacasterName
                };
                await _context.Broadcasters.AddAsync(broadcaster);
                await _context.SaveChangesAsync();
            }

            var record = new Record()
            {
                DateTime = dateTime,
                Temperature = temperature,
                Humidity = humidity,
                BroadcasterId = broadcaster.BroadcasterId
            };
            
            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Record>> GetAllRecords(string broadcasterName)
        {
            return await GetRecordsByBroadcasterAsync(broadcasterName).ToListAsync();
        }

        public async Task<IEnumerable<Record>> GetRecordsByDateRangeAsync(string broadcasterName, DateTime begin, DateTime end)
        {
            if(begin == DateTime.MinValue || end == DateTime.MinValue)
                throw new  ApiArgumentException("Error parameter : begin and end have to be valid datetimes");
            if(begin > end)
                throw new ApiArgumentException("Error parameter : The 1st date should be prior to or the same as the 2nd date");

            var records = GetRecordsByBroadcasterAsync(broadcasterName)
                .Where(record => 
                    record.DateTime >= begin && 
                    record.DateTime <= end
                    );
            
            return await records.ToListAsync();
        }

        public async Task<Record> GetLastRecordAsync(string broadcasterName)
        {
            return await GetRecordsByBroadcasterAsync(broadcasterName).OrderByDescending(r => r.DateTime).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Broadcaster>> GetAllBroadcastersAsync()
        {
            return await _context.Broadcasters.ToListAsync();
        }

        private IQueryable<Record> GetRecordsByBroadcasterAsync(string broadcasterName)
        {
            var broadcaster =
                _context.Broadcasters.FirstOrDefault(b => b.Name.Equals(broadcasterName));
            if(broadcaster == null)
                throw new BroadcasterNotFoundException($"No broadcaster named {broadcasterName} found.");

            return _context.Records.Where(record => record.BroadcasterId == broadcaster.BroadcasterId);
        }
        
        
    }
}