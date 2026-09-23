using sat_ground_station.Models;
using sat_ground_station.Repository;
using sat_ground_station.Service;

namespace sat_ground_station.Service;
public class TelemetryService
{
    private readonly AppDbContext _dbContext;
    private readonly CheckSeverity checkSeverity;
    public TelemetryService(AppDbContext dbContext, CheckSeverity checkSeverity)
    {
        _dbContext = dbContext;
        this.checkSeverity = checkSeverity;
    }

    public async Task SaveTelemetry(Telemetry telemetry)
    {
        if(telemetry == null)
        {
            return;
        }

        Alert? alert = checkSeverity.CheckLevels(telemetry);

        if(alert != null)
        {
            Console.WriteLine($"Alert Created for: {telemetry.SatelliteId} Severity Level : {alert.Severity}");
            _dbContext.Alerts.Add(alert);
        }

        Console.WriteLine("Before SaveChanges");
        _dbContext.Telemetries.Add(telemetry);
        _dbContext.SaveChanges();
        Console.WriteLine("After SaveChanges");


        Console.WriteLine($"Data Verified for Satellite: {telemetry.SatelliteId}");

    }
}
