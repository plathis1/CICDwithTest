using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

namespace SimpleAPI.Models
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

         public WeatherDbContext() { }

        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; } = default!;
        
    }
}