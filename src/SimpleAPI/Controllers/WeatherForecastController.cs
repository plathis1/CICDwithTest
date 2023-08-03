using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherDbContext _dbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        _logger.LogInformation("GetWeatherForecast called");
        return await _dbContext.WeatherForecasts.ToListAsync();
    }

    [HttpPut("{id}", Name = "PutWeatherForecast")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(int id, WeatherForecast weatherForecast)
    {
        _logger.LogInformation("PutWeatherForecast called");
        if (id != weatherForecast.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(weatherForecast).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}
