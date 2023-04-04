using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTube.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }

        public string MovieNationality { get; set; }

        public List<Actor> Actors { get; set; }

        public int GenreId { get; set; }

        public Genre? Genre { get; set; }

    }
}
