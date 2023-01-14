using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[] { "World!!!!", "Hello", };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Get", Name = "GetWeatherForecast")]
    public string Get()
    {
        return Summaries.FirstOrDefault();
    }

    [HttpGet("GetAll", Name = "GetAll")]
    public IEnumerable<WeatherForecast> GetAll()
    {
        return Summaries
            .Select((summary, index) => new WeatherForecast() { Summary = summary })
            .ToArray();
    }
}
