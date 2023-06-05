using Microsoft.AspNetCore.Identity;

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


        public async Task<IdentityResult> RegisterUser(RegisterUserViewModel user)
        {

            var NewUser = _Mapper.Map<IdentityUser>(user);

            var Result = await _User.CreateAsync(NewUser, user.Password);

            return Result;
        }

        public async Task<SignInResult> UserLogin(UserLogInViewModel login)
        {
            try
            {
                var FoundUser = await _User.FindByNameAsync(login.UserName);

                var signinresult = await _SignInManager.PasswordSignInAsync(FoundUser, login.PassWord,
                    login.IsPersistent, false);
                return signinresult;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task UserLogout()
        {
            await _SignInManager.SignOutAsync();
        }
    }
}
