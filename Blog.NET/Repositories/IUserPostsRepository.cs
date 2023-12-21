using Blog.NET.Areas.Identity.Data;
using Blog.NET.Data;
using Blog.NET.Models;

namespace Blog.NET.Repositories;

public interface IUserPostsRepository {
    Task<List<BlogPost>> GetUserPosts(BlogNETUser user);
}