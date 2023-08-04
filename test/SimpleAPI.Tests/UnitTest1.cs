using Microsoft.Extensions.Logging;
using SimpleAPI.Controllers;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using SimpleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SimpleAPI.Tests;

public class UnitTest1
{    
    private readonly Mock<ILogger<WeatherForecastController>> _logger;
    private readonly Mock<WeatherDbContext> _dbContext;
    public static List<WeatherForecast> GetFakeEmployeeList()
    {
        return new List<WeatherForecast>()
        {
            new WeatherForecast { Id = 1, TemperatureC = 90, Summary = "Hot" , Date = DateTime.Now},
            new WeatherForecast { Id = 2, TemperatureC = 39, Summary = "cold" , Date = DateTime.Now},
            new WeatherForecast { Id = 3, TemperatureC = 75, Summary = "moderate" , Date = DateTime.Now}
        };
    }

    public UnitTest1()
    {
        this._logger = new Mock<ILogger<WeatherForecastController>>();
        this._dbContext = new Mock<WeatherDbContext>();
    }

    [Fact]
    public async Task Test1()
    {
        var sampleData = new List<WeatherForecast>()
        {
            new WeatherForecast { Id = 1, TemperatureC = 90, Summary = "Hot" , Date = DateTime.Now},
            new WeatherForecast { Id = 2, TemperatureC = 39, Summary = "cold" , Date = DateTime.Now},
            new WeatherForecast { Id = 3, TemperatureC = 75, Summary = "moderate" , Date = DateTime.Now}
        };

        // Set up the DbSet mock to return the sample data when queried
        this._dbContext.Setup<DbSet<WeatherForecast>>(db => db.WeatherForecasts).ReturnsDbSet(sampleData);

        var controller = new WeatherForecastController(this._logger.Object, this._dbContext.Object);
        IEnumerable<WeatherForecast>? result = (await controller.Get()).Value;

        // Assert
        Assert.Equal(sampleData.Count, result?.Count());
    }

    [Fact]
    public async Task Test1_put()
    {
        var sampleData = new List<WeatherForecast>()
        {
            new WeatherForecast { Id = 1, TemperatureC = 90, Summary = "Hot" , Date = DateTime.Now},
            new WeatherForecast { Id = 2, TemperatureC = 39, Summary = "cold" , Date = DateTime.Now},
            new WeatherForecast { Id = 3, TemperatureC = 75, Summary = "moderate" , Date = DateTime.Now}
        };

        // Set up the DbSet mock to return the sample data when queried
        this._dbContext.Setup<DbSet<WeatherForecast>>(db => db.WeatherForecasts).ReturnsDbSet(sampleData);
        WeatherForecast sample_put= new() { TemperatureC = 43, Summary = " Second cold" , Date = DateTime.Now};


        var controller = new WeatherForecastController(this._logger.Object, this._dbContext.Object);
        IActionResult result = await controller.Put(2,sample_put);

        // Assert
        Assert.Equal(typeof(BadRequestResult), result.GetType());
    }

    [Theory]
    [InlineData("lathihs")]
    public void Test1_getById(string value)
    {
        var controller = new WeatherForecastController(this._logger.Object, this._dbContext.Object);
        string result = controller.GetById(value);

        // Assert
        Assert.Equal(result, "GetWeatherForecastById called "+ value);
    }
    
}