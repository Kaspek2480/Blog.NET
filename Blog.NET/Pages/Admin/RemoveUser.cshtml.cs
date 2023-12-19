using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Blog.NET.Areas.Identity.Data;

namespace Blog.NET.Pages.Admin;
public class RemoveUserModel : PageModel
{
    private readonly UserManager<BlogNETUser> _userManager;
    private readonly ILogger<RemoveUserModel> _logger;

    public RemoveUserModel(UserManager<BlogNETUser> userManager, ILogger<RemoveUserModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public void OnGet()
    {
    }
    public IActionResult OnPost(string userEmail)
    {
        var user = _userManager.FindByEmailAsync(userEmail).Result;

        if (user != null)
        {
            // Usuñ u¿ytkownika
            var result = _userManager.DeleteAsync(user).Result;

            if (result.Succeeded)
            {
                // Obs³u¿ sukces
                return RedirectToPage("/ConfirmRemoveUser");
            }
            else
            {
                // Obs³u¿ b³êdy usuwania u¿ytkownika
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        // Obs³u¿ sytuacjê, gdy u¿ytkownik nie zosta³ znaleziony
        return NotFound();
    }
}

