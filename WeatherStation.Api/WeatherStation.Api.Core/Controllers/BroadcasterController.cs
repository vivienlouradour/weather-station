using System;
using Microsoft.AspNetCore.Mvc;
using WeatherStation.Api.Data.implementation;

namespace WeatherStation.Api.Core.Controllers
{        
    [Route("weatherstation/api/[controller]")]
    public class BroadcasterController : Controller
    {
        private readonly WeatherStationContext _context;

        public BroadcasterController(WeatherStationContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            try
            {
                using (var dal = new WeatherStationDal(_context))
                {
                    var broadcasters = dal.GetAllBroadcasters();
                    return Ok(broadcasters);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        
    }
}