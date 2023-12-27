using System.Diagnostics;
using Blog.NET.Models.ViewModels;
using Blog.NET.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace Blog.NET.Pages.Admin;

[IgnoreAntiforgeryToken(Order = 1001)]
public class UploadFile : PageModel
{
    private IImageRepository _imageRepository;

    public UploadFile(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public void OnGet()
    {
        Debug.WriteLine("Get request");
    }

    public async Task<IActionResult> OnPost()
    {
        if (!Request.HasFormContentType || Request.Form.Files.Count == 0 || Request.Form.Files[0].Length == 0)
        {
            return BadRequest();
        }

        var file = Request.Form.Files[0];
        var uploadResult = await _imageRepository.UploadAsync(file);

        return new JsonResult(new UploadFileResult { link = uploadResult.SecureUrl.AbsoluteUri });
    }

    public async Task<IActionResult> OnDelete(string url)
    {
        //quite hacky, but it works
        var publicId = url.Split('/').Last().Split('.').First();

        var deleteResult = await _imageRepository.DeleteAsync(publicId);

        return new JsonResult(new { status = deleteResult.StatusCode });
    }
}