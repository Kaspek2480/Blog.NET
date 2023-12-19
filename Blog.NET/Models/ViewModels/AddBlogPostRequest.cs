using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.NET.Models.ViewModels;

public class AddBlogPostRequest
{
    public string Heading { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } 
    public string FeaturedImage { get; set; }
    public string UrlHandle { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Visible { get; set; }
    public string UserId { get; set; }
    
    public IEnumerable<SelectListItem> Tags { get; set; }
    public string SelectedTag { get; set; }
}