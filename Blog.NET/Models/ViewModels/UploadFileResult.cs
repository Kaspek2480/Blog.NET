namespace Blog.NET.Models.ViewModels;

public class UploadFileResult
{
    // ReSharper disable once InconsistentNaming
    // needs to be lowercase for the JSON response
    // https://froala.com/wysiwyg-editor/docs/concepts/image/upload/
    public string link { get; set; } = "";
}