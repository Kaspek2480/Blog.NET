namespace Blog.NET.Repositories;

public interface IImageRepository
{
    Task<string> UploadAsync(IFormFile file);
}