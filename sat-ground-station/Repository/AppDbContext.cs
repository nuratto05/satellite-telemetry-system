using System;
using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;

namespace sat_ground_station.Repository;

public class AppDbContext : DbContext
{

    public DbSet<Telemetry> Telemetries { get; set; }
    public DbSet<Satellite> Satellites { get; set; }
    public DbSet<Mission> Missions { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

}
