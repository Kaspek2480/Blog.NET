using System.ComponentModel.DataAnnotations;


public class DeleteComment
{
    [Required] public int Id { get; set; }
    [Required] public Guid? BlogPostId { get; set; }
}