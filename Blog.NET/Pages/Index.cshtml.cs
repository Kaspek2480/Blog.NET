using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    public readonly UserManager<BlogNETUser> _userManager;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context, IBlogPostLikeRepository blogPostLikeRepository, UserManager<BlogNETUser> userManager)
    {
        _logger = logger;
        _context = context;
        _blogPostLikeRepository = blogPostLikeRepository;
        _userManager = userManager;
    }
    
    public List<Models.BlogPost>? Posts { get; set; }
    
    public List<Tag>? Tags { get; set; }

    public UserManager<BlogNETUser> UserManager { get; }


    public void OnGet()
    {
        Posts = _context.Blogs
            .Include(b => b.Tags)
            .Where(p => p.Visible)
            .OrderByDescending(p => p.CreatedAt)
            .ToList();
        Tags = _context.Tags.ToList();
    }

    public BlogNETUser GetUserById(string userId)
    {
        return _userManager.FindByIdAsync(userId).Result;
    }
}