using Microsoft.Build.Framework;

namespace P7CreateRestApi.Controllers
{
    public class LoginModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
