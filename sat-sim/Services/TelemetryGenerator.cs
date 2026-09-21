using System;

public class TelemetryGenerator
{
	Random random = new Random();

    Dictionary<Satellite, Telemetry> telemetryMap;
	int telCount = 0;

	public TelemetryGenerator()
	{
        telemetryMap = new Dictionary<Satellite, Telemetry>();
	}

	public Telemetry generate(Satellite satallite)
	{
        Telemetry newTel;

        if (!telemetryMap.ContainsKey(satallite))
		{
			newTel = new Telemetry(
				telCount++,
                satallite.Id,
                DateTime.UtcNow,
                random.NextDouble() * 360 - 180, //Longitude
                random.NextDouble() * 180 - 90, // Latitude
                random.Next(1000, 30000), // Velocity mph
				random.Next(-10, 40), // Temperature
                random.Next(50, 101)// Battery level
            );

        }else
		{
            Telemetry prevTel = telemetryMap[satallite];

            newTel = new Telemetry(
                telCount++,
                satallite.Id,
                DateTime.UtcNow,
                Math.Round(prevTel.Longitude + 0.05, 2),
                Math.Round(prevTel.Latitude + 0.05, 2),
                prevTel.Velocity + random.Next(-1000, 1000),
                prevTel.Temperature + random.Next(-2, 3),
                prevTel.BatteryLevel
            );
        }

        telemetryMap[satallite] = newTel;
        return newTel;
	}
}
