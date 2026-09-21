using System;

public class Satellite
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
    public Mission Mission { get; set; }
    public string Country { get; set; }

    public Satellite() { }

    public Satellite(int id, string name, Mission mission, string country)
    {
        this.Id = id;
        this.Name = name;
        this.Status = Status.Active;
        this.Mission = mission;
        this.Country = country;
    } 
}
