using System.ComponentModel.DataAnnotations;

namespace Hotel.Models;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public ICollection<Booking> Bookings { get; set; }

    [Display(Name = "Full Name")]
    public string? FullName => FirstName + " " + LastName;
}
