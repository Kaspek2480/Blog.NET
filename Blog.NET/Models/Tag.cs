using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}
