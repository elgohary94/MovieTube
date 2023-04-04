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

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _Context.Movies.ToListAsync();
        }
        
        public async Task<List<Genre>> GetAllGenre()
        {
            var gen = await _Context.Genres.ToListAsync();
            return gen; 
        }

        public async Task<Movie> FindMovieById(int id)
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

        public async Task<Movie> CreateMovie(Movie movie)
        {
            await _Context.Movies.AddAsync(movie);
            await _Context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> UpdateMovie(int id, Movie movie)
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

               return OneMovie;
        }

        public async Task<Movie> DeleteMovie(int id)
        {
            var OneMovie = await _Context.Movies.FirstOrDefaultAsync(m => m.ID == id);
            if(OneMovie is not null )
            {
                _Context.Remove(OneMovie);
            }

            return OneMovie;
        }
    }
}