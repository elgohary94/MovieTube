using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieTube.Models
{
    public class MovieDbContext : IdentityDbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

    }
}
