using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;
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
        new Satellite { Id = 1, SatelliteId = "SAT-001-Explorer", MissionId = 1, Country = "US" },
        new Satellite { Id = 2, SatelliteId = "SAT-002-Explorer", MissionId = 1, Country = "US" },
        new Satellite { Id = 3, SatelliteId = "SAT-003-Weather", MissionId = 2, Country = "US" },
        new Satellite { Id = 4, SatelliteId = "SAT-004-Weather", MissionId = 2, Country = "US" },
        new Satellite { Id = 5, SatelliteId = "SAT-005-Communication", MissionId = 3, Country = "US" },
        new Satellite { Id = 6, SatelliteId = "SAT-006-Communication", MissionId = 3, Country = "US" },
        new Satellite { Id = 7, SatelliteId = "SAT-007-Navigation", MissionId = 4, Country = "US" },
        new Satellite { Id = 8, SatelliteId = "SAT-008-Navigation", MissionId = 4, Country = "US" },
        new Satellite { Id = 9, SatelliteId = "SAT-009-Science", MissionId = 5, Country = "US" },
        new Satellite { Id = 10, SatelliteId = "SAT-010-Science", MissionId = 5, Country = "US" },
        new Satellite { Id = 11, SatelliteId = "SAT-011-Imaging", MissionId = 6, Country = "US" },
        new Satellite { Id = 12, SatelliteId = "SAT-012-Imaging", MissionId = 6, Country = "US" },
        new Satellite { Id = 13, SatelliteId = "SAT-013-Tracking", MissionId = 7, Country = "US" },
        new Satellite { Id = 14, SatelliteId = "SAT-014-Tracking", MissionId = 7, Country = "US" },
        new Satellite { Id = 15, SatelliteId = "SAT-015-Research", MissionId = 8, Country = "US" },
        new Satellite { Id = 16, SatelliteId = "SAT-016-Research", MissionId = 8, Country = "US" },
        new Satellite { Id = 17, SatelliteId = "SAT-017-Defense", MissionId = 9, Country = "US" },
        new Satellite { Id = 18, SatelliteId = "SAT-018-Defense", MissionId = 9, Country = "US" },
        new Satellite { Id = 19, SatelliteId = "SAT-019-Technology", MissionId = 10, Country = "US" },
        new Satellite { Id = 20, SatelliteId = "SAT-020-Technology", MissionId = 10, Country = "US" }
    );
    }

}
