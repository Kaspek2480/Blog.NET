using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Pages.Admin;

public class EditPostModel : PageModel
{
    private readonly AppDbContext _context;

    public List<Tag>? AvailableTags { get; set; }
    
    public EditPostModel(AppDbContext context)
    {
        _context = context;
        
        AvailableTags = _context.Tags.ToList();
    }
    
    [BindProperty] public EditPost? EditPost { get; set; }
    
    public async Task<IActionResult> OnGet(int id)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();

        EditPost = new EditPost
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            RawContent = post.Content,
            FeaturedImage = post.FeaturedImage,
            Visible = post.Visible,
            Tags = post.Tags
        };

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {

        if (ModelState.IsValid)
        {

            var postToUpdate = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => p.Id == EditPost.Id);

            if (postToUpdate != null)
            {
                postToUpdate.Title = EditPost.Title;
                postToUpdate.Description = EditPost.Description;
                postToUpdate.Content = EditPost.RawContent;
                postToUpdate.FeaturedImage = EditPost.FeaturedImage;
                postToUpdate.Visible = EditPost.Visible;
                postToUpdate.Tags = EditPost.Tags;


                await _context.SaveChangesAsync();

                return RedirectToPage("/Admin/ListPost"); 
            }
        }
        
        return Page();
    }
    
}