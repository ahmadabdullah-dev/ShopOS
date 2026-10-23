using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataSeeder
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppUserRole> _roleManager;

    public DataSeeder(
        UserManager<AppUser> userManager,
        RoleManager<AppUserRole> roleManager
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        await SeedRoles();
        await SeedUsers();
    }

    public async Task SeedRoles()
    {
        var dbRoleNames = await _roleManager.Roles
            .Select(r => r.Name)
            .ToListAsync();

        var roles = new List<AppUserRole>()
        {
            new() { Name = "Platform_Owner" },
            new() { Name = "Platform_Admin" },
            new() { Name = "Tenant_Owner" },
            new() { Name = "Tenant_Admin" },
        };

        foreach (var role in roles)
        {
            if (!dbRoleNames.Contains(role.Name))
            {
                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create role '{role.Name}': {errors}");
                }
            }
        }
    }

    public async Task SeedUsers()
    {
        var users = new List<(AppUser user, string role, string password)>()
        {
            (new() { FirstName = "Ahmad", LastName = "Abdullah", UserName = "abdullah", Email = "ahmad@test.com", EmailConfirmed = true },
                "Platform_Owner", "Pa$$w0rd"),
            (new() { FirstName = "Sara", LastName = "Hassan", UserName = "sara.hassan", Email = "sara@test.com", EmailConfirmed = true },
                "Platform_Admin", "Pa$$w0rd"),
            (new() { FirstName = "Daniel", LastName = "Carter", UserName = "daniel.carter", Email = "daniel@test.com", EmailConfirmed = true },
                "Tenant_Owner", "Pa$$w0rd"),
            (new() { FirstName = "Emma", LastName = "Walker", UserName = "emma.walker", Email = "emma@test.com", EmailConfirmed = true },
                "Tenant_Admin", "Pa$$w0rd"),
        };

        foreach (var (user, role, password) in users)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email!);

            if (existingUser != null)
                continue;

            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user '{user.UserName}': {errors}");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to add user '{user.UserName}' to role '{role}': {errors}");
            }
        }
    }
}