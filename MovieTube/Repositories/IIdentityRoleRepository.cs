using Microsoft.AspNetCore.Identity;

namespace MovieTube.Repositories
{
    public interface IIdentityRoleRepository
    {
        List<IdentityRole> GetallRoles();
        Task<IdentityResult> CreateRole(NewRoleViewModel role);
        Task<IdentityResult> RemoveRole(string roleName);
    }
}