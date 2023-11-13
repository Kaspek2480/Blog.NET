using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string IPAddress { get; set; }


        //Relacja wiele do jednego
        public int BlogPostId { get; set; }
        public BlogPost BlogPost {  get; set; }
    }
}
