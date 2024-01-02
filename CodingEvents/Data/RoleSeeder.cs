namespace CodingEvents.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        string[] roles = { "Admin", "Organizer", "User" };

        foreach (var roleName in roles)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}

