using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hotel.Models;

public enum BookingState
{
    Pending,
    Confirmed,
    Declined,
    Canceled
}

public class Booking
{
    public int Id { get; set; }

    [ForeignKey("ApplicationUser")]
    public string GuestId { get; set; }

    public int RoomId { get; set; }


    [DataType(DataType.Date)]
    public DateTime CheckInDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime CheckOutDate { get; set; }
    public BookingState State { get; set; }
    public string? Reason { get; set; }

    public ApplicationUser? Guest { get; set; }
    public Room? Room { get; set; }
}