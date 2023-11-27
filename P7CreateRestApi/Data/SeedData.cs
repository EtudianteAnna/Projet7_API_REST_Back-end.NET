using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new LocalDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<LocalDbContext>>());
        SeedRoles(context);
        SeedUsers(context);

        // Ajoutez d'autres méthodes de séquence si nécessaire
    }

    private static void SeedRoles(LocalDbContext option)
    {
        // Code de séquence pour les rôles
        // ...
    }

    private static void SeedUsers(LocalDbContext context)
    {
        // Code de séquence pour les utilisateurs
        // ...
    }
}
