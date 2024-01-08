using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;


public class AuthentificationController : ControllerBase
{
    private readonly ILogger<AuthentificationController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IJwtFactory _jwtFactory;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthentificationController(ILogger<AuthentificationController> logger, IUserRepository userRepository, IJwtFactory jwtFactory, IPasswordHasher<User> passwordHasher)
    {
        _logger = logger;
        _userRepository = userRepository;
        _jwtFactory = jwtFactory;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        _logger.LogInformation($"Tentative de connexion : {model.Username}");

        var user = await _userRepository.GetUserByCredentialsAsync(model.Username);
        if (user == null)
        {
            _logger.LogWarning($"Utilisateur avec le nom d'utilisateur {model.Username} non trouvé.");

            // L'utilisateur n'existe pas, vous pouvez ajouter un code pour créer cet utilisateur.
            // Créer un nouvel utilisateur avec le modèle
            var newUser = new User
            {
                Username = model.Username
            };
            // Ajouter le nouvel utilisateur à la base de données
            await _userRepository.AddUser(newUser);

            _logger.LogInformation($"Utilisateur {model.Username} créé avec succès.");

            // Générer le jeton JWT pour le nouvel utilisateur
            var newToken = _jwtFactory.GeneratedEncodedToken(newUser);

            return Ok(new { Message = "Utilisateur créé et connecté avec succès", Token = newToken });
            // Hasher le mot de passe avec l'utilisateur
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, model.Password);

        }

        // Vérifier le mot de passe avec le CustomPasswordHasher
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            _logger.LogWarning($"Mot de passe incorrect pour l'utilisateur {model.Username}.");
            return BadRequest("Mot de passe incorrect");
        }

        // Générer le jeton JWT pour l'utilisateur existant
        var token = _jwtFactory.GeneratedEncodedToken(user);

        _logger.LogInformation($"Connexion réussie pour l'utilisateur {model.Username}.");
        return Ok(new { Message = "Connexion réussie", Token = token });
    }
}
