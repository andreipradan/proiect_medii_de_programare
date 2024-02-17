using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty(SupportsGet = true)]
        public int RoomIdFromQuery { get; set; }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(Request.Query["id"]) && int.TryParse(Request.Query["id"], out int roomIdInt))
            {
                RoomIdFromQuery = roomIdInt;
            }
            else
            {
                ViewData["RoomId"] = new SelectList(_context.Room.ToList(), "Id", "DisplayName");
            }
            ViewData["GuestId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
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
