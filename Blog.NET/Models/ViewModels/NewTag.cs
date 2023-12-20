using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models.ViewModels;

public class NewTag
{
    [Required, StringLength(256)] public string? Name { get; set; }
    [Required, StringLength(256)] public string? DisplayName { get; set; }
}