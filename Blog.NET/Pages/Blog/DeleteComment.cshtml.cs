using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages.Blog;

[Authorize(Roles = "User")]
public class DeleteCommentModel : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty] public DeleteComment? DeleteComment { get; set; }

    public DeleteCommentModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet(int id)
    {
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var comment = await _context.Comments.FirstOrDefaultAsync(c => Equals(c.Id, id));
        if (comment == null)
        {
            throw new Exception("Tried to delete non-existing comment (id: " + id + ")");
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return RedirectToPage("/");
    }
}