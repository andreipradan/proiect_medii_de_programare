using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hotel.Data;
using Hotel.Models;

namespace Hotel.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly Hotel.Data.ApplicationDbContext _context;

        public CreateModel(Hotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GuestId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["RoomId"] = new SelectList(_context.Room.Where(room => room.IsAvailable).ToList(), "Id", "DisplayName");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
                ViewData["GuestId"] = new SelectList(_context.Users, "Id", "FullName");
                ViewData["RoomId"] = new SelectList(_context.Room.Where(room => room.IsAvailable).ToList(), "Id", "DisplayName");
                return Page();
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
