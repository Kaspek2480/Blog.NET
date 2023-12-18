using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Blog.NET.Areas.Identity.Data;

public class ConfirmRemoveUserModel : PageModel
{
    private readonly ILogger<ConfirmRemoveUserModel> _logger;

    public ConfirmRemoveUserModel(ILogger<ConfirmRemoveUserModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
