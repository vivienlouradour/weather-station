using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using WeatherStation.Api.Core.Helpers;
using WeatherStation.Api.Data.contract;
using WeatherStation.Api.Data.Exceptions;
using WeatherStation.Api.Data.implementation;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Core.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    public class RecordController : Controller
    {
        private readonly WeatherStationContext _context;
        private readonly SimpleLogger _logger;

        public RecordController(WeatherStationContext context)
        {
            _context = context;
            _logger = new SimpleLogger();
        }

        // GET 
        [HttpGet("{broadcasterName}")]
        public async Task<IActionResult> GetLast(string broadcasterName)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    var record = await dal.GetLastRecordAsync(broadcasterName);
                    return Ok(record);
                }
                catch (BroadcasterNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (ApiArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{broadcasterName}/{begin}/{end}")]
        public async Task<IActionResult> GetRange(string broadcasterName, DateTime begin, DateTime end)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    var records = await dal.GetRecordsByDateRangeAsync(broadcasterName, begin, end);
                    return Ok(records);
                }
                catch (BroadcasterNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (ApiArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpGet("{broadcasterName}/hottest")]
        public async Task<IActionResult> GetHottestRecord(string broadcasterName)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    var hottestRecord = await dal.GetHottestRecordAsync(broadcasterName);
                    return Ok(hottestRecord);
                }
                catch (BroadcasterNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (ApiArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{broadcasterName}/coldest")]
        public async Task<IActionResult> GetColdestRecord(string broadcasterName)
        {
            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    var coldestRecord = await dal.GetColdestRecordAsync(broadcasterName);
                    return Ok(coldestRecord);
                }
                catch (BroadcasterNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (ApiArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Route("seed")]
        public async Task AddBouchon()
        {
            using (var dal = new WeatherStationDal(_context))
            {
                float temp = 12.5f;
                float hum = 45f;
                DateTime dateTime = DateTime.Now;
                await dal.AddRecordAsync(dateTime.AddMinutes(10), temp, hum, "broadcasterTesta");
                await dal.AddRecordAsync(dateTime.AddMinutes(10), temp, hum, "broadcasterTest");
                await dal.AddRecordAsync(dateTime.AddMinutes(10), temp, hum, "broadcasterTest");
                await dal.AddRecordAsync(dateTime.AddMinutes(10), temp, hum, "broadcasterTest");
            }
        }

        //[AllowAnonymous] //TODO: update SensorRecorder to use secure API
        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody] PostRecord record)
        {
            _logger.Info("posting record : " + record);
            if (record == null)
            {
                _logger.Info("Record is null (not created).");
                return BadRequest();
            }

            using (var dal = new WeatherStationDal(_context))
            {
                try
                {
                    await dal.AddRecordAsync(record.DateTime, record.Temperature, record.Humidity, record.BroadcasterName);
                    _logger.Info("Record created.");
                    return Created(String.Empty, null);
                }
                catch (ApiException ex)
                {
                    string exMessage = ex.Message + Environment.NewLine;
                    _logger.Error("Error creating record : " + exMessage);
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    string exMessage = ex.Message + Environment.NewLine;
                    while (ex.InnerException != null)
                    {
                        exMessage += $"InnerException : {ex.InnerException.Message}{Environment.NewLine}";
                        ex = ex.InnerException;
                    }
                    _logger.Error("Error creating record : " + exMessage);
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

            public override string ToString()
            {
                return
                    $"Date : {DateTime.ToString()} ; Temp : {Temperature} ; Hum : {Humidity} ; Broadcaster : {BroadcasterName}";
            }
        }
    }
}