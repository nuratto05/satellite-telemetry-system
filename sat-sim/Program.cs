Satellite satellite = new Satellite(1, "Satellite-1", "active");

TelemetryGenerator generator = new TelemetryGenerator();

Telemetry telemetry = generator.generate(satellite);

while (true)
{
    Thread.Sleep(5000);
    telemetry = generator.generate(satellite);
    Console.WriteLine($"Satellite: {telemetry.SatelliteId}");
    Console.WriteLine($"Longitude: {telemetry.Longitude}");
    Console.WriteLine($"Latitude: {telemetry.Latitude}");
    Console.WriteLine($"Velocity: {telemetry.Velocity}");
    Console.WriteLine($"Temperature: {telemetry.Temperature}");
    Console.WriteLine($"Battery: {telemetry.BatteryLevel}");
}

Console.ReadLine();