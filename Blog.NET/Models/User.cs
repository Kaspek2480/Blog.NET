using System.ComponentModel.DataAnnotations;

namespace Blog.NET.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Relacja jeden do wielu: jeden użytkownik wiele postów
        public List<BlogPost> BlogPosts { get; set; }
    }
}
