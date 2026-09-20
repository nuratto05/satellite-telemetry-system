using System;

public class Satellite
{
    public int id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string health { get; set; }
    public string orbitType { get; set; }
    public Mission mission { get; set; }
    public string country { get; set; }

    public Satellite(int id, string name, string status)
    {
        this.id = id;
        this.name = name;
        this.status = status;
    } 
}
