using MovieTube.DTOs;
using MovieTube.Migrations;

namespace MovieTube.Profiles
{
    public class MappingConfiguering : Profile
    {
        public MappingConfiguering()
        {
            CreateMap<MovieDTO, Movie>().ReverseMap();
        }
    }
}
