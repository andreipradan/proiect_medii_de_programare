using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Pages.Bookings
{
    [Authorize(Roles = "Employee,Guest")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [BindProperty(SupportsGet = true)]
        public int RoomIdFromQuery { get; set; }
        public string GuestIdFromSession { get; set; }
        public List<Booking> Bookings { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(Request.Query["id"]) && int.TryParse(Request.Query["id"], out int roomIdInt))
            {
                RoomIdFromQuery = roomIdInt;
                Bookings = await _context.Booking.Where(b => b.RoomId == RoomIdFromQuery).ToListAsync();
            }
            else
            {
                ViewData["RoomId"] = new SelectList(_context.Room.ToList(), "Id", "DisplayName");
            }

            if (User.IsInRole("Employee"))
            {
                ViewData["GuestId"] = new SelectList(_context.Users, "Id", "Email");
            } else if (User.IsInRole("Guest"))
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                GuestIdFromSession = userId;
            }

            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (Booking.CheckInDate >= Booking.CheckOutDate)
            {
                ModelState.AddModelError("Booking.CheckInDate", "Check-in date must be before check-out date.");
                ModelState.AddModelError("Booking.CheckOutDate", "Check-out date must be after check-in date.");
            }

            var conflicting = _context.Booking.FirstOrDefault(b =>
                b.RoomId == Booking.RoomId && b.CheckInDate < Booking.CheckOutDate &&
                 b.CheckOutDate > Booking.CheckInDate);
            if (conflicting != null)
            {
                ModelState.AddModelError("Booking.CheckInDate", "There is a conflicting booking: " + conflicting.CheckInDate.ToShortDateString() + "-" + conflicting.CheckOutDate.ToShortDateString());
            }
            if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.Write(error.ErrorMessage);
                        
                    }
                    
                }
                ViewData["GuestId"] = new SelectList(_context.Users, "Id", "Email");
                if (!string.IsNullOrEmpty(Request.Query["id"]) && int.TryParse(Request.Query["id"], out int roomIdInt))
                {
                    RoomIdFromQuery = roomIdInt;
                    Bookings = await _context.Booking.Where(b => b.RoomId == RoomIdFromQuery).ToListAsync();
                }
                else
                {
                  ViewData["RoomId"] = new SelectList(_context.Room.ToList(), "Id", "DisplayName");
                }
                return Page();
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
