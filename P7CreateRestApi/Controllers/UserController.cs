﻿using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, UserManager<User> userManager)
    {
        _logger = logger;
        _userRepository = userRepository;

        // Personnalisation de l'initialisation de _userManager
        if (userManager != null)
        {
            _userManager = userManager;
            // Autres opérations d'initialisation si nécessaire
        }
        else
        {
            // Initialisation par défaut ou gestion des cas où userManager est null
            _userManager = new UserManager<User>(/* Paramètres d'initialisation */);
        }
    }

    public UserController(ILogger<UserController> object1, IUserRepository object2)
    {
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
        _logger.LogInformation($"Utilisateur validé et ajouté avec succès : {user.Id}");
        return Ok();
    }

    // Reste du code...

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
