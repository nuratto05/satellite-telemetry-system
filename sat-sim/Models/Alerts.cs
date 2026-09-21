using System;

public class Alerts
{
	public int id {  get; set; }
	public string name { get; set; }
	public string type { get; set; }
	public string description { get; set; }
	public DateTime timestamp { get; set; }
	public string status { get; set; }

	public Alerts(int id, string name, string type, string description, DateTime timestamp, string status)
	{
		this.id = id;
		this.name = name;
		this.type = type;
		this.description = description;
		this.timestamp = timestamp;
		this.status = status;
	}
} 
