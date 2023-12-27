using Blog.NET.Data;

namespace Blog.NET.Repositories;

public class BlogPostLikeRepository : IBlogPostLikeRepository
{
    private readonly AppDbContext _context;
    
    public BlogPostLikeRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task<int> GetTotalLikes(Guid blogPostId)
    {
        // TODO: Implement this method, modify the database

        // for now return an arbitrary number
        return 11;
    }
}