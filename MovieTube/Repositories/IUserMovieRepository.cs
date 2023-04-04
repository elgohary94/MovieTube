using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTube.Controllers.Repositories
{
    public interface IUserMovieRepository
    {
        public Task<List<Movie>> GetAllMovies();
        public Task<List<Genre>> GetAllGenre();
        public Task<Genre> GetGenreById(int id);
        public Task<Movie> FindMovieById(int id);
        public Task<Movie> CreateMovie(Movie movie);
        public Task<Movie> UpdateMovie(int id, Movie movie);
        public Task<Movie> DeleteMovie(int id);
    }
}