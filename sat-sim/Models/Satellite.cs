using System;

public class Satellite
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Satellite() { }

    public Satellite(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    } 
}
