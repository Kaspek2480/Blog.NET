using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.NET.Pages.Admin;

[Authorize(Roles = "Admin")]
public class ListTagModel : PageModel
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