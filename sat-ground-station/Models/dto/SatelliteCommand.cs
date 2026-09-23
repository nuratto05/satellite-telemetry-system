using sat_ground_station.Models;
using sat_ground_station.Models.Enums;

namespace sat_sim.Dto;
public class SatelliteCommand
{
    public Satellite Satellite { get; set; }
    public Commands Action { get; set; }

    public SatelliteCommand(Satellite satellite, Commands action)
    {
        Satellite = satellite;
        Action = action;
    }
}
