using sat_ground_station.Models;
using sat_ground_station.Models.Enums;

namespace sat_sim.Dto;
public class SatelliteCommand
{
    public Satellite Satellite { get; private set; }
    public Commands Action { get; private set; }

    public SatelliteCommand(Satellite satellite, Commands action)
    {
        Satellite = satellite;
        Action = action;
    }
}
