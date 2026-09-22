using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sat_ground_station.Models.Dto;
public class UpdateSatelliteObject
{
    public Status? Status { get; set; }
    public int? MissionId { get; set; }
}