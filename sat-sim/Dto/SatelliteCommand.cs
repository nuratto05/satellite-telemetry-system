using sat_ground_station.Models.Enums;
using sat_sim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sat_sim.Dto;

public class SatelliteCommand
{
    public Satellite Satellite { get; set; }
    public Commands Action { get; set; }
}
