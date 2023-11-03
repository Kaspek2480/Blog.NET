using Blog.NET.Models;
using Microsoft.EntityFrameworkCore;
namespace Blog.NET.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Tag> Tags {  get; set; }
        public DbSet<Comment> Comments { get; set; }
    }


}
