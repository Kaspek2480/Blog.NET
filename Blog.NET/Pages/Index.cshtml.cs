using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public List<Models.BlogPost>? Posts { get; set; }

    public void OnGet()
    {
        Posts = _context.Blogs.Include(b => b.Tags).ToList();
    }
}