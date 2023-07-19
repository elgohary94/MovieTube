using Microsoft.AspNetCore.Identity;
using MovieTube.Models;

namespace MovieTube.Repositories
{
    public class IdentityUserRepository : IIdentityUserRepository
    {
        private readonly UserManager<IdentityUser> _User;
        private readonly IMapper _Mapper;
        private readonly SignInManager<IdentityUser> _SignInManager;



        public IdentityUserRepository(UserManager<IdentityUser> User, IMapper Mapper, SignInManager<IdentityUser> signInManager)
        {
            _User = User;
            _Mapper = Mapper;
            _SignInManager = signInManager;
        }


        public async Task<IdentityResult> RegisterUserAsync(RegisterUserViewModel user)
        {

            var NewUser = _Mapper.Map<IdentityUser>(user);

            var Result = await _User.CreateAsync(NewUser, user.Password);
            
            return Result;
        }

        public async Task<UserWapper> UserLoginAsync(UserLogInViewModel login)
        {
            try
            {
                UserWapper userwrapper = new();
                
                userwrapper.user = await _User.FindByNameAsync(login.UserName);

                userwrapper.signInResult = await _SignInManager.PasswordSignInAsync(userwrapper.user, login.PassWord,
                    login.IsPersistent, false);
                
                return userwrapper;

            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task UserLogoutAsync()
        {
            await _SignInManager.SignOutAsync();
        }

        public async Task<IdentityUser> FindUserByNameAsync(string UserName)
        {
            var FoundUser = await _User.FindByNameAsync(UserName);
            return FoundUser;
        }

        public async Task<IdentityUser> FindUserByEmailAsync(string Email)
        {
            var FoundUser = await _User.FindByEmailAsync(Email);
            return FoundUser;
        }

        public async Task<bool> FindUserRoleAsync(IdentityUser founduser)
        {
           return await _User.IsInRoleAsync(founduser, "User");
        }
    }  
}
