using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;

namespace Hotel.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Hotel.Models.Room> Room { get; set; } = default!;
    public DbSet<Hotel.Models.Booking> Booking { get; set; } = default!;
    public DbSet<Hotel.Models.Facility> Facility { get; set; } = default!;
}