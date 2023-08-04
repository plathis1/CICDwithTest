using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configurationManager = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WeatherDbContext>(opt => opt.UseSqlServer(configurationManager.GetConnectionString("WeatherConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("weatherforecast", async (WeatherForecast weatherForecast, WeatherDbContext dbContext) =>
{
    dbContext.WeatherForecasts.Add(weatherForecast);
    await dbContext.SaveChangesAsync();
    return Results.CreatedAtRoute("GetWeatherForecast", new { id = weatherForecast.Id }, weatherForecast);
}).Produces<WeatherForecast>(StatusCodes.Status201Created);

app.Run();
