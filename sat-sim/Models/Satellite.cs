using System;

namespace sat_sim.Models;
public class Satellite
{
    public int Id { get; set; }

    public Status Status { get; set; }

    public Satellite() { }

    public Satellite(int id, Status status)
    {
        this.Id = id;
        Status = status;
    }
}
