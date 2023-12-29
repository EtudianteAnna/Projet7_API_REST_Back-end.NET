using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;
    private readonly TestUserManager _userManager;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, TestUserManager userManager)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        _logger.LogInformation($"Ajout de l'utilisateur : {user.Id}");
        await _userRepository.AddAsync(user);
        _logger.LogInformation($"Utilisateur ajouté avec succès : {user.Id}");
        return Ok();
    }

    [HttpPost("validate")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
    public async Task<IActionResult> Validate([FromBody] User user)
    {
        _logger.LogInformation($"Validation de l'utilisateur : {user.Id}");

        if (!ModelState.IsValid)
        {
            _logger.LogError("ModelState invalide");
            return BadRequest();
        }

        await _userRepository.AddAsync(user);
        _logger.LogInformation($"Utilisateur validé et ajout� avec succès : {user.Id}");
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    public async Task<IActionResult> DeleteUser(int id)
    {
        _logger.LogInformation($"Suppression de l'utilisateur avec l'ID : {id}");

        await _userRepository.DeleteAsync(id);
        _logger.LogInformation($"Utilisateur supprim� avec succès : {id}");

        return Ok();
    }

    [HttpGet("secure/article-details")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    public async Task<ActionResult<List<User>>> GetAllUserArticles()
    {
        _logger.LogInformation("Récupération de tous les articles de l'utilisateur");
        var users = await _userRepository.GetAllAsync();
        _logger.LogInformation("Récupére articles de l'utilisateur");
        return Ok(users);
    }
    // Nouvelle méthode pour la mise à jour de l'utilisateur
    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        _logger.LogInformation($"Mise à jour de l'utilisateur avec l'ID : {id}");

        var existingUser = await _userRepository.GetByIdAsync(id);

        if (existingUser == null)
        {
            _logger.LogInformation($"Utilisateur avec l'ID {id} non trouvé.");
            return NotFound();
        }

        // Update properties of existingUser with values from updatedUser
        existingUser.UserName = updatedUser.UserName;
        // Update other properties as needed

        await _userRepository.UpdateAsync(existingUser);
        _logger.LogInformation($"Utilisateur mis à jour avec succès : {id}");

        return Ok();
    }
}