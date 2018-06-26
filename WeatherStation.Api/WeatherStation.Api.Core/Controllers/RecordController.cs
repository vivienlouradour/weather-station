using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeatherStation.Api.Data.Exceptions;
using WeatherStation.Api.Data.implementation;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Core.Controllers
{
    [Route("weatherstation/api/[controller]")]
    public class RecordController
    {
        // GET 
        [HttpGet]
        public JsonResult GetLast()
        {
            Record record;
            using (WeatherStationDal dal = new WeatherStationDal())
            {
                record = dal.GetLastRecord();
            }

            return new JsonResult(record);
        }

        [HttpGet("{begin}/{end}")]
        public JsonResult GetRange(DateTime begin, DateTime end)
        {
            using (var dal = new WeatherStationDal())
            {
                try
                {
                    var records = dal.GetRecordsByDateRange(begin, end).ToList();
                    return new JsonResult(records);
                }
                catch (ApiException ex)
                {
                    return new JsonResult(new JsonApiResult(ex));
                }
            }

        }

        [HttpPost]
        public JsonResult AddRecord([FromBody]DateTime dateTime, [FromBody]float temperature, [FromBody]float humidity, [FromBody]string broadcasterName)
        {
            using (var dal = new WeatherStationDal())
            {
                try
                {
                    
                    dal.AddRecord(dateTime, temperature, humidity, broadcasterName);
                    return new JsonResult(new JsonApiResult("record added"));
                }
                catch (ApiException ex)
                {
                    return new JsonResult(new JsonApiResult(ex));
                }
            }
        }
        
        
    }
}