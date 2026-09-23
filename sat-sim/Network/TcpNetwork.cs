using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace sat_sim.Network;
public class TcpNetwork
{
    private TcpClient _client;
    private NetworkStream _stream;

    public TcpNetwork(string ip, int port)
    {
        _client = new TcpClient();

        // Connect simulator to ground station
        _client.Connect(ip, port);

        _stream = _client.GetStream();

        Console.WriteLine($"Connected to Ground Station at {ip}:{port}");
    }

    public void SendTelemetry(Telemetry tel)
    {
        // Telemetry -> JSON
        string json = JsonSerializer.Serialize(tel);

        // Add newline because receiver uses ReadLineAsync()
        string message = json + "\n";

        // JSON -> bytes
        byte[] data = Encoding.UTF8.GetBytes(message);

        // Send to ground station
        _stream.Write(data, 0, data.Length);

        Console.WriteLine($"Data Sent For: {tel.SatelliteId}");
    }
}