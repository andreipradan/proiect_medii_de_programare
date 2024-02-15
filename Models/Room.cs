using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models;

public enum RoomType
{
    Single,
    Double,
    Triple,
}
public class Room
{
    public int Id { get; set; }
    public RoomType Type { get; set; }
    public float Price { get; set; }
    
    public ICollection<Booking>? Bookings { get; set; }

    // Navigation property for rooms
    [InverseProperty("Rooms")]
    public ICollection<Facility>? Facilities { get; set; }    
    
    [Display(Name = "Display Name")]
    public string DisplayName => "#" + Id + " - " + Type + " @ $" + Price;
}
