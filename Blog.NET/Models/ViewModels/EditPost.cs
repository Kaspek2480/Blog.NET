using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models.ViewModels;

public class EditPost
{
    public Guid Id { get; set; }
    
    [Required] public string? Title { get; set; }

    [Required, StringLength(256)] public string? Description { get; set; }

    [Required, StringLength(5000)] public string? RawContent { get; set; }

    public List<string>? Images { get; set; } //TODO: add image upload

    public string? FeaturedImage { get; set; }

    public bool Visible { get; set; } = true;
    public List<Tag>? Tags { get; set; }
}