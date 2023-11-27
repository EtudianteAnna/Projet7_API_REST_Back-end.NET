using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Text;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        ConfigureServices(builder.Services);

        var app = builder.Build();

        Configure(app, builder.Environment);

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        // Ajout du service MVC pour les contrôleurs
        services.AddMvc();

        // Ajout des services DbContext et repositories
        services.AddIdentity<User, IdentityRole>()
               .AddDefaultTokenProviders();

        services.AddDbContext<LocalDbContext>(options =>
            options.UseSqlServer("YourConnectionString"));

        services.AddScoped<IBidListRepository, BidListRepository>();
        services.AddScoped<ICurvePointRepository, CurvePointRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<IRuleNameRepository, RuleNameRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddControllers();

        // Configuration du service d'authentification JWT
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FINDEXIUM",
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
                    ValidIssuer = "your-issuer",
                    ValidAudience = "your-audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
                };
            });
    }

    public static void Configure(WebApplication app, IHostEnvironment env)
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
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    internal class Options
    {
    }
}
