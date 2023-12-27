using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages.Admin;

public class ListPostsModel : PageModel
{
    private readonly AppDbContext _context;

    public ListPostsModel(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Models.BlogPost>? Posts { get; set; }
    
    public void OnGet()
    {
        Posts = _context.Blogs.Include(b => b.Tags).ToList();
    }
}