using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Infrastructure.Identity;

public static class AdminSeeder
{
    private const string AdminRole = "Admin";
    private const string AdminEmail = "admin@schoolweb.local";
    private const string AdminPassword = "Admin123!"; // change later

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 1) Ensure role exists
        if (!await roleManager.RoleExistsAsync(AdminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRole));
        }

        // 2) Ensure admin user exists
        var user = await userManager.FindByEmailAsync(AdminEmail);
        if (user == null)
        {
            user = new ApplicationUser
            {
                Email = AdminEmail,
                UserName = AdminEmail,
                FullName = "System Admin",
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, AdminPassword);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create admin user: {errors}");
            }
        }

        // 3) Ensure admin user has Admin role
        if (!await userManager.IsInRoleAsync(user, AdminRole))
        {
            await userManager.AddToRoleAsync(user, AdminRole);
        }
    }
}
