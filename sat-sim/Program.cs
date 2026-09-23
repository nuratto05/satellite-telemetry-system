using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using sat_ground_station.Models.Enums;
using sat_sim.Dto;
using sat_sim.Models;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;

DotNetEnv.Env.Load();
DotNetEnv.Env.TraversePath().Load();

var signaleRendpointKey = (string)Environment.GetEnvironmentVariable("SIGNALR_ENDPOINT_KEY");

Dictionary<int, Satellite> satellites = new Dictionary<int, Satellite>
{
    { 1, new Satellite(1, Status.Active) },
    { 2, new Satellite(2, Status.Inactive) },
    { 3, new Satellite(3, Status.Inactive) },
    { 4, new Satellite(4, Status.Inactive) },
    { 5, new Satellite(5, Status.Inactive) },
    { 6, new Satellite(6, Status.Inactive) },
    { 7, new Satellite(7, Status.Inactive) },
    { 8, new Satellite(8, Status.Inactive) },
    { 9, new Satellite(9, Status.Inactive) },
    { 10, new Satellite(10, Status.Inactive) },
    { 11, new Satellite(11, Status.Inactive) },
    { 12, new Satellite(12, Status.Inactive) },
    { 13, new Satellite(13, Status.Inactive) },
    { 14, new Satellite(14, Status.Inactive) },
    { 15, new Satellite(15, Status.Inactive) },
    { 16, new Satellite(16, Status.Inactive) },
    { 17, new Satellite(17, Status.Inactive) },
    { 18, new Satellite(18, Status.Inactive) },
    { 19, new Satellite(19, Status.Inactive) },
    { 20, new Satellite(20, Status.Inactive) },
    { 21, new Satellite(21, Status.Inactive) },
    { 22, new Satellite(22, Status.Inactive) },
    { 23, new Satellite(23, Status.Inactive) },
};


TelemetryGenerator generator = new TelemetryGenerator();

var connection = new HubConnectionBuilder()
    .WithUrl($"http://localhost:5142/simulationHub/{signaleRendpointKey}")
    .WithAutomaticReconnect()
    .Build();

connection.On<SatelliteCommand>( "ModifySatellite", command =>
    {

        switch (command.Action)
        {
            case Commands.Activate:
            if (satellites.TryGetValue(command.Satellite.Id, out Satellite satActive))
                {
                    satActive.Status = Status.Active;
                }
                Console.WriteLine($"Activate satellite: {command.Satellite.Id}");
                break;

            case Commands.Deactivate:
                if (satellites.TryGetValue(command.Satellite.Id, out Satellite satDeactivate))
                {
                    satDeactivate.Status = Status.Inactive;
                }
                Console.WriteLine($"Deactivate satellite: {command.Satellite.Id}");
                break;

            case Commands.Delete:
                satellites.Remove(command.Satellite.Id);
                Console.WriteLine($"Deleted satellite: {command.Satellite.Id}");
                break;

            case Commands.Add:
                satellites[command.Satellite.Id] = command.Satellite;
                Console.WriteLine($"Added satellite: {command.Satellite.Id}");
                break;
        }
    }
);

await connection.StartAsync();
Console.WriteLine("Connected to backend.");

while (true)
{

    Console.WriteLine("--------------------------------------------------------");
    int activeSatellites = 0;
    foreach (Satellite sat in satellites.Values)
    {
        if(sat.Status == Status.Active)
        {
            activeSatellites++;
            Telemetry telemetry = generator.generate(sat);
            await connection.InvokeAsync("SendTelemetry", telemetry);
        }

    }
    Console.WriteLine($"Data Points Sent {activeSatellites}");
    Console.WriteLine("--------------------------------------------------------");

    Thread.Sleep(5000);
}