using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Data.contract
{
    /// <summary>
    /// Contract of the Data Access Layer
    /// </summary>
    public interface IDal : IDisposable
    {
        #region Records
        /*
         * Add section
         */
        Task AddRecordAsync(DateTime dateTime, float temperature, float humidity, string broacasterName);

        /*
         * Get section
         */
        Task<IEnumerable<Record>> GetAllRecords(string broadcasterName);
        Task<IEnumerable<Record>> GetRecordsByDateRangeAsync(string broadcasterName, DateTime begin, DateTime end);
        Task<Record> GetLastRecordAsync(string broadcasterName);
        Task<Record> GetHottestRecordAsync(string broadcasterName);
        Task<Record> GetColdestRecordAsync(string broadcasterName);
        #endregion

        #region Broadcasters
        Task<IEnumerable<Broadcaster>> GetAllBroadcastersAsync();
        #endregion


    }
}