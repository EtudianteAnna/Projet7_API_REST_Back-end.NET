using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;
using Serilog;
using System.Text;
using IJwtFactory = P7CreateRestApi.Repositories.IJwtFactory;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration des logs
        var configuration = builder.Configuration;
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Console()
            .CreateLogger();

        // Configuration de la base de données
        var  connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString != null)
        {
            builder.Services.AddDbContext<LocalDbContext>(options => options.UseSqlServer(connectionString));
        }
        else
        {
            // Gérer le cas où la chaîne de connexion est nulle
            // Vous pouvez journaliser un avertissement, effectuer une autre action corrective, ou lancer une exception
            Log.Warning("La chaîne de connexion à la base de données est nulle.");
            // Ou vous pouvez lancer une exception pour indiquer un problème avec la configuration
            throw new InvalidOperationException("La chaîne de connexion à la base de données est nulle.");
        }


        // Configuration pour Identity
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<LocalDbContext>()
        .AddDefaultTokenProviders();

        // Configuration de l'authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>

        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

        // Ajout du service de génération de jeton
        IServiceCollection serviceCollection = 

        // Configuration des politiques d'autorisation
        _ = builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RHPolicy", policy => policy.RequireRole("RH"));
            options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
        });

        // Configuration des contrôleurs
        builder.Services.AddControllers();

        // Configuration du swagger/open API
        builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen(option =>
        {
            option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Vous devez saisir un jeton valide",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new List<string>()
        }
            });
        });

        // Configuration des services
        builder.Services.AddScoped<IBidListRepository, BidListRepository>();
        builder.Services.AddScoped<IRatingRepository, RatingRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITradeRepository, TradeRepository>();
        builder.Services.AddScoped<IRuleNameRepository, RuleNameRepository>();
        builder.Services.AddScoped<ICurvePointRepository, CurvePointsRepository>();
        builder.Services.AddScoped<IPasswordHasher<IdentityUser>, CustomPasswordHasher >();

        var app = builder.Build();

        // ... autres configurations

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
            c.RoutePrefix = "";
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapping des contrôleurs
        app.MapControllers();

        // Exécution de l'application
        app.Run();
    }
}