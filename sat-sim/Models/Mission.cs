using sat_sim.Models.Enums;
using sat_sim.Models;
using System;

namespace sat_sim.Models;
public class Mission
{
    public int Id {  get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Status? Status { get; set; } = Enums.Status.Active;

    public ICollection<Satellite>? Satellites { get; set; }

}
