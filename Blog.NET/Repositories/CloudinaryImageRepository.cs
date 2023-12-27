using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Blog.NET.Repositories;

public class CloudinaryImageRepository : IImageRepository
{
    private readonly IConfiguration _configuration;
    private readonly Account _account;

    public CloudinaryImageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _account = new Account(
            _configuration["Cloudinary:CloudName"],
            _configuration["Cloudinary:ApiKey"],
            _configuration["Cloudinary:ApiSecret"]
        );
    }

    public async Task<ImageUploadResult> UploadAsync(IFormFile file)
    {
        var client = new Cloudinary(_account);

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            DisplayName = file.FileName,
            Transformation = new Transformation().Crop("limit").Width(1000).Height(1000)
        };

        var uploadResult = await client.UploadAsync(uploadParams);

        if (uploadResult is { StatusCode: HttpStatusCode.OK })
            return uploadResult;

        throw new Exception("Error uploading image");
    }

    public async Task<DeletionResult> DeleteAsync(string publicId)
    {
        var client = new Cloudinary(_account);

        var deleteParams = new DeletionParams(publicId);

        var deleteResult = await client.DestroyAsync(deleteParams);

        return deleteResult;
    }
}