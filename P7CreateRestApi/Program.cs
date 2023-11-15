using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;

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
        services.AddDbContext<LocalDbContext>(options =>
            options.UseSqlServer("YourConnectionString"));

        services.AddScoped<IBidListRepository, BidListRepository>();
        services.AddScoped<ICurvePointRepository, CurvePointRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        _ = services.AddScoped<IRuleNameRepository, RuleNameRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", builder =>
            {
                _ = builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });
    }

    private static void Configure(WebApplication app, IHostEnvironment env)
    {
        if (env.EnvironmentName == "Development")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
            });
        }

        app.UseHttpsRedirection();
        app.UseCors("MyPolicy");
        app.MapControllers();
    }
}

/*using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;

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
        services.AddDbContext<LocalDbContext>(options =>
            options.UseSqlServer("YourConnectionString"));

        services.AddScoped<IBidListRepository, BidListRepository>();
        services.AddScoped<ICurvePointRepository, CurvePointRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<IRuleNameRepository, RuleNameRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });
    }

    private static void Configure(WebApplication app, IHostEnvironment env)
    {
        if (env.EnvironmentName == "Development")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
            });
        }

        app.UseHttpsRedirection();
        app.UseCors("MyPolicy");
        app.MapControllers();
    }
}*/
