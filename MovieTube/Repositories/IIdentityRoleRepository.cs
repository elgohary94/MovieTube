using Microsoft.AspNetCore.Identity;

namespace MovieTube.Repositories
{
    public interface IIdentityRoleRepository
    {
        Task<List<IdentityRole>> GetallRolesAsync();
        Task<IdentityResult> CreateRoleAsync(NewRoleViewModel role);
        Task<IdentityResult> RemoveRoleAsync(string roleName);
        Task<List<string>> FindUserRoleAsync(IdentityUser user);
    }
}