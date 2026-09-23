using sat_ground_station.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace sat_ground_station.Models;
[Table("Satellite")]
public class Satellite
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("satelliteId")]
    public string SatelliteId { get; set; }

    [Column("status")]
    public Status? Status { get; set; } = Enums.Status.Active;

    [Required]
    [Column("missionId")]
    public int MissionId { get; set; }

    [Required]
    [Column("Country")]
    public string Country { get; set; }

    [JsonIgnore]
    public Mission? Mission { get; set; }

}