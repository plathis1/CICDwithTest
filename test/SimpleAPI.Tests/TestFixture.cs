using Xunit;
using Microsoft.Extensions.Logging;
using SimpleAPI.Controllers;

public class TestFixture : IClassFixture<ILogger<WeatherForecastController>>
{
    private readonly ILogger<WeatherForecastController> _logger;

    public TestFixture(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    public ILogger<WeatherForecastController> GetLoggerInstance()
    {
        return _logger;
    }
}
