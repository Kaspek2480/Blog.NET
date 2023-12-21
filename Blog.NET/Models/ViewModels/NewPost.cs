using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models.ViewModels;

public class NewPost
{
    /*
     * jako Zalogowany użytkownik ma możliwość tworzenia wpisów.
     * Wpis ma tytuł, opis z ograniczeniem do 256 znaków oraz tagi. Jeden wpis
może zawierać kilka tagów. Do wpisu mogą być dołączone zdjęcia
(jedno lub więcej)
     */

    [Required] public string? Title { get; set; }

    [Required, StringLength(256)] public string? Description { get; set; }

    [Required, StringLength(5000)] public string? RawContent { get; set; }

    public List<string>? Images { get; set; } //TODO: add image upload

    public string? CustomUrl { get; set; }

    public bool Visible { get; set; } = true;
    public List<Tag>? Tags { get; set; }
}