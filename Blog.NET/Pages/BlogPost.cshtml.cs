using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages;

public class BlogPostModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    
    [BindProperty] public BlogPost? BlogPost { get; set; }
    [BindProperty] public int TotalLikes { get; set; }

    public BlogPostModel(AppDbContext context, IBlogPostLikeRepository blogPostLikeRepository)
    {
        _context = context;
        _blogPostLikeRepository = blogPostLikeRepository;
    }
    
    public async Task<IActionResult> OnGet(Guid id)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();

        BlogPost = post;
        TotalLikes = await _blogPostLikeRepository.GetTotalLikes(id);

        return Page();
    }
}