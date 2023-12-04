using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.NET.Areas.Identity.Data;

public class AccountController : Controller
{
    private readonly UserManager<BlogNETUser> _userManager;

    public AccountController(UserManager<BlogNETUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(BlogNETUser model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        // Update user properties based on the model
        user.Email = model.Email;
        user.UserName = model.UserName;

        // Add additional properties as needed

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            // Optionally, you can redirect to a profile confirmation page
            return RedirectToAction("ProfileUpdated");
        }

        // If there are errors during the update, add them to the model state
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    public IActionResult ProfileUpdated()
    {
        return View();
    }
}
