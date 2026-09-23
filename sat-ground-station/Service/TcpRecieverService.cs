using sat_ground_station.Network;


namespace sat_ground_station.Service;

public class TcpRecieverService : BackgroundService
{

    private readonly TcpReceiver _receiver;
    private readonly IServiceProvider _provider;

    public TcpRecieverService(IServiceProvider provider)
    {
        //_receiver = new TcpReceiver(int.Parse(Environment.GetEnvironmentVariable("PORT")));
        _provider = provider;
        _receiver = new TcpReceiver(int.Parse(Environment.GetEnvironmentVariable("PORT")), _provider);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _receiver.StartAsync(stoppingToken);
    }
}
