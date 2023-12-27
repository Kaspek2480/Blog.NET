namespace Blog.NET.Repositories;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikes(Guid blogPostId);
}