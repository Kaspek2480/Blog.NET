using Blog.NET.Areas.Identity.Data;
using Blog.NET.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string FeaturedImage { get; set; }

        public string UrlHandle { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Visible { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }


        // Wiele do jednego
        public string UserId { get; set; }
        public BlogNETUser User { get; set; }
    }
}
