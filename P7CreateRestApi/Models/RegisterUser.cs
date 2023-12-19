using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Controllers
{
    public class RegisterUser
    {
        [Required]

        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string? Email { get; set; }

    }

}
