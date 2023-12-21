using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages;

public class BlogPostModel : PageModel
{
    private readonly AppDbContext _context;
    
    [BindProperty] public BlogPost? BlogPost { get; set; }
    

    public BlogPostModel(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnGet(Guid id)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();
        
        BlogPost = new BlogPost
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            FeaturedImage = post.FeaturedImage,
            Visible = post.Visible,
            Tags = post.Tags
        };

        return Page();
    }
}