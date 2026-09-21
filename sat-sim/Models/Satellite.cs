using System;

public class Satellite
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Health { get; set; }
    public string OrbitType { get; set; }
    public Mission Mission { get; set; }
    public string Country { get; set; }

    public Satellite(int id, string name, string health, string orbitType, Mission mission, string country)
    {
        this.Id = id;
        this.Name = name;
        this.Status = Status.Active;
        this.Health = health;
        this.OrbitType = orbitType;
        this.Mission = mission;
        this.Country = country;
    } 
}
