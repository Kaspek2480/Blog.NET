using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Blog;

[Authorize(Roles = "User")]
public class NewCommentModel : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty] public NewComment? NewComment { get; set; }

    public NewCommentModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var comment = new Comment()
        {
            Content = NewComment!.RawContent!,
            CreatedAt = DateTime.Now,
            IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return RedirectToPage("/");
    }
}