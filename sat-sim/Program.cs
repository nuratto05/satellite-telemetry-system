using sat_sim.Network;
using System.Net;
using System.Reflection;

DotNetEnv.Env.Load();
DotNetEnv.Env.TraversePath().Load();

List<Satellite> satellites = new List<Satellite>
{
    new Satellite(1, "SAT-001-Explorer"),
    new Satellite(2, "SAT-002-Explorer"),
    new Satellite(3, "SAT-003-Weather"),
    new Satellite(4, "SAT-004-Weather"),
    new Satellite(5, "SAT-005-Communication"),
    new Satellite(6, "SAT-006-Communication"),
    new Satellite(7, "SAT-007-Navigation"),
    new Satellite(8, "SAT-008-Navigation"),
    new Satellite(9, "SAT-009-Science"),
    new Satellite(10, "SAT-010-Science"),
    new Satellite(11, "SAT-011-Imaging"),
    new Satellite(12, "SAT-012-Imaging"),
    new Satellite(13, "SAT-013-Tracking"),
    new Satellite(14, "SAT-014-Tracking"),
    new Satellite(15, "SAT-015-Research"),
    new Satellite(16, "SAT-016-Research"),
    new Satellite(17, "SAT-017-Defense"),
    new Satellite(18, "SAT-018-Defense"),
    new Satellite(19, "SAT-019-Technology"),
    new Satellite(20, "SAT-020-Technology")
};


TelemetryGenerator generator = new TelemetryGenerator();

int port = int.Parse(Environment.GetEnvironmentVariable("PORT"));
string ip = Environment.GetEnvironmentVariable("IP_ADDRESS");

TcpNetwork network = new TcpNetwork(ip, port);

while (true)
{

    Console.WriteLine("--------------------------------------------------------");
    foreach (Satellite sat in satellites)
    {
        Telemetry telemetry = generator.generate(sat);

        network.SendTelemetry(telemetry);
    }
    Console.WriteLine("--------------------------------------------------------");

    Thread.Sleep(5000);
}

Console.ReadLine();