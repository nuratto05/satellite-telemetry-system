using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using sat_sim.Dto;
using sat_sim.Models;
using sat_sim.Models.Enums;
using sat_sim.Services;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;

DotNetEnv.Env.Load();
DotNetEnv.Env.TraversePath().Load();

var signaleRendpointKey = (string)Environment.GetEnvironmentVariable("SIGNALR_ENDPOINT_KEY");

Dictionary<int, Satellite> satellites = new Dictionary<int, Satellite>();

using HttpClient httpClient = new HttpClient();

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    Converters =
    {
        new JsonStringEnumConverter()
    }
};

var satelliteList = await httpClient.GetFromJsonAsync<List<Satellite>>(
    "http://localhost:5142/api/satellite", options
);

if (satelliteList != null)
{
    int i = 0;
    foreach (Satellite satellite in satelliteList)
    {
        satellites.Add(i++, satellite);
        Console.WriteLine($"ID: {satellite.Id}");
        Console.WriteLine($"Satellite ID: {satellite.SatelliteId}");
        Console.WriteLine($"Satellite Name: {satellite.SatelliteName}");
    }
}


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
                    satActive.Activate();
                }
                Console.WriteLine($"Activate satellite: {command.Satellite.Id}");
                break;

            case Commands.Deactivate:
                if (satellites.TryGetValue(command.Satellite.Id, out Satellite satDeactivate))
                {
                    satDeactivate.Deactivate();
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

    var order = satellites.Values.ToArray();
    Random.Shared.Shuffle(order);

    foreach (Satellite sat in order)
    {

        if (sat.Status == Status.Active)
        {
            activeSatellites++;
            Telemetry telemetry = generator.generate(sat);
            byte[] packet = PacketEncoder.EncodeTelemetry(telemetry);
            await connection.InvokeAsync("SendPacket", packet);

            await Task.Delay(Random.Shared.Next(100, 1000));
        }

    }
    Console.WriteLine($"Data Points Sent {activeSatellites}");
    Console.WriteLine("--------------------------------------------------------");

    await Task.Delay(5000);
}