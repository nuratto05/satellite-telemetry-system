using System;
using sat_sim.Models.Enums;

namespace sat_sim.Models;
public class Satellite
{
    public int Id { get; private set; }
    public Status Status { get; private set; }

    private Satellite() { }

    public Satellite(int id, Status status)
    {
        this.Id = id;
        Status = status;
    }

    public void Activate() { Status = Status.Active; }
    public void Deactivate() { Status = Status.Inactive; }
}
