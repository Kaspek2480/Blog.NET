using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context, IBlogPostLikeRepository blogPostLikeRepository)
    {
        _logger = logger;
        _context = context;
        _blogPostLikeRepository = blogPostLikeRepository;
    }
    
    public List<Models.BlogPost>? Posts { get; set; }
    
    public List<Tag>? Tags { get; set; }
    

    public void OnGet()
    {
        Posts = _context.Blogs.Include(b => b.Tags).OrderByDescending(p => p.CreatedAt).ToList();
        Tags = _context.Tags.ToList();
    }
}