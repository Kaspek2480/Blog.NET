using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages.Admin;

[Authorize(Roles = "Admin")]
public class EditTagModel : PageModel
{
    private readonly AppDbContext _context;

    public EditTagModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] public EditTag? EditTag { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => Equals(t.Id, id));
        if (tag == null) return NotFound();

        EditTag = new EditTag
        {
            Id = tag.Id,
            Name = tag.Name,
            DisplayName = tag.DisplayName
        };

        return Page();
    }

    public async Task<IActionResult> OnPostUpdate(int id)
    {
        //changed -> base only on current tag id, never create new tag
        //TODO: do global validation error page

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var tag = await _context.Tags.FirstOrDefaultAsync(t => Equals(t.Id, id));
        if (tag == null)
        {
            throw new Exception("Tried to edit non-existing tag (id: " + id + ")");
        }

        tag.Name = EditTag!.Name;
        tag.DisplayName = EditTag.DisplayName;

        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Admin/ListTag");
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var tag = _context.Tags.FirstOrDefault(t => Equals(t.Id, id));
        if (tag == null)
        {
            throw new Exception("Tried to delete non-existing tag (id: " + id + ")");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Admin/ListTag");
    }
}