using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Hotel.Models.Room> Room { get; set; } = default!;
    public DbSet<Hotel.Models.Booking> Booking { get; set; } = default!;
    public DbSet<Hotel.Models.Facility> Facility { get; set; } = default!;
}