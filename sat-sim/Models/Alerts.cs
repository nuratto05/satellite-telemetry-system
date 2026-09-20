using System;

public class Alerts
{
	public int id {  get; set; }
	public string name { get; set; }
	public string type { get; set; }
	public string description { get; set; }
	public string timestamp { get; set; }
	public int status { get; set; }

	public Alerts(int id, int name, int type, int status)
	{
		this.id = id;
		this.name = name;
		this.type = type;
		this.status = status;
	}
} 
