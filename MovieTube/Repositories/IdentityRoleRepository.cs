using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MovieTube.Repositories;

public class IdentityRoleRepository : IIdentityRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityRoleRepository(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<IdentityRole>> GetallRolesAsync()
    {
        var roleslist = await _roleManager.Roles.ToListAsync();

        return roleslist;
    }

    public async Task<IdentityResult> CreateRoleAsync(NewRoleViewModel role)
    {
        try
        {
            var createdRole = new IdentityRole(role.RoleName);
            var result = await _roleManager.CreateAsync(createdRole);
            return result;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<IdentityResult> RemoveRoleAsync(string roleName)
    {
        try
        {
            var foundRole = await _roleManager.FindByNameAsync(roleName);
            var deletionResult = await _roleManager.DeleteAsync(foundRole);
            return deletionResult;

        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<List<string>> FindUserRoleAsync(IdentityUser user)
    {
        return (List<string>) await _userManager.GetRolesAsync(user);
    }
}

