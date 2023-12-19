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
    public EditTagModel (AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public EditTagRequest EditTagRequest { get; set; }
    
    public async Task<IActionResult> OnGet(int id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => Equals(t.Id, id));

        if (tag != null)
        {
            EditTagRequest = new EditTagRequest
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            return Page();
        }

        return NotFound();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var tag = new Tag
            {
                Id = EditTagRequest.Id,
                Name = EditTagRequest.Name,
                DisplayName = EditTagRequest.DisplayName
            };
            
            var existingTag = await _context.Tags.FirstOrDefaultAsync(t => Equals(t.Id, tag.Id));
            
            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                
                await _context.SaveChangesAsync();
                
                return RedirectToPage("/Admin/ListTag");
            }
        }
        
        return RedirectToAction("OnGet", new {id = EditTagRequest.Id});
        
    }
    
    public async Task<IActionResult> OnPostDelete(int id)
    {
        var tag = _context.Tags.FirstOrDefault(t => Equals(t.Id, id));
        
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("/Admin/ListTag");
        }

        return RedirectToAction("OnGet", new {id = EditTagRequest.Id});
    }
}