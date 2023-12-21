using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.NET.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.NET.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BlogNETUser class
public class BlogNETUser : IdentityUser
{
    // Relacja jeden do wielu: jeden użytkownik wiele postów
    public List<BlogPost> BlogPosts { get; set; }
}

