using Blog.NET.Data;
using Microsoft.AspNetCore.Mvc;
using Blog.NET.Models;

namespace Blog.NET.Pages.Admin;

public class ListCshtml : Controller
{
    public class ListTagModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string DisplayName { get; set; }

        public ListTagModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Tag>? Tags { get; set; }

        public void OnGet()
        {
            Tags = _context.Tags.ToList();  
        }
    }
}