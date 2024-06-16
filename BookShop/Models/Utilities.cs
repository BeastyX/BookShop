using Microsoft.AspNetCore.Identity;

namespace BookShop.Models;

public static class Utilities
{
    public const string USER_ADMIN_NAME = "Admin";
    public const string USER_ADMIN_EMAIL = "admin@admin.com";
    public const string USER_ADMIN_PASS = "Book123!";
    public const string USER_ADMIN_ROLE = "Admin";

    public const string USER_CUSTOMER_ROLE = "Customer";

    public static async Task SeedRolesAndCreateAdminAsync(WebApplication application) 
    {
        using (var scope = application.Services.CreateScope())
        {
            //Prvo dodajemo roles 
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { USER_ADMIN_ROLE, USER_CUSTOMER_ROLE };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role)) // ako rola ne postoji, kreiraj je
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            //Kreiranje administratora
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (await userManager.FindByEmailAsync(USER_ADMIN_EMAIL) == null)
            {
                var user = new IdentityUser
                {
                    UserName = USER_ADMIN_NAME,
                    Email = USER_ADMIN_EMAIL,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, USER_ADMIN_PASS);
                await userManager.AddToRoleAsync(user, USER_ADMIN_ROLE);
            }
        }
    }
}
