using Microsoft.AspNetCore.Identity;

namespace MovieTube.Models
{
    public class UserWrapper
    {
        public IdentityUser user { get; set; }
        public SignInResult signInResult { get; set; }
    }
}
