using Blog.NET.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }


        // Wiele do jednego
        public string UserId { get; set; }
        public BlogNETUser User { get; set; }
    }
}
