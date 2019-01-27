using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherStation.Api.Data.Exceptions;
using WeatherStation.Api.Data.implementation;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.CoreMock.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    public class RecordController : Controller
    {
        private Random _random;
        public RecordController()
        {
            _random = new Random();
        }

        private float GetRandomFloat()
        {
            return _random.Next(100) + 0.51f;
        }
        
        // GET 
        [HttpGet("{broadcasterName}")]
        public async Task<IActionResult> GetLast(string broadcasterName)
        {
            var record = new Record()
            {
                DateTime = DateTime.Now,
                Humidity = GetRandomFloat(),
                Temperature = GetRandomFloat()
            };
            return Ok(record);
        }

        [HttpGet("{broadcasterName}/{begin}/{end}")]
        public async Task<IActionResult> GetRange(string broadcasterName, DateTime begin, DateTime end)
        {
            var records = new List<Record>();
            var timeCounter = begin.Date;
            while (timeCounter <= end)
            {
                records.Add(new Record()
                {
                    DateTime = timeCounter,
                    Humidity = GetRandomFloat(),
                    Temperature = GetRandomFloat()
                });
                timeCounter += TimeSpan.FromSeconds(10); 
            }

            return Ok(records);
        }
    }
}