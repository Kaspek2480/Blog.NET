using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models.ViewModels;

public class NewComment
{
    /*
     * jako Zalogowany Użytkownik mogę dodać komentarz do dowolnego
wpisu. Komentarz ma treść, datę i godzinę oraz IP komputera, z
którego został wysłany komentarz
     */
    [Required] public string? RawContent { get; set; }
    
    [Required] public Guid? BlogPostId { get; set; }
}