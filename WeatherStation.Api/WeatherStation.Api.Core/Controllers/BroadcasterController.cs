using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherStation.Api.Data.implementation;

namespace WeatherStation.Api.Core.Controllers
{        
    [Route("[controller]")]
    public class BroadcasterController : Controller
    {
        private readonly WeatherStationContext _context;
        private readonly ILogger _logger;

        public BroadcasterController(WeatherStationContext context, ILogger<BroadcasterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> GetAll()
        {
            try
            {
                using (var dal = new WeatherStationDal(_context))
                {
                    var broadcasters = await dal.GetAllBroadcastersAsync();
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