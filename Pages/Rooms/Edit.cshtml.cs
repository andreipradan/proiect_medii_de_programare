using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Hotel.Pages.Rooms
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; } = default!;
        [BindProperty]
        public List<Facility> Facilities { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room =  await _context.Room.Include(r => r.Facilities).FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            Room = room;
            Facilities = await _context.Facility.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAssignFacilitiesAsync(int roomId, List<int> facilityIds)
        {
            var room = await _context.Room.Include(r => r.Facilities).FirstOrDefaultAsync(m => m.Id == roomId);
            var facilities = _context.Facility.Where(f => facilityIds.Contains(f.Id));
            if (room != null && facilities != null)
            {
                room.Facilities?.AddRange(facilities);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Edit",new { id = roomId });
        }

        public async Task<IActionResult> OnPostRemoveFacilityAsync(int roomId, int facilityId)
        {
            var room =  await _context.Room.Include(r=>r.Facilities).FirstOrDefaultAsync(m => m.Id == roomId);
            var facility =  await _context.Facility.FirstOrDefaultAsync(m => m.Id == facilityId);
            if (room != null && facility != null)
            {
                room.Facilities?.Remove(facility);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Edit",new { id = roomId });
        }

        public async Task<IActionResult> OnPostUpdateRoomAsync()
        {
            if (!ModelState.IsValid)
            {return Page();}
            _context.Attach(Room).State = EntityState.Modified;
            try
            {await _context.SaveChangesAsync();}
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(Room.Id))
                {return NotFound();}
                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool RoomExists(int id)
        {
          return (_context.Room?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
