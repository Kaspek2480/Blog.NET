using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> OnPost()
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

            await _context.Tags.AddAsync(newTag);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/ListTag");
        }
        
    }
}
