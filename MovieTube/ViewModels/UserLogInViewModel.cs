using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels
{
    public class UserLogInViewModel
    {
        [Required]
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String PassWord { get; set; }

        public bool IsPersistent { get; set; }
    }
}
