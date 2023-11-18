using Blog.NET.Areas.Identity.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Data;

public class AppDbContext : IdentityDbContext<BlogNETUser>
{
    public DbSet<BlogNETUser> Users { get; set; }
    public DbSet<BlogPost> Blogs { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
        });

        modelBuilder.Entity<IdentityUserRole<string>>(b =>
        {
            b.HasKey(ur => new { ur.UserId, ur.RoleId });
        });

        modelBuilder.Entity<IdentityUserToken<string>>(b =>
        {
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        });



        modelBuilder.Entity<BlogPost>()
            .HasOne(b => b.User)
            .WithMany(u => u.BlogPosts)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<BlogPost>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.BlogPosts)
            .UsingEntity(j => j.ToTable("BlogPostTag"));

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.BlogPost)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.BlogPostId);
    }
}
