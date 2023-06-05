using Microsoft.AspNetCore.Identity;

namespace MovieTube.Repositories
{
    public interface IIdentityUserRepository
    {
        Task<IdentityResult> RegisterUser(RegisterUserViewModel user);
        Task<SignInResult> UserLogin(UserLogInViewModel login);
        Task UserLogout();
    }
}