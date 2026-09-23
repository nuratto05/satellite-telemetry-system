using System;

public class TelemetryGenerator
{
	Random random = new Random();

    Dictionary<Satellite, Telemetry> telemetryMap;
    private readonly Dictionary<Satellite, double> orbitAngles;

    public TelemetryGenerator()
	{
        telemetryMap = new Dictionary<Satellite, Telemetry>();
        orbitAngles = new Dictionary<Satellite, double>();
    }

	public Telemetry generate(Satellite satellite)
	{
        if (satellite == null) throw new ArgumentNullException(nameof(satellite));

        Telemetry newTel;

        if (!telemetryMap.ContainsKey(satellite))
		{
            double startingLongitude = random.NextDouble() * 360 - 180;
            double startingAngle = random.NextDouble() * 360;

            orbitAngles[satellite] = startingAngle;

            newTel = new Telemetry(
                satellite.Id,
                DateTime.UtcNow,
                startingLongitude, //Longitude
                CalculateLatitude(startingAngle), // Latitude
                random.Next(400, 600), // Altitude
                random.Next(27000, 28000), // Velocity mph
				random.Next(-10, 40), // Temperature
                100 // Battery level
            );

        }else
		{
            Telemetry prevTel = telemetryMap[satellite];


            // Simulate realistic orbit path
            orbitAngles[satellite] += 2.0;

            if (orbitAngles[satellite] >= 360)
            {
                orbitAngles[satellite] -= 360;
            }

            double newAngle = orbitAngles[satellite];
            double newLongitude = prevTel.Longitude + 2.0;
            double newLatitude = CalculateLatitude(newAngle);
            int newAltitude = prevTel.Altitude + random.Next(-2, 3);
            int newVelocity = prevTel.Velocity + random.Next(-50, 51);
            int newTemperature = prevTel.Temperature + random.Next(-2, 3);
            int newBatteryLevel = prevTel.BatteryLevel + random.Next(-1, 1);

            //Keep within range
            newAltitude = Math.Clamp(newAltitude, 480, 520);
            newVelocity = Math.Clamp(newVelocity, 27000, 28200);
            newTemperature = Math.Clamp(newTemperature, -30, 60);
            newBatteryLevel = Math.Clamp(newBatteryLevel, 10, 100);

            newTel = new Telemetry(
                satellite.Id,
                DateTime.UtcNow,
                newLongitude,
                newLatitude,
                newAltitude,
                newVelocity,
                newTemperature,
                newBatteryLevel
            );
        }

        telemetryMap[satellite] = newTel;
        return newTel;
	}
    private double CalculateLatitude(double angle)
    {
        double maximumLatitude = 60;

        return maximumLatitude * Math.Sin(angle * Math.PI / 180);
    }
}
