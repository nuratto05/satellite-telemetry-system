using System;

namespace sat_sim.Models;
public class Telemetry
{
	public int SatelliteId {  get; private set; }
    public DateTime Timestamp { get; private set; }
    public double Longitude { get; private set; }
    public double Latitude { get; private set; }
    public int Altitude { get; private set; }
    public int Velocity { get; private set; }
    public int Temperature { get; private set; }
    public int BatteryLevel { get; private set; }

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