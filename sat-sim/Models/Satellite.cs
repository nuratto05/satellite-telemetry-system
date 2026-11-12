using sat_sim.Models;
using sat_sim.Models.Enums;
using System;
using System.Reflection;

namespace sat_sim.Models;
public class Satellite
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public long SatelliteId { get; set; }
    public string SatelliteName { get; set; }
    public int MissionId { get; set; }
    public string Country { get; set; }
    public Mission? Mission { get; set; }

    public void Activate() { Status = Status.Active; }
    public void Deactivate() { Status = Status.Inactive; }
}
