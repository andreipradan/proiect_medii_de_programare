using System.ComponentModel.DataAnnotations;

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
    public int Number { get; set; }
    public RoomType Type { get; set; }
    public float Price { get; set; }
    public bool IsAvailable { get; set; }
    
    public ICollection<Booking>? Bookings { get; set; }
    public ICollection<RoomFacility>? Facilities { get; set; }
    
    
    [Display(Name = "Display Name")]
    public string? DisplayName => "#" + Number + " - " + Type + " @ $" + Price;
}
