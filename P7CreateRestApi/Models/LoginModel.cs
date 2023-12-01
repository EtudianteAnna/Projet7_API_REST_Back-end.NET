using Microsoft.Build.Framework;

namespace P7CreateRestApi.Models
{
    public class LoginModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
