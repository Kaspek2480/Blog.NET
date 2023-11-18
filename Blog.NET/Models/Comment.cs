using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IPAddress { get; set; }


        //Relacja wiele do jednego
        public int BlogPostId { get; set; }
        public BlogPost BlogPost {  get; set; }
    }
}
