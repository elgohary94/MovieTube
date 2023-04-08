using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTube.Controllers.Repositories
{
    public interface IUserMovieRepository
    {
        public Task<List<Movie>> GetAllMoviesAsync();
        public Task<List<Genre>> GetAllGenreAsync();
        public Task<Genre> GetGenreByIdAsync(int id);
        public Task<Movie> GetMovieByIdAsync(int id);
        public Task CreateMovieAsync(Movie movie);
        public Task UpdateMovieAsync(int id, Movie movie);
        public Task DeleteMovieAsync(int id);
    }
}