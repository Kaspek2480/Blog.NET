using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages;

public class UserPosts : PageModel
{
    public string? Username { get; set; }
    public List<BlogPost> Posts { get; set; }
    
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    private readonly IUserRepository _userRepository;
    private readonly AppDbContext _context;

    public UserPosts(IUserRepository userRepository, AppDbContext context)
    {
        _userRepository = userRepository;
        _context = context;
        Posts = new List<BlogPost>();
    }

    public async Task<IActionResult> OnGet(string? username, [FromQuery] int page)
    {
        page = page < 1 ? 1 : page;
        
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
        CurrentPage = page;
        
        
        const int PageSize = 10;
        var skip = (page - 1) * PageSize;
        
        Posts = _context.Blogs
            .Include(b => b.Tags)
            .Where(p => p.UserId == user.Id)
            .OrderByDescending(p => p.CreatedAt)
            .Skip(skip)
            .Take(PageSize)
            .ToList();
        
        TotalPages = (int)Math.Ceiling(_context.Blogs.Count(p => p.Visible && p.UserId == user.Id) / (double)PageSize);


        return Page();
    }
}