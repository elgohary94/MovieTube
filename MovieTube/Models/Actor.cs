using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTube.Models
{
    [PrimaryKey("FirstName","LastName")]
    public class Actor
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Nationality { get; set; }


        public List<Movie> Movies { get; set; }
    }
}
