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
    public class IndexModel : PageModel
    {
        private readonly Hotel.Data.ApplicationDbContext _context;

        public IndexModel(Hotel.Data.ApplicationDbContext context)
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
