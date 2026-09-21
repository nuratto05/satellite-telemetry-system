using System;

public class Telemetry
{
	public int Id {  get; set; }
	public int SatelliteId {  get; set; }
	public DateTime Timestamp { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }
	public int Altitude { get; set; }
	public int Velocity { get; set; }
	public int Temperature { get; set; }
	public int BatteryLevel { get; set; }

	public Telemetry(int id, int satelliteId, DateTime timestamp, double longitude, double latitude, int velocity, int temperture, int batteryLevel)
	{
		this.Id = id;
		this.SatelliteId = satelliteId;
		this.Timestamp = timestamp;
		this.Longitude = longitude;
		this.Latitude = latitude;
		this.Velocity = velocity;
		this.Temperature = temperture;
		this.BatteryLevel = batteryLevel;
	}
}