using Blog.NET.Areas.Identity.Data;

namespace Blog.NET.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        public string Heading { get; set; } = "";

        public string Title { get; set; }

        public string Content { get; set; }

        public string? FeaturedImage { get; set; } = "";

        public string UrlHandle { get; set; } = "";

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Visible { get; set; }
        public List<Tag>? Tags { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public string UserId { get; set; }
        public BlogNETUser User { get; set; }

        public override string ToString()
        {
            return
                $"BlogPost(Id={Id}, Heading={Heading}, Title={Title}, Content={Content}, FeaturedImage={FeaturedImage}, UrlHandle={UrlHandle}, Description={Description}, CreatedAt={CreatedAt}, Visible={Visible}, Tags={Tags}, Comments={Comments}, UserId={UserId}, User={User})";
        }
    }
}