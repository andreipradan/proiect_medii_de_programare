using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Booking> Booking { get;set; } = default!;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public async Task OnGetAsync()
        {
            if (_context.Booking != null)
            {
                if (User.IsInRole("Guest"))
                {
                    var userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    Booking = await _context.Booking.Where(b=>b.GuestId == userId)
                    .Include(b => b.Guest)
                    .Include(b => b.Room).ToListAsync();                    
                }
                else
                {
                    Booking = await _context.Booking
                    .Include(b => b.Guest)
                    .Include(b => b.Room).ToListAsync();
                }
            }
        }
        
        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var booking= await _context.Booking.FindAsync(id);
            if (booking == null) return RedirectToPage("./Index");
            booking.State = BookingState.Confirmed;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        
        public async Task<IActionResult> OnPostDeclineAsync(int id)
        {
            var booking= await _context.Booking.FindAsync(id);
            if (booking == null) return RedirectToPage("./Index");
            booking.State = BookingState.Declined;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
