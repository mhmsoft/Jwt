using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IJwtAuthenticateManager _jwtAuthenticateManager;
        public WeatherForecastController(IJwtAuthenticateManager jwtAuthenticateManager)
        {
            _jwtAuthenticateManager = jwtAuthenticateManager;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.
                Length)]
            })
            .ToArray();
        }
        [HttpGet("test")]
        public string test()
        {
            return "naber dostum";
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(InputParam param)
        {
           var token= _jwtAuthenticateManager.Authenticate(param.uname, param.passw);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
