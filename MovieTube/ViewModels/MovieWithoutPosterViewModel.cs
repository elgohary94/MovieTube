using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieTube.ViewModels
{
    public class MovieWithoutPosterViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Year { get; set; }
        [DisplayName("Movie Nationality")]
        public string MovieNationality { get; set; }

        public List<Actor> Actors { get; set; }

        public int GenreId { get; set; }

        public Genre? Genre { get; set; }

    }
}
