using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieTube.Controllers.Repositories
{
    public class UserMovieRepository
    {
        public MovieDbContext _Context;
        public UserMovieRepository(MovieDbContext context)
        {
            _Context = context;
            
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _Context.Movies.ToListAsync();
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
    }
}