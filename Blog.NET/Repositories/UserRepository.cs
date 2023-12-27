using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.NET.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<BlogNETUser> _userManager;

    public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor,
        UserManager<BlogNETUser> userManager)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }


    public async Task<List<BlogPost>> GetUserPosts(BlogNETUser user)
    {
        var blogPosts = _context.Blogs.Where(b => b.UserId == user.Id).OrderByDescending(b => b.CreatedAt);
        
        return await blogPosts.ToListAsync();
    }

    public async Task<BlogNETUser?> GetUserByUsername(string? username)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<BlogNETUser?> GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext == null || _context.Users == null)
        {
            throw new Exception("HttpContext is null");
        }

        var userId = _httpContextAccessor.HttpContext.User.Claims.First().Value;
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) ??
               throw new InvalidOperationException("User not found");
    }
}