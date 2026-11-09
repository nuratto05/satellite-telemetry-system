using Microsoft.AspNetCore.SignalR;
using sat_ground_station.Models;
using sat_ground_station.Service;

namespace sat_ground_station.Network;

public class SignalRServerHub : Hub
{

    private readonly IServiceProvider _provider;

    public SignalRServerHub(IServiceProvider provider)
    {
        _provider = provider;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine( $"Simulator connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Simulator disconnected: {Context.ConnectionId}");

        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task SendPacket(byte[] packet)
    {
        Console.WriteLine($"Packet Recived");

        Telemetry telemetry = PacketDecoder.Decode(packet);

        await using var scope = _provider.CreateAsyncScope();
        var telemetryService = scope.ServiceProvider.GetRequiredService<TelemetryService>();

        await telemetryService.SaveTelemetry(telemetry);
        Console.WriteLine("--------------------------------------------------------");
    }
}
