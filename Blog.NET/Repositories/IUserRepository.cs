using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;

namespace Blog.NET.Repositories;

public interface IUserRepository {
    Task<List<BlogPost>> GetUserPosts(BlogNETUser user);
    
    Task<BlogNETUser?> GetUserByUsername(string? username);
    
    Task<BlogNETUser?> GetCurrentUser();
}