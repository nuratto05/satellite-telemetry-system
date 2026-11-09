using sat_ground_station.Models;
using sat_ground_station.Repository;
using sat_ground_station.Network;
using Microsoft.AspNetCore.SignalR;

namespace sat_ground_station.Service;
public class TelemetryService
{
    private readonly IHubContext<SignalRServerHub> _hubContext;
    private readonly AppDbContext _dbContext;
    private readonly CheckSeverity checkSeverity;
    public TelemetryService(AppDbContext dbContext, CheckSeverity checkSeverity, IHubContext<SignalRServerHub> hubContext)
    {
        _hubContext = hubContext;
        _dbContext = dbContext;
        this.checkSeverity = checkSeverity;
    }

    public async Task SaveTelemetry(Telemetry telemetry)
    {
        if(telemetry == null)
        {
            return;
        }

        _dbContext.Telemetries.Add(telemetry);
        _dbContext.SaveChanges();
        await _hubContext.Clients.All.SendAsync("SendTelemetry", telemetry);

        Alert? alert = checkSeverity.CheckLevels(telemetry);

        if (alert != null)
        {
            Console.WriteLine($"Alert Created for: {telemetry.SatelliteId} Severity Level : {alert.Severity}");
            _dbContext.Alerts.Add(alert);
            _dbContext.SaveChanges();
            await _hubContext.Clients.All.SendAsync("SendAlert", alert);
        }



        Console.WriteLine($"Data Verified for Satellite: {telemetry.SatelliteId}");

    }
}
