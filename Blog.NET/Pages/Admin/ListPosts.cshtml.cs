using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages.Admin;

public class ListPostsModel : PageModel
{
    private readonly AppDbContext _context;
    public readonly UserManager<BlogNETUser> _userManager;

    public ListPostsModel(AppDbContext context, UserManager<BlogNETUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public List<Models.BlogPost>? Posts { get; set; }
    
    public void OnGet()
    {
        Posts = _context.Blogs.OrderByDescending(p => p.CreatedAt).ToList();
    }
}