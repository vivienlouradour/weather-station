using System;
using Microsoft.EntityFrameworkCore;
using WeatherStation.Api.Data.model;

namespace WeatherStation.Api.Data.implementation
{
    public class WeatherStationContext : DbContext
    {
        public DbSet<Broadcaster> Broadcasters { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WheatherStationRecords.db");
        }
        
    }
}