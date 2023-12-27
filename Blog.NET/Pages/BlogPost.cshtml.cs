using System.Security.Claims;
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

    [BindProperty] public List<Comment>? Comments { get; set; }


    public BlogPostModel(AppDbContext context, IBlogPostLikeRepository blogPostLikeRepository,
        IUserRepository userRepository)
    {
        _context = context;
        _blogPostLikeRepository = blogPostLikeRepository;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();

        Comments = await _context.Comments.Include(comment => comment.User).Where(comment => comment.BlogPostId == id)
            .OrderByDescending(comment => comment.CreatedAt).ToListAsync();
        
       
        BlogPost = post; //FIXME: User object in BlogPost is null
        TotalLikes = await _blogPostLikeRepository.GetTotalLikes(id);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        //if (!ModelState.IsValid) return Page();
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userRepository.GetCurrentUser();
        if (user == null) return NotFound();
        
        Comment.UserId = userId;
        //Comment.User = user;
        Comment.CreatedAt = DateTime.Now;
        Comment.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        Comment.BlogPostId = BlogPost.Id;
        await _context.Comments.AddAsync(Comment);
        await _context.SaveChangesAsync();
        
        return RedirectToPage("/BlogPost", new { id = Comment.BlogPostId });
    }
}