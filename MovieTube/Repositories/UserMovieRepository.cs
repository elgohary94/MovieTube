using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieTube.Controllers.Repositories
{
    public class UserMovieRepository : IUserMovieRepository
    {
        private readonly MovieDbContext _Context;
        public UserMovieRepository(MovieDbContext context)
        {
            _Context = context;
            
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _Context.Movies.ToListAsync();
        }
        
        public async Task<List<Genre>> GetAllGenreAsync()
        {
            var gen = await _Context.Genres.ToListAsync();
            return gen; 
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            var genre = await _Context.Genres.FindAsync(id);
            return genre; 
        }

        public async Task<Movie> FindMovieByIdAsync(int id)
        {
            try
            {
                var movie = await _Context.Movies.FindAsync(id);
                
                return movie;

            }
            catch (Exception ex)
            {

                throw;
                
            }

        }

        public async Task CreateMovieAsync(Movie movie)
        {
            await _Context.Movies.AddAsync(movie);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int id, Movie movie)
        {
            var OneMovie = await _Context.Movies.FirstOrDefaultAsync(m => m.ID == id);

            if(OneMovie is not null )
            {
                OneMovie.Title = movie.Title;
                OneMovie.Actors = movie.Actors;
                OneMovie.Description = movie.Description;
                OneMovie.Genre = movie.Genre;
                OneMovie.MovieNationality = movie.MovieNationality;
                OneMovie.Poster = movie.Poster;
            }
               await _Context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var OneMovie = await _Context.Movies.FirstOrDefaultAsync(m => m.ID == id);
            if(OneMovie is not null )
            {
                _Context.Remove(OneMovie);
                await _Context.SaveChangesAsync();
            }
        }
    }
}