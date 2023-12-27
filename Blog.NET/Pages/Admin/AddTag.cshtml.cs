using Blog.NET.Data;
using Blog.NET.Models;
using Blog.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class NewTagModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty] public NewTag? NewTag { get; set; }

        public NewTagModel(AppDbContext context)
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
                Name = NewTag!.Name,
                DisplayName = NewTag.DisplayName
            };

            await _context.Tags.AddAsync(newTag);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/ListTag");
        }
    }
}