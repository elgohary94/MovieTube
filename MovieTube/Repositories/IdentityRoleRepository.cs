using Microsoft.AspNetCore.Identity;

namespace MovieTube.Repositories;

public class IdentityRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityRoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateRole(NewRoleViewModel role)
    {
        var createdRole = new IdentityRole(role.RoleName);
        var result = await _roleManager.CreateAsync(createdRole);
        return result;
        
    }

    public async Task<IdentityResult> RemoveRole(string roleName)
    {
        try
        {
            var foundRole = await _roleManager.FindByNameAsync(roleName);
            var deletionResult = await _roleManager.DeleteAsync(foundRole);
            return deletionResult;

        }
        catch (Exception e)
        {
            
            throw e;
        }

    }
}

