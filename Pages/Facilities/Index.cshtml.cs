using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Pages.Facilities
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Facility> Facility { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Facility != null)
            {
                Facility = await _context.Facility.ToListAsync();
            }
        }
    }
}
