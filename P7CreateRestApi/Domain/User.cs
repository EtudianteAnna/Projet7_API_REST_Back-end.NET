

using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace P7CreateRestApi.Domain

{
    public class User : IdentityUser<string>

    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int IdentityRole { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Fullname { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
