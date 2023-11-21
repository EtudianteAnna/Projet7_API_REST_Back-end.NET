using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        Configure(app, builder.Environment);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Ajoutez le service MVC pour les contrôleurs
        services.AddMvc();
        // Ajoutez les services DbContext et repositories
        services.AddDbContext<LocalDbContext>(options =>
            options.UseSqlServer("YourConnectionString"));

        services.AddScoped<IBidListRepository, BidListRepository>();
        services.AddScoped<ICurvePointRepository, CurvePointRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<IRuleNameRepository, RuleNameRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Configuration du service d'authentification JWT
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Titre",
                Version = "1.0",
                Contact = new OpenApiContact
                {
                    Email = "bidon"
                }
            });
        });


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "your-issuer", // Mettez votre émetteur (issuer) JWT ici
                    ValidAudience = "your-audience", // Mettez votre audience JWT ici
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key")) // Mettez votre clé secrète JWT ici
                };
            });

    }

    private static void Configure(WebApplication app, IHostEnvironment env)
    {
        // ... autres configurations

        if (env.EnvironmentName == "Development")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
                c.RoutePrefix = "";
            });
        }

        app.UseHttpsRedirection();
        app.UseCors("MyPolicy");
        app.UseAuthentication(); // Ajoutez cette ligne pour activer l'authentification JWT
        app.MapControllers();
    }
}

                   