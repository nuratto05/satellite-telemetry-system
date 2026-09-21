using System;

public class Alert
{
	public int Id {  get; set; }
	public int SatelliteId { get; set; }
    public DateTime timestamp { get; set; }
	public SeverityStatus Severity { get; set; } = SeverityStatus.Moderate;
    public SeverityStatus VelocityLevel { get; set; } = SeverityStatus.Moderate;
    public SeverityStatus TemperatureLevel { get; set; } = SeverityStatus.Moderate;
    public SeverityStatus BatteryLevel { get; set; } = SeverityStatus.Moderate;

    public Alert() { }

	public Alert(int id, int satelliteName, DateTime timestamp)
	{
		this.Id = id;
		this.SatelliteId = satelliteName;
		this.timestamp = timestamp;
	}
} 
