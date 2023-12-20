using System.Diagnostics;
using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin;

[Authorize(Roles = "Admin, User")]
public class NewPostModel : PageModel
{
    private readonly AppDbContext _context;

    public NewPostModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    [BindProperty] public NewPost? NewPost { get; set; }

    public List<Tag>? AvailableTags { get; set; }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public void OnGet()
    {
        AvailableTags = _context.Tags.ToList();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Debug.WriteLine("Model is not valid");
            return Page();
        }

        var blogNetUser = GetCurrentUser();
        if (blogNetUser == null)
        {
            //should never happen
            throw new Exception("User not found");
        }

        var post = new BlogPost()
        {
            Title = NewPost!.Title!,
            Content = NewPost.RawContent!,
            Description = NewPost.Description!,
            CreatedAt = DateTime.Now,
            Visible = NewPost.Visible,
            UserId = blogNetUser.Id,
            User = blogNetUser,
            FeaturedImage = NewPost.FeaturedImage
        };

        //handle tags from form
        foreach (var tag in Request.Form["NewPost.Tags"].ToList())
        {
            //get tag from db by id
            var tagToAdd = _context.Tags.FirstOrDefault(t => t.Id.ToString() == tag);
            if (tagToAdd == null)
            {
                throw new Exception("Tag provided in form not found in database " + tag);
            }

            post.Tags.Add(tagToAdd);
        }

        Debug.WriteLine("Post: " + post);
        _context.Blogs.Add(post);
        _context.SaveChanges();
        return RedirectToPage("/Index");
    }

    //TODO: move to service / utility class
    public BlogNETUser GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext == null || _context.Users == null)
        {
            throw new Exception("HttpContext is null");
        }

        var userId = _httpContextAccessor.HttpContext.User.Claims.First().Value;
        return _context.Users.FirstOrDefault(u => u.Id == userId) ??
               throw new InvalidOperationException("User not found");
    }
}