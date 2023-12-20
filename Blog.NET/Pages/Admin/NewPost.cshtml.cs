using System.Diagnostics;
using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin;

public class NewPostController : Controller
{
    [Authorize(Roles = "Admin, User")]
    public class NewPostModel : PageModel
    {
        private readonly AppDbContext _context;

        public NewPostModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty] public required string Heading { get; set; }
        [BindProperty] public required string Title { get; set; }
        [BindProperty] public required string RawContent { get; set; }
        [BindProperty] public required string FeaturedImage { get; set; }
        [BindProperty] public required string UrlHandle { get; set; }
        [BindProperty] public required string Description { get; set; }
        [BindProperty] public DateTime CreatedAt { get; set; }
        [BindProperty] public bool Visible { get; set; }
        [BindProperty] public List<Tag>? Tags { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public void OnGet()
        {
            Tags = _context.Tags.ToList();
        }

        public IActionResult OnPost()
        {
            // Validate(this);
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
                Heading = Heading,
                Title = Title,
                Content = RawContent,
                FeaturedImage = FeaturedImage,
                UrlHandle = UrlHandle,
                Description = Description,
                //CreatedAt = DateTime.Now,
                CreatedAt = CreatedAt,
                Visible = Visible, 
                UserId = blogNetUser.Id,
                User = blogNetUser
            };

            //handle tags from form
            foreach (var tag in Request.Form["Tags"].ToList())
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

        public bool Validate(NewPostModel model)
        {
            var valid = true;
            if (model.Heading.Length < 5)
            {
                ModelState.AddModelError("Heading", "Heading must be at least 5 characters long");
                valid = false;
            }

            if (model.Title.Length < 5)
            {
                ModelState.AddModelError("Title", "Title must be at least 5 characters long");
                valid = false;
            }

            if (model.RawContent.Length < 5)
            {
                ModelState.AddModelError("Content", "Content must be at least 5 characters long");
                valid = false;
            }

            return valid;
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
}