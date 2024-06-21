using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace BookShop.Models;

public static class Utilities
{
    public const string USER_ADMIN_NAME = "Admin";
    public const string USER_ADMIN_EMAIL = "admin@admin.com";
    public const string USER_ADMIN_PASS = "Book123!";
    public const string USER_ADMIN_ROLE = "Admin";

    public const string USER_CUSTOMER_ROLE = "Customer";

    public const int SEED_NUMBER_CATEGORIES = 6;
    public const int SEED_NUMBER_BOOKS = 10;

    public const int NUMBER_CATEGORIES_PER_PAGE = 5;

    public const string DELETE_CONFIRMATION_ONSUBMIT_FORM_CALL = "event.preventDefault(); confirmDelete(event);";
    public const int ORDER_CONFIRMATION_NUM_ITEMS = 3;

    public const string UPLOAD_IMAGE_PATH = @"images\uploads";
    public const string DATA_TABLE_NAME = "mydatatable"; // Za DataTables plugin

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

    public static string GetPriceInDinars(double price)
    {
        NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = ".";
        nfi.NumberDecimalSeparator = ",";

        return $"{price.ToString("N2", nfi)} din";
    }

    public static string GetDateInSerbian(DateTime date)
    {
        CultureInfo culture = new CultureInfo("sr-Latn-CS");
        return date.ToString("dd. MMMM yyyy. \u2022 HH:mm", culture);
    }
}
