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
    private readonly IUserRepository _userRepository;
    
    [BindProperty] public BlogPost? BlogPost { get; set; }
    [BindProperty] public int TotalLikes { get; set; }
    [BindProperty] public Comment? Comment { get; set; }
    

    public BlogPostModel(AppDbContext context, IBlogPostLikeRepository blogPostLikeRepository, IUserRepository userRepository)
    {
        _context = context;
        _blogPostLikeRepository = blogPostLikeRepository;
        _userRepository = userRepository;
    }
    
    public async Task<IActionResult> OnGet(Guid id)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();

        BlogPost = post; //post user is null
        TotalLikes = await _blogPostLikeRepository.GetTotalLikes(id);
        
        //to be fixed

        return Page();
    }
    
    public async Task<IActionResult> OnPostCommentAsync(Guid id, string comment)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).Include(blogPost => blogPost.Comments).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();

        post.Comments.Add(new Comment
        {
            Content = comment,
            CreatedAt = DateTime.UtcNow,
            // UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
        });

        await _context.SaveChangesAsync();

        return RedirectToPage("/BlogPost", new {id});
    }
}