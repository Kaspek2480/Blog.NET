using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.NET.Pages.Admin;
[Authorize(Roles = "Admin")]

public class AddPostModel : PageModel
{
    private readonly AppDbContext _context;
    
    public AddPostModel(AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public AddBlogPostRequest AddBlogPostRequest { get; set; }
    public List<Tag> Tags { get; set; }
    
    public void OnGet()
    {
        Tags = _context.Tags.ToList();

        AddBlogPostRequest = new AddBlogPostRequest
        {
            Tags = Tags.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            })
        };
    }
    

}