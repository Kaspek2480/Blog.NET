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
    [BindProperty] public Guid _id { get; set; }
    
    public async Task<IActionResult> OnGet(Guid id)
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, id));
        if (post == null) return NotFound();

        _id = post.Id;
        
        //Console.WriteLine(_id);

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
    
    public async Task<IActionResult> OnPostUpdate()
    {

        if (!ModelState.IsValid)
            return Page();
        
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, _id));
        
        if (post == null)
            throw new Exception("Tried to edit non-existing post (id: " + EditPost?.Id + ")"); 
        
        post.Title = EditPost!.Title!;
        post.Description = EditPost!.Description!;
        post.Content = EditPost!.RawContent!;
        post.FeaturedImage = EditPost!.FeaturedImage;
        post.Visible = EditPost.Visible;
        
        post.Tags?.Clear();

        if (EditPost.Tags != null)
            foreach (var tag in EditPost.Tags)
            {
                var tagInDb = await _context.Tags.FindAsync(tag.Id);
                if (tagInDb != null)
                {
                    post.Tags?.Add(tagInDb);
                }
            }

        _context.Blogs.Update(post);
        await _context.SaveChangesAsync();
        
        return RedirectToPage("/Admin/ListPosts");
        
    }

    public async Task<IActionResult> OnPostDelete()
    {
        var post = await _context.Blogs.Include(blogPost => blogPost.Tags).FirstOrDefaultAsync(p => Equals(p.Id, _id));
        
        if (post == null)
            throw new Exception("Tried to delete non-existing post (id: " + EditPost?.Id + ")");
        
        _context.Blogs.Remove(post);
        await _context.SaveChangesAsync();
        
        return RedirectToPage("/Admin/ListPosts");
    }
    
}