using Blog.NET.Areas.Identity.Data;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages;

public class UserPosts : PageModel
{
    public string? Username { get; set; }
    public List<UserPost>? Posts { get; set; }
    
    private readonly UserManager<BlogNETUser> _userManager;

    public UserPosts(UserManager<BlogNETUser> userManager)
    {
        _userManager = userManager;
    }

    public void OnGet(string? username)
    {
        Username = username;
        // Tutaj umieść kod do pobierania postów użytkownika na podstawie nazwy
        // i przypisz je do właściwości modelu, aby można było je wyświetlić w widoku
    }
    
    public BlogNETUser GetUserByUsername(string username)
    {
        var user = _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
        return user;
    }
}