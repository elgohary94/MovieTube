using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set;}

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="This Field Doesn't Match The Password Field")]
        public string ConfirmPassword { get; set; }
        

    }
}
