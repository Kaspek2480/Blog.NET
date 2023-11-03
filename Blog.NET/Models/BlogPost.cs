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
    }
}
