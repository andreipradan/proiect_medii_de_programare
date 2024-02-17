using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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

    [ForeignKey("IdentityUser")]
    public string GuestId { get; set; }

    [ForeignKey("Room")]
    public int RoomId { get; set; }


    [DataType(DataType.Date)]
    public DateTime CheckInDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime CheckOutDate { get; set; }

    public BookingState State { get; set; } = BookingState.Pending;
    public string? Reason { get; set; }

    public IdentityUser? Guest { get; set; }
    public Room? Room { get; set; }
}