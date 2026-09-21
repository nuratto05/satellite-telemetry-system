using System;
using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;

namespace sat_ground_station.Repository;

public class TelemetryRepository : DbContext
{

    public DbSet<Telemetry> Telemetries { get; set; }

    public TelemetryRepository(DbContextOptions options) : base(options)
    {

    }

}
