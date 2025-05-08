using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Utilities
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Tworzenie ról, jeśli nie istnieją
            string[] roleNames = { "Administrator", "Klient" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Tworzenie domyślnego użytkownika Administratora
            var adminEmail = "admin@wypozyczalnia.pl";
            var adminPassword = "Admin123!"; // Zmień na bezpieczne hasło
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Imie = "Admin",
                    Nazwisko = "User"
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Przypisanie roli Administrator
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                    Console.WriteLine($"Utworzono konto Administratora: {adminEmail}");
                }
                else
                {
                    throw new Exception("Nie udało się utworzyć użytkownika Administratora: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Upewnij się, że istniejący użytkownik ma rolę Administrator
                if (!await userManager.IsInRoleAsync(adminUser, "Administrator"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                    Console.WriteLine($"Przypisano rolę Administratora do istniejącego użytkownika: {adminEmail}");
                }
            }
        }
    }
}