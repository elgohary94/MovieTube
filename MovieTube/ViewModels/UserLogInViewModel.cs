using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels
{
    public class UserLogInViewModel
    {
        [Required]
        [DisplayName("User Name")]
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public String PassWord { get; set; }

        [DisplayName("Remember Me!")]
        public bool IsPersistent { get; set; }
    }
}
