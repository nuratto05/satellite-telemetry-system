using System;
using sat_ground_station.Models;

namespace sat_ground_station.Service;

public class CheckSeverity
{

    public Alert? CheckLevels(Telemetry telemetry)
    {
        if (telemetry == null) throw new ArgumentNullException(nameof(telemetry));

        Alert alert = new Alert();
        SeverityStatus aggregated = SeverityStatus.Moderate;

        CheckVelocity(alert, telemetry, ref aggregated);
        CheckBatteryLevel(alert, telemetry, ref aggregated);
        CheckTemperature(alert, telemetry, ref aggregated);

        alert.Severity = aggregated;

        if (aggregated == SeverityStatus.Moderate)
        {
            return null;
        }

        alert.Timestamp = DateTime.UtcNow;
        return alert;
    }

    private void CheckVelocity(Alert alert, Telemetry telemetry, ref SeverityStatus aggregated)
    {

        int velocity = telemetry.Velocity;
        alert.Severity = SeverityStatus.Moderate;

        if (velocity <= 16000) // TOO SLOW STARTS TO DE-ORBIT
        {
            alert.VelocityLevel = SeverityStatus.Severe;
            aggregated = MaxSeverity(aggregated, SeverityStatus.Severe);
        }
        else if (velocity >= 27000)// TOO FAST LOST TO DEEP SPACE
        {
            alert.VelocityLevel = SeverityStatus.Critical;
            aggregated = MaxSeverity(aggregated, SeverityStatus.Critical);
        }
    }

    private void CheckBatteryLevel(Alert alert, Telemetry telemetry, ref SeverityStatus aggregated)
    {

        int batteryLevel = telemetry.BatteryLevel;
        alert.Severity = SeverityStatus.Moderate;

        if (batteryLevel <= 30)// BATTERY IS TOO LOW TO POWER THRUSTERS
        {
            alert.BatteryLevel = SeverityStatus.Critical;
            aggregated = MaxSeverity(aggregated, SeverityStatus.Critical);
        }
        else if (batteryLevel <= 50)// BATTERY IS ENTERING DANGEROUS LEVELS
        {
            alert.BatteryLevel = SeverityStatus.Severe;
            aggregated = MaxSeverity(aggregated, SeverityStatus.Severe);
        }
    }

    private void CheckTemperature(Alert alert, Telemetry telemetry, ref SeverityStatus aggregated)
    {

        int temperature = telemetry.Temperature;
        alert.Severity = SeverityStatus.Moderate;

        if (temperature <= -65 || temperature >= 120)// SATELLITE ENTERS LOW POWER MODE 
        {
            alert.TemperatureLevel = SeverityStatus.Critical;
            aggregated = MaxSeverity(aggregated, SeverityStatus.Critical);
        }
        else if (temperature <= -20 || temperature >= 60) // CAUSES PHYSICAL DAMAGE TO SATELLITE
        {
            alert.TemperatureLevel = SeverityStatus.Severe;
            aggregated = MaxSeverity(aggregated, SeverityStatus.Severe);
        }
    }

    private static SeverityStatus MaxSeverity(SeverityStatus a, SeverityStatus b)
    {
        return (SeverityStatus)Math.Max((int)a, (int)b);
    }

}
