using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTube.Models
{
    public class Movie
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [MaxLength(220)]
        public string Description { get; set; }

        public int Year { get; set; }

        public int MovieNationality { get; set; }


        public List<Actor> Actors{ get; set; }


        public byte[] Poster { get; set; }

        [ForeignKey("Genre")]
        public int GenreID { get; set; }

        public Genre Genre { get; set; }
    }
}
