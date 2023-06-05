using Microsoft.AspNetCore.Identity;

namespace MovieTube.Repositories
{
    public class IdentityUserRepository
    {
        private readonly UserManager<IdentityUser> _User;
        private readonly IMapper _Mapper;



        public IdentityUserRepository(UserManager<IdentityUser> User, IMapper Mapper)
        {
            _User = User;
            _Mapper = Mapper;
        }


        public async Task<IdentityResult> RegisterUser(RegisterUserViewModel user)
        {

            var NewUser = _Mapper.Map<IdentityUser>(user);
            
            var Result = await _User.CreateAsync(NewUser,user.Password);

            return Result;
        }

    }
}
