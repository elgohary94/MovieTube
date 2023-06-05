using MovieTube.ViewModels;
using MovieTube.Migrations;
using Microsoft.AspNetCore.Identity;

namespace MovieTube.Profiles
{
    public class MappingConfiguering : Profile
    {
        public MappingConfiguering()
        {
            CreateMap<MovieWithoutPosterViewModel, Movie>().ReverseMap();
            CreateMap<Movie,MovieIncludingPosterViewModel>().ReverseMap();
            CreateMap<RegisterUserViewModel, IdentityUser>()
                .ForMember(dest=>dest.PasswordHash,opt=>opt.Ignore());
            
        }
    }
}
