using sat_ground_station.Models;
using sat_ground_station.Service;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace sat_ground_station.Network;

public class TcpReceiver
{
    private readonly int _port;
    private readonly IServiceProvider _provider;


    public TcpReceiver(int port, IServiceProvider provider)
    {
        _port = port;
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var listener = new TcpListener(IPAddress.Any, _port);

        listener.Start();

        Console.WriteLine($"Ground Station listening on TCP port {_port}");

        while (!cancellationToken.IsCancellationRequested)
        {
            TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken);

            Console.WriteLine("Simulator connected!");

            // Handle the simulator connection
            await ReceiveTelemetryAsync(client, cancellationToken);
        }

        listener.Stop();
    }

    private async Task ReceiveTelemetryAsync(TcpClient client, CancellationToken cancellationToken)
    {
        using (client)
        using (NetworkStream stream = client.GetStream())
        using (StreamReader reader = new StreamReader( stream, Encoding.UTF8))
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Read until the \n sent by the simulator
                string? json = await reader.ReadLineAsync(cancellationToken);

                // Connection was closed
                if (json == null)
                {
                    Console.WriteLine("Simulator disconnected.");
                    break;
                }
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine($"Data Received");

                // JSON -> Telemetry
                Telemetry? telemetry = JsonSerializer.Deserialize<Telemetry>(json);

                if (telemetry == null)
                {
                    Console.WriteLine("Invalid telemetry.");
                    continue;
                }

                await using var scope = _provider.CreateAsyncScope();
                var telemetryService = scope.ServiceProvider.GetRequiredService<TelemetryService>();

                await telemetryService.SaveTelemetry(telemetry);
                Console.WriteLine("--------------------------------------------------------");
            }
        }
    }
}