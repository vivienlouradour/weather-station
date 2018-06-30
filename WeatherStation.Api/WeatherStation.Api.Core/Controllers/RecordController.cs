using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using WeatherStation.Api.Data.contract;
using WeatherStation.Api.Data.Exceptions;
using WeatherStation.Api.Data.implementation;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Core.Controllers
{
    [Route("weatherstation/api/[controller]")]
    public class RecordController : Controller
    {
        private readonly WeatherStationContext _context;

        public RecordController(WeatherStationContext context)
        {
            _context = context;
        }

        // GET 
        [HttpGet("{broadcasterName}")]
        public IActionResult GetLast(string broadcasterName)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    var record = dal.GetLastRecord(broadcasterName);
                    if(record == null)
                        return NotFound(record);
            
                    return Ok(record);        
                }
                catch (ApiArgumentException ex)
                {
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult(500);
                }
            }
        }

        [HttpGet("{broadcasterName}/{begin}/{end}")]
        public IActionResult GetRange(string broadcasterName, DateTime begin, DateTime end)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    var records = dal.GetRecordsByDateRange(broadcasterName, begin, end).ToList();
                    if (!records.Any())
                        return NotFound();
                    
                    return Ok(records);
                }
                catch (ApiArgumentException ex)
                {
                    return BadRequest();
                }
            }

        }

        [Route("seed")]
        public void AddBouchon()
        {
            using (var dal = new WeatherStationDal(_context))
            {
                float temp = 12.5f;
                float hum = 45f;
                DateTime dateTime = DateTime.Now;
                dal.AddRecord(dateTime.AddMinutes(10), temp, hum, "broadcasterTesta");
                dal.AddRecord(dateTime.AddMinutes(10), temp, hum, "broadcasterTest");
                dal.AddRecord(dateTime.AddMinutes(10), temp, hum, "broadcasterTest");
                dal.AddRecord(dateTime.AddMinutes(10), temp, hum, "broadcasterTest");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody]PostRecord record)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                if (record == null)
                    return BadRequest();
                try
                {
                    dal.AddRecord(record.DateTime, record.Temperature, record.Humidity, record.BroadcasterName);
                    return Created(String.Empty, null);
                }
                catch (ApiException ex)
                {
                    return BadRequest();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
        }

        public class PostRecord
        {
            public DateTime DateTime { get; set; }
            public float Temperature { get; set; }
            public float Humidity { get; set; }
            public string BroadcasterName { get; set; }
        }
        
        
    }
}