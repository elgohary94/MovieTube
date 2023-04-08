using MovieTube.ViewModels;
using MovieTube.Migrations;

namespace MovieTube.Profiles
{
    public class MappingConfiguering : Profile
    {
        public MappingConfiguering()
        {
            CreateMap<MovieWithoutPosterViewModel, Movie>().ReverseMap();
            CreateMap<Movie,MovieIncludingPosterViewModel>().ReverseMap();
        }
    }
}
