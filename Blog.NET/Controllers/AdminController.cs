using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Blog.NET.Areas.Identity.Data;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<BlogNETUser> _userManager;

    public AdminController(UserManager<BlogNETUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult RemoveUser(string userId)
    {
        // Pobierz użytkownika do usunięcia
        var user = _userManager.FindByIdAsync(userId).Result;

        if (user != null)
        {
            return View("RemoveUser");
        }

        // Obsłuż sytuację, gdy użytkownik nie został znaleziony
        return NotFound();
    }

    [HttpPost]
    public IActionResult ConfirmRemoveUser(string userEmail)
    {
        var user = _userManager.FindByEmailAsync(userEmail).Result;

        if (user != null)
        {
            // Usuń użytkownika
            var result = _userManager.DeleteAsync(user).Result;

            if (result.Succeeded)
            {
                // Obsłuż sukces
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Obsłuż błędy usuwania użytkownika
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        // Obsłuż sytuację, gdy użytkownik nie został znaleziony
        return NotFound();
    }
}
