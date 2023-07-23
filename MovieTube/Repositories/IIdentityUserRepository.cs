using Microsoft.AspNetCore.Identity;

namespace MovieTube.Repositories
{
    public interface IIdentityUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserViewModel user, string userRole);
        Task<UserWapper> UserLoginAsync(UserLogInViewModel login);
        Task UserLogoutAsync();
        Task<IdentityUser> FindUserByNameAsync(string UserName);
        Task<IdentityUser> FindUserByEmailAsync(string Email);
        Task<bool> FindUserRoleAsync(IdentityUser founduser);
    }
}