using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Repositories;

public class UserPostsRepository : IUserPostsRepository
{
    private readonly AppDbContext _context;

    public UserPostsRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<BlogPost>> GetUserPosts(BlogNETUser user)
    {
        var blogPosts = _context.Blogs.Where(b => b.UserId == user.Id);
        return await blogPosts.ToListAsync();
    }
}