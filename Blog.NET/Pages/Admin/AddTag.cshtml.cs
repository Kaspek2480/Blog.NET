using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin
{
    public class AdminTag : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string DisplayName { get; set; }

        public AdminTag(AppDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var newTag = new Tag
            {
                Name = Name,
                DisplayName = DisplayName
            };

            _context.Tags.Add(newTag);
            _context.SaveChanges();

            return RedirectToPage("/Admin/ListTag");
        }
        
    }
}
