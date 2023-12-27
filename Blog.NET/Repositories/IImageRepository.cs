using CloudinaryDotNet.Actions;

namespace Blog.NET.Repositories;

public interface IImageRepository
{
    Task<ImageUploadResult> UploadAsync(IFormFile file);
    
    Task<DeletionResult> DeleteAsync(string publicId);
}