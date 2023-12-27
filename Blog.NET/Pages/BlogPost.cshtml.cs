using System.Security.Claims;
using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
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

    public BlogPost? BlogPost { get; set; }
    public int TotalLikes { get; set; }
    [BindProperty] public NewComment? NewComment { get; set; }
    [BindProperty] public DeleteComment? DeleteComment { get; set; }

    public List<Comment>? Comments { get; set; }


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

    public async Task<IActionResult> OnPostCommentAdd()
    {
        var user = await _userRepository.GetCurrentUser();
        if (user == null) return NotFound();

        var post = await _context.Blogs.FirstOrDefaultAsync(p => Equals(p.Id, NewComment!.BlogPostId));
        if (post == null) return NotFound();

        var comment = new Comment()
        {
            Content = NewComment!.RawContent!,
            CreatedAt = DateTime.Now,
            IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            User = user,
            BlogPost = post
        };

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return RedirectToPage("/BlogPost", new { id = NewComment.BlogPostId });
    }

    public async Task<IActionResult> OnPostCommentDelete()
    {
        var comment = await _context.Comments.Include(comment => comment.User)
            .FirstOrDefaultAsync(c => c.Id == DeleteComment!.Id);
        if (comment == null) return NotFound();

        var currentUser = await _userRepository.GetCurrentUser();

        if (!Equals(currentUser, comment.User) || !User.IsInRole("Admin"))
        {
            return RedirectToPage("/Error", new { message = "You are not allowed to delete this comment" });
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return RedirectToPage("/BlogPost", new { id = comment.BlogPostId });
    }
}