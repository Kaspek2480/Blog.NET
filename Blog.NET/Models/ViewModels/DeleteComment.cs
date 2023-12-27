using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models.ViewModels;

public class DeleteComment
{
    [Required] public int Id { get; set; }
    [Required] public Guid? BlogPostId { get; set; }
}