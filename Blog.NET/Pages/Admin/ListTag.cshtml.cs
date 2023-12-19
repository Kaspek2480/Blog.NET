using Blog.NET.Data;
using Microsoft.AspNetCore.Mvc;
using Blog.NET.Models;

namespace Blog.NET.Pages.Admin;

public class ListCshtml : Controller
{
    public class ListTagModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly AppDbContext _context;
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