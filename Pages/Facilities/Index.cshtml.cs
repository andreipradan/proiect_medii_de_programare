using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            Facility = await _context.Facility.ToListAsync();
        }
        
        public async Task<IActionResult> OnPostAsync(string name, string description)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.Facility.AddAsync(new Facility{Description = description, Name = name});
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
