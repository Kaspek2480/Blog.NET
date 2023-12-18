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
            // Usu� u�ytkownika
            var result = _userManager.DeleteAsync(user).Result;

            if (result.Succeeded)
            {
                // Obs�u� sukces
                return RedirectToPage("/ConfirmRemoveUser");
            }
            else
            {
                // Obs�u� b��dy usuwania u�ytkownika
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        // Obs�u� sytuacj�, gdy u�ytkownik nie zosta� znaleziony
        return NotFound();
    }
}

