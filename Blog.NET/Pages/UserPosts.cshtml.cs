using Blog.NET.Areas.Identity.Data;
using Blog.NET.Models;
using Blog.NET.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages;

public class UserPosts : PageModel
{
    public string? Username { get; set; }
    public List<BlogPost> Posts { get; set; }

    private readonly IUserPostsRepository _userPostsRepository;
    private readonly UserManager<BlogNETUser> _userManager;

    public UserPosts(IUserPostsRepository userPostsRepository, UserManager<BlogNETUser> userManager)
    {
        _userPostsRepository = userPostsRepository;
        _userManager = userManager;
        Posts = new List<BlogPost>();
    }

    public IActionResult OnGet(string? username)
    {
        if (username == null)
        {
            return NotFound();
        }

        var user = GetUserByUsername(username);
        if (user == null)
        {
            return NotFound();
        }

        Username = username;
        Posts = _userPostsRepository.GetUserPosts(user).Result;

        return Page();
    }

    public BlogNETUser? GetUserByUsername(string? username)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
        return user;
    }
}