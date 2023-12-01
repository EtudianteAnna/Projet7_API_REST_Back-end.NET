﻿using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var dbContext = serviceProvider.GetRequiredService<LocalDbContext>();

        await SeedRoles(roleManager, dbContext);
        await SeedUsers(userManager, dbContext);
        // Ajoutez d'autres méthodes de séquençage si nécessaire
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager, LocalDbContext dbContext)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        if (!await roleManager.RoleExistsAsync("RH"))
        {
            await roleManager.CreateAsync(new IdentityRole("RH"));
        }
    }

    private static async Task SeedUsers(UserManager<User> userManager, LocalDbContext dbContext)
    {
        // Vérifiez si l'utilisateur admin existe, sinon créez-le
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            var newAdmin = new User
            {
                UserName = "admin",
                Email = "admin@example.com"
                // Ajoutez d'autres propriétés d'utilisateur selon votre modèle
            };

            var result = await userManager.CreateAsync(newAdmin, "YourSecurePassword");

            if (result.Succeeded)
            {
                // Ajoutez l'utilisateur au rôle 'Admin'
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
            // Gérez les erreurs si la création de l'utilisateur échoue
        }
    }
}
