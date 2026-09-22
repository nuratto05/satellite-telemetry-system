using sat_ground_station.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sat_ground_station.Models;
[Table("Mission")]
public class Mission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id {  get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Required]
    [Column("description")]
    public string Description { get; set; }

    [Column("status")]
    public Status? Status { get; set; } = Models.Status.Active;

    public ICollection<Satellite>? Satellites { get; set; }

}
