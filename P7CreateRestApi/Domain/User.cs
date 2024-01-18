using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;


namespace P7CreateRestApi.Domain
{
    public class User: IdentityUser<int> 
    {
        
        [Required]
        public int IdentityRole { get; set; }

        [Required]
        public  override string? UserName { get; set; }

        public override string? PasswordHash { get; set; }

        [Required]
        public string? Fullname { get; set; }

        [Required]
        public string? Role { get; set; }
                
        public override string Email { get; set; }

        public User(string email) 
        {
            Email = email;
        }

        public User(string userName, string email) 
        {
            UserName = userName;
            Email = email;
        }
    }
}
