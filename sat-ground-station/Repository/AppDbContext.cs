using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;
using sat_ground_station.Models.Enums;
using System;
using System.Xml.Linq;

namespace sat_ground_station.Repository;

public class AppDbContext : DbContext
{

    public DbSet<Telemetry> Telemetries { get; set; }
    public DbSet<Satellite> Satellites { get; set; }
    public DbSet<Mission> Missions { get; set; }

    public DbSet<Alert> Alerts { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Satellite>()
            .HasOne(s => s.Mission)
            .WithMany(m => m.Satellites)
            .HasForeignKey(s => s.MissionId);

        modelBuilder.Entity<Satellite>()
        .Property(s => s.Status)
        .HasConversion<string>();

        modelBuilder.Entity<Mission>()
        .Property(m => m.Status)
        .HasConversion<string>();

        modelBuilder.Entity<Alert>()
        .Property(a => a.Severity)
        .HasConversion<string>();

        modelBuilder.Entity<Alert>()
            .Property(a => a.VelocityLevel)
            .HasConversion<string>();

        modelBuilder.Entity<Alert>()
            .Property(a => a.BatteryLevel)
            .HasConversion<string>();

        modelBuilder.Entity<Alert>()
            .Property(a => a.TemperatureLevel)
            .HasConversion<string>();
        modelBuilder.Entity<Alert>()
            .Property(a => a.SeverityTypes)
            .HasConversion(
                v => v.Select(x => x.ToString()).ToArray(),
                v => v.Select(x => Enum.Parse<SeverityTypes>(x)).ToList()
            );

        //SEED DATA

        modelBuilder.Entity<Mission>().HasData(
            new Mission { Id = 1, Name = "Explorer", Description = "Exploration mission" },
            new Mission { Id = 2, Name = "Weather", Description = "Weather monitoring mission" },
            new Mission { Id = 3, Name = "Communication", Description = "Communication mission" },
            new Mission { Id = 4, Name = "Navigation", Description = "Navigation mission" },
            new Mission { Id = 5, Name = "Science", Description = "Scientific research mission" },
            new Mission { Id = 6, Name = "Imaging", Description = "Earth imaging mission" },
            new Mission { Id = 7, Name = "Tracking", Description = "Tracking mission" },
            new Mission { Id = 8, Name = "Research", Description = "Research mission" },
            new Mission { Id = 9, Name = "Defense", Description = "Monitoring mission" },
            new Mission { Id = 10, Name = "Technology", Description = "Technology demonstration mission" }
        );

        modelBuilder.Entity<Satellite>().HasData(
            new Satellite { Id = 1, SatelliteId = 7854376908376L, SatelliteName = "SAT-100-EXPLORER", MissionId = 1, Country = "US" },
            new Satellite { Id = 2, SatelliteId = 7854376908377L, SatelliteName = "SAT-101-EXPLORER", MissionId = 1, Country = "US" },
            new Satellite { Id = 3, SatelliteId = 7854376908378L, SatelliteName = "SAT-102-WEATHER", MissionId = 2, Country = "US" },
            new Satellite { Id = 4, SatelliteId = 7854376908379L, SatelliteName = "SAT-103-WEATHER", MissionId = 2, Country = "US" },
            new Satellite { Id = 5, SatelliteId = 7854376908380L, SatelliteName = "SAT-104-COMMUNICATION", MissionId = 3, Country = "US" },
            new Satellite { Id = 6, SatelliteId = 7854376908381L, SatelliteName = "SAT-105-COMMUNICATION", MissionId = 3, Country = "US" },
            new Satellite { Id = 7, SatelliteId = 7854376908382L, SatelliteName = "SAT-106-NAVIGATION", MissionId = 4, Country = "US" },
            new Satellite { Id = 8, SatelliteId = 7854376908383L, SatelliteName = "SAT-107-NAVIGATION", MissionId = 4, Country = "US" },
            new Satellite { Id = 9, SatelliteId = 7854376908384L, SatelliteName = "SAT-108-SCIENCE", MissionId = 5, Country = "US" },
            new Satellite { Id = 10, SatelliteId = 7854376908385L, SatelliteName = "SAT-109-SCIENCE", MissionId = 5, Country = "US" },
            new Satellite { Id = 11, SatelliteId = 7854376908386L, SatelliteName = "SAT-110-IMAGING", MissionId = 6, Country = "US" },
            new Satellite { Id = 12, SatelliteId = 7854376908387L, SatelliteName = "SAT-111-IMAGING", MissionId = 6, Country = "US" },
            new Satellite { Id = 13, SatelliteId = 7854376908388L, SatelliteName = "SAT-112-TRACKING", MissionId = 7, Country = "US" },
            new Satellite { Id = 14, SatelliteId = 7854376908389L, SatelliteName = "SAT-113-TRACKING", MissionId = 7, Country = "US" },
            new Satellite { Id = 15, SatelliteId = 7854376908390L, SatelliteName = "SAT-114-RESEARCH", MissionId = 8, Country = "US" },
            new Satellite { Id = 16, SatelliteId = 7854376908391L, SatelliteName = "SAT-115-RESEARCH", MissionId = 8, Country = "US" },
            new Satellite { Id = 17, SatelliteId = 7854376908392L, SatelliteName = "SAT-116-DEFENSE", MissionId = 9, Country = "US" },
            new Satellite { Id = 18, SatelliteId = 7854376908393L, SatelliteName = "SAT-117-DEFENSE", MissionId = 9, Country = "US" },
            new Satellite { Id = 19, SatelliteId = 7854376908394L, SatelliteName = "SAT-118-TECHNOLOGY", MissionId = 10, Country = "US" },
            new Satellite { Id = 20, SatelliteId = 7854376908395L, SatelliteName = "SAT-119-TECHNOLOGY", MissionId = 10, Country = "US" }
        );
    }

}
