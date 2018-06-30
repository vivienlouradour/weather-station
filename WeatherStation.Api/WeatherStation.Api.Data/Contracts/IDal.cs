using System;
using System.Collections.Generic;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Data.contract
{
    /// <summary>
    /// Contract of the Data Access Layer
    /// </summary>
    public interface IDal : IDisposable
    {
        /*
         * Add section
         */
        void AddRecord(DateTime dateTime, float temperature, float humidity, string broacasterName);
        
        /*
         * Get section
         */
        IEnumerable<Record> GetAllRecords(string broadcasterName);
        IEnumerable<Record> GetRecordsByDateRange(string broadcasterName, DateTime begin, DateTime end); 
        Record GetLastRecord(string broadcasterName);
        IEnumerable<Broadcaster> GetAllBroadcasters();


    }
}