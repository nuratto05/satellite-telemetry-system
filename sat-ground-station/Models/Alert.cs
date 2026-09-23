using sat_ground_station.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sat_ground_station.Models;
[Table("Alert")]
public class Alert
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id {  get; set; }

    [Required]
    [Column("satelliteId")]
    public int SatelliteId { get; set; }

    [Required]
    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    [Required]
    [Column("severity")]
    public SeverityStatus? Severity { get; set; } = SeverityStatus.Moderate;

    [Column("velocityLevel")]
    public SeverityStatus? VelocityLevel { get; set; } = SeverityStatus.Moderate;

    [Column("temperatureLevel")]
    public SeverityStatus? TemperatureLevel { get; set; } = SeverityStatus.Moderate;

    [Column("batteryLevel")]
    public SeverityStatus? BatteryLevel { get; set; } = SeverityStatus.Moderate;

} 
