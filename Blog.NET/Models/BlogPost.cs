using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }


        // Wiele do jednego
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
