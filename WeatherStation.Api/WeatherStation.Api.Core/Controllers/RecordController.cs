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
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using WeatherStation.Api.Core.Helpers;
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
        private readonly SimpleLogger _logger;

        public RecordController(WeatherStationContext context)
        {
            _context = context;
            _logger = new SimpleLogger();
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
                    if (record == null)
                        return NotFound();

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
                    dal.AddRecord(record.DateTime, record.Temperature, record.Humidity, record.BroadcasterName);
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