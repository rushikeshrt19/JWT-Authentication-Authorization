using Custom_JWT_Token.Helpers;
using Custom_JWT_Token.Models;
using Microsoft.AspNetCore.Mvc;

namespace Custom_JWT_Token.Controllers
{
    [Authorization(Role.Customer)]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
              {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
        private readonly ILogger<WeatherForecastController> _logger;
     
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
           this._logger = logger;
        }
        [HttpGet]
        [Route("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}