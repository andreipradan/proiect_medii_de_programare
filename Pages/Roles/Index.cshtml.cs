using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<IdentityRole> Roles { get; set; }
        public List<UserRoleViewModel> Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            Users = await GetUserRolesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateRoleAsync(string roleName)
        {
            Console.WriteLine($"ModelState Error: {ModelState} ");
        
            if (!ModelState.IsValid)
            {
                return Page();
            }
        
            var newRole = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(newRole);
        
            if (result.Succeeded)
            {
                return RedirectToPage("./Index");
            }
        
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAssignRoleAsync(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null && user != null && role.Name != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(roleName);
            if (user != null && role != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToPage("./Index");
        }
        private async Task<List<UserRoleViewModel>> GetUserRolesAsync()
        {
            var usersWithRoles = new List<UserRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = userRoles.ToList()
                });
            }
            return usersWithRoles;
        }
        
    }
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
