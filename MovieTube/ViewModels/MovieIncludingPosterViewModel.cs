using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels
{
    public class MovieIncludingPosterViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Year { get; set; }

        public string MovieNationality { get; set; }

        public List<Actor> Actors { get; set; }

        public byte[] Poster { get; set; }

        public int GenreId { get; set; }

        public Genre? Genre { get; set; }
    }
}
