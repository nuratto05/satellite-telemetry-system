using System;

public class Alert
{
	public int Id {  get; set; }
	public string SatelliteId { get; set; }
    public DateTime timestamp { get; set; }

	public SeverityStatus severity { get; set; }

	public SeverityStatus VelocityLevel { get; set; }
	public SeverityStatus TemperatureLevel { get; set; }
	public SeverityStatus BatteryLevel { get; set; }

	public Alert() { }

	public Alert(int id, string satelliteName, DateTime timestamp, SeverityStatus severity, SeverityStatus velocityLevel, SeverityStatus tempLevel, SeverityStatus batteryLevel)
	{
		this.Id = id;
		this.SatelliteId = satelliteName;
		this.timestamp = timestamp;
		this.severity = severity;
		this.VelocityLevel = velocityLevel;
		this.TemperatureLevel = tempLevel;
		this.BatteryLevel = batteryLevel
	}
} 
