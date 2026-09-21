using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace sat_ground_station.Models;

[Table("Telemetry")]
public class Telemetry
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("id")]
	public int Id {  get; set; }

    [Required]
    [Column("satalliteId")]
	public int SatelliteId {  get; set; }

	[Required]
    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    [Required]
    [Column("longitude")]
    public double Longitude { get; set; }

    [Required]
    [Column("latitude")]
    public double Latitude { get; set; }

    [Required]
    [Column("altitude")]
    public int Altitude { get; set; }

    [Required]
    [Column("velocity")]
    public int Velocity { get; set; }

    [Required]
    [Column("temperature")]
    public int Temperature { get; set; }

    [Required]
    [Column("batteryLevel")]
    public int BatteryLevel { get; set; }

	public Telemetry() { }

	public Telemetry(int satelliteId, DateTime timestamp, double longitude, double latitude, int altitude, int velocity, int temperture, int batteryLevel)
	{
		this.SatelliteId = satelliteId;
		this.Timestamp = timestamp;
		this.Longitude = longitude;
		this.Latitude = latitude;
		this.Altitude = altitude;
		this.Velocity = velocity;
		this.Temperature = temperture;
		this.BatteryLevel = batteryLevel;
	}
}