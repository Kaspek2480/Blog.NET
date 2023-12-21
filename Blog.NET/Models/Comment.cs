using Blog.NET.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
// ReSharper disable All

namespace Blog.NET.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? IPAddress { get; set; }


        public string UserId { get; set; }
        public BlogNETUser User {get;  set;}

        public Guid BlogPostId { get; set; }
        public BlogPost BlogPost {  get; set; }
    }
}
