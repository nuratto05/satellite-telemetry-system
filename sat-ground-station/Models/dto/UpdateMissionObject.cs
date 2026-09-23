using sat_ground_station.Models;
using sat_ground_station.Models.Enums;

namespace sat_ground_station.Models.Dto;
public class UpdateMissionObject
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Status? Status { get; set; }
}