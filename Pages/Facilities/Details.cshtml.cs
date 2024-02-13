using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.Pages.Facilities
{
    [Authorize(Roles = "Employee")]
    public class DetailsModel : PageModel
    {
        private readonly Hotel.Data.ApplicationDbContext _context;

        public DetailsModel(Hotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Facility Facility { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facility == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility.FirstOrDefaultAsync(m => m.Id == id);
            if (facility == null)
            {
                return NotFound();
            }
            else 
            {
                Facility = facility;
            }
            return Page();
        }
    }
}
