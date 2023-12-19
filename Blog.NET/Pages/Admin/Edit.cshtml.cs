using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;
    public EditModel (AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public EditTagRequest EditTagRequest { get; set; }
    
    public IActionResult OnGet(int id)
    {
        var tag = _context.Tags.FirstOrDefault(t => Equals(t.Id, id));

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

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            var tag = new Tag
            {
                Id = EditTagRequest.Id,
                Name = EditTagRequest.Name,
                DisplayName = EditTagRequest.DisplayName
            };
            
            var existingTag = _context.Tags.FirstOrDefault(t => Equals(t.Id, tag.Id));
            
            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                
                _context.SaveChanges();
                
                return RedirectToPage("/Admin/ListTag");
            }
        }
        
        return RedirectToAction("OnGet", new {id = EditTagRequest.Id});
        
    }
    
    public IActionResult OnPostDelete(int id)
    {
        var tag = _context.Tags.FirstOrDefault(t => Equals(t.Id, id));
        
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            
            return RedirectToPage("/Admin/ListTag");
        }

        return RedirectToAction("OnGet", new {id = EditTagRequest.Id});
    }
}