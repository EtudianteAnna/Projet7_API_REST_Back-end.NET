using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _logger.LogInformation($"Ajout de l'utilisateur : {user.Id}");
            await _userRepository.AddUserAsync(user);
            _logger.LogInformation($"Utilisateur ajouté avec succès : {user.Id}");
            return Ok();
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] User user)
        {
            _logger.LogInformation($"Validation de l'utilisateur : {user.Id}");

            if (!ModelState.IsValid)
            {
                _logger.LogError("ModelState invalide");
                return BadRequest();
            }

            await _userRepository.AddUserAsync(user);
            _logger.LogInformation($"Utilisateur validé et ajouté avec succès : {user.Id}");
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation($"Suppression de l'utilisateur avec l'ID : {id}");

            await _userRepository.DeleteUserAsync(id);
            _logger.LogInformation($"Utilisateur supprimé avec succès : {id}");

            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }

    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<List<User>> GetAllUsersAsync();
        // Autres méthodes du référentiel des utilisateurs
    }

    // Structure pour une nouvelle structure non spécifiée
    internal record struct NewStruct(object Item1, object Item2)
    {
        public static implicit operator (object, object)(NewStruct value)
        {
            return (value.Item1, value.Item2);
        }

        public static implicit operator NewStruct((object, object) value)
        {
            return new NewStruct(value.Item1, value.Item2);
        }
    }
}
