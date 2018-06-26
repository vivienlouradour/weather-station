using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            var test = "test";
            return new JsonResult(test);
        } 
        
        
    }
}