using System;

public class AlertGenerator
{
    int alertCount = 0;

    public AlertGenerator()
    {
    }

    // Returns an Alert if severity is greater than moderate
    public Alert? Generate(Telemetry telemetry)
    {
        if (telemetry == null) throw new ArgumentNullException(nameof(telemetry));

        Alert alert = new Alert
        (
            alertCount++,
            telemetry.SatelliteId,
            telemetry.Timestamp
        );

        SeverityStatus aggregated = SeverityStatus.Moderate;

        CheckVelocity(alert, telemetry, ref aggregated);
        CheckBatteryLevel(alert, telemetry, ref aggregated);
        CheckTemperature(alert, telemetry, ref aggregated);

        alert.Severity = aggregated;

        if (aggregated == SeverityStatus.Moderate) return null;

        return alert;
    }

    private void CheckVelocity(Alert alert, Telemetry telemetry, ref SeverityStatus aggregated)
    {

        int velocity = telemetry.Velocity;

        if (velocity <= 16000)
        {
            alert.VelocityLevel = SeverityStatus.Severe; // TOO SLOW STARTS TO DE-ORBIT
            aggregated = MaxSeverity(aggregated, SeverityStatus.Severe);
        }
        else if (velocity >= 27000)
        {
            alert.VelocityLevel = SeverityStatus.Critical; // TOO FAST LOST TO DEEP SPACE
            aggregated = MaxSeverity(aggregated, SeverityStatus.Critical);
        }
    }

    private void CheckBatteryLevel(Alert alert, Telemetry telemetry, ref SeverityStatus aggregated)
    {

        int batteryLevel = telemetry.BatteryLevel;

        if (batteryLevel <= 30)
        {
            alert.BatteryLevel = SeverityStatus.Critical; // BATTERY IS TOO LOW TO POWER THRUSTERS
            aggregated = MaxSeverity(aggregated, SeverityStatus.Critical);
        }else if (batteryLevel <= 50)
        {
            alert.BatteryLevel = SeverityStatus.Severe; // BATTERY IS ENTERING DANGEROUS LEVELS
            aggregated = MaxSeverity(aggregated, SeverityStatus.Severe);
        }
    }

    private void CheckTemperature(Alert alert, Telemetry telemetry, ref SeverityStatus aggregated)
    {

        int temperature = telemetry.Temperature;

        if (temperature <= -65 || temperature >= 120)
        {
            alert.TemperatureLevel = SeverityStatus.Critical; // SATELLITE ENTERS LOW POWER MODE 
            aggregated = MaxSeverity(aggregated, SeverityStatus.Critical);
        }
        else if (temperature <= -20 || temperature >= 60)
        {
            alert.TemperatureLevel = SeverityStatus.Severe; // CAUSES PHYSICAL DAMAGE TO SATELLITE
            aggregated = MaxSeverity(aggregated, SeverityStatus.Severe);
        }
    }

    private static SeverityStatus MaxSeverity(SeverityStatus a, SeverityStatus b)
    {
        return (SeverityStatus)Math.Max((int)a, (int)b);
    }
}
