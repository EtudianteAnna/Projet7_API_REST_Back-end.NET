using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new IdentityDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>());
        SeedRoles(context);
        SeedUsers(context);

        // Ajoutez d'autres méthodes de séquence si nécessaire
    }

    private static void SeedRoles(IdentityDbContext option)
    {
        // Code de séquence pour les rôles
        // ...
    }

    private static void SeedUsers(IdentityDbContext context)
    {
        // Code de séquence pour les utilisateurs
        // ...
    }
}
