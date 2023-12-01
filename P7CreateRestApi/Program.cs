using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;
using Serilog;
using System.Text;



var builder = WebApplication.CreateBuilder(args);


//Configuration des logs
var configuration = builder.Configuration;
Log.Logger = new Serilog.LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Console()
    .CreateLogger();


//Configuration de la base de données

builder.Services.AddDbContext<LocalDbContext>(options=>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//configuration pour identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(Options=>

{
    // Password settings.
    Options.Password.RequireDigit = true;
    Options.Password.RequireLowercase = true;
    Options.Password.RequireNonAlphanumeric = true;
    Options.Password.RequireUppercase = true;
    Options.Password.RequiredLength = 8;
}

)
   . AddEntityFrameworkStores<LocalDbContext>()
    .AddDefaultTokenProviders();


//Configuration de l'athentication
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValiAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))


    };

});

//Configuration des politiques d'autorisation
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RHPolicy", policy => policy.RequireRole("RH"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

// Configuration des contrôleurs
builder.Services.AddControllers();

//Configuration du swagger/open API

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
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

//Configuration des services

builder.Services.AddScoped<IBidListRepository, BidListRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<IRuleNameRepository, RuleNameRepository>();
builder.Services.AddScoped<ICurvePointRepository, CurvePointsRepository>();



var app = builder.Build();


          // ... autres configurations

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                
            }
app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


//mapping des contrôleurs


            app.MapControllers();


//éxécution de l'application
              app.Run();
             

    internal class Options
    {
    }


