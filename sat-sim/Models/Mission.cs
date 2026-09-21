using System;

public class Mission
{
	public int id {  get; set; }
	public string name { get; set; }
	public string description { get; set; }
	public Status status { get; set; }

	public Mission() { }

	public Mission(int id, string name, string description)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.status = Status.Active;
	}
}
