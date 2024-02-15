using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models;

public class Facility
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    [InverseProperty("Facilities")]
    public ICollection<Room>? Rooms { get; set; }
}