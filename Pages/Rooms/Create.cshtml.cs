using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel.Pages.Rooms
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Room == null || Room == null)
          {
              foreach (var modelState in ModelState.Values)
              {
                  foreach (var error in modelState.Errors)
                  {
                      Console.Write(error.ErrorMessage);
                  }
              }
              return Page();
          }

          _context.Room.Add(Room);
          await _context.SaveChangesAsync();
          
          return RedirectToPage("./Index");
        }
    }
}
