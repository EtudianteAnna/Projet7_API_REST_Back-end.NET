using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Domain

{
    public class User : IdentityUser<string>
    {
        public int UserId { get; set; }

        public int IdentityRole { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Fullname { get; set; }

        public string? Role { get; set; }
    }
}
