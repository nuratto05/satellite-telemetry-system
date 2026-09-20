using System;

public class Telemetry
{
	public int id {  get; set; }
	public int satelliteId {  get; set; }
	public string timestamp { get; set; }
	public double longitude { get; set; }
	public double latitude { get; set; }
	public double altitude { get; set; }
	public double velocity { get; set; }
	public double temperture { get; set; }
	public double batteryLevel { get; set; }

	public Telemetry(int id, int satelliteId)
	{
		this.id = id;
		this.satelliteId = satelliteId;
	}
}
