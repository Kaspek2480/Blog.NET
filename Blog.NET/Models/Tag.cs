using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}
