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

    private readonly IUserRepository _userRepository;

    public UserPosts(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        Posts = new List<BlogPost>();
    }

    public async Task<IActionResult> OnGet(string? username)
    {
        if (username == null)
        {
            return NotFound();
        }

        var user = _userRepository.GetUserByUsername(username).Result;
        if (user == null)
        {
            return NotFound();
        }

        Username = username;
        Posts = await _userRepository.GetUserPosts(user);

        return Page();
    }
}